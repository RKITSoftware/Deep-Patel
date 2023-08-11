$(document).ready(function () {

    // Defining variables.
    let $btnChange = $('#btnChange');
    let $btnCheck = $('#btnCheck');
    let $modalUserName = $('#modalUserName');
    let $modalUserEmail = $('#modalUserEmail');
    let $modalPassword = $('#modalPassword');
    let $modalUserNewPassword = $('#modalUserNewPassword');

    // Tells user details is correct or not
    let isUserVerified = false;

    // It hide password change field
    $modalPassword.hide();

    // Change button click event
    $btnChange.click(function () {

        // User details check
        if (isUserVerified) {

            // Taking values from password modal
            const userName = $modalUserName.val().trim();
            const newUserPassword = $modalUserNewPassword.val().trim();

            // Parsing data from local storage.
            const myRecords = JSON.parse(localStorage.getItem('myRecords') || '[]');

            // Find the user with it's name
            myRecords.find((user) => {
                if (userName === user['userName']) {

                    // check whether newUserPassword doesn't contain null string or oldpassword.
                    if (newUserPassword !== "" && newUserPassword !== user['userPassword']) {
                        user['userPassword'] = newUserPassword;
                    }
                    return true;
                }
            });

            // Storing data in localstorage after updating the password.
            localStorage.setItem('myRecords', JSON.stringify(myRecords));
        }
    });

    // Checking the user value on local storage
    $btnCheck.click(function () {

        // Taking values 
        const userName = $modalUserName.val().trim();
        const userEmail = $modalUserEmail.val().trim();

        // Parsing localStorage json object data.
        const myRecords = JSON.parse(localStorage.getItem('myRecords') || '[]');

        // Finding user
        const user = myRecords.find((user) => {
            return userEmail === user['userEmail'] && userName === user['userName'];
        });

        // If user is there in localstorage.
        if (user !== undefined) {
            isUserVerified = true;
            $modalPassword.show();
        } else {
            // else it will hide password field and give alert.
            isUserVerified = false;
            $modalPassword.hide(function () {
                alert('Enter correct details');
            });
        }
    });
});