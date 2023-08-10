$(document).ready(function () {

    // It hide password change div if user details verify then it will show to user.
    $('#modalPassword').hide();

    // Variable for checking is User Details verify or not.
    let isUserVerified = false;

    // when change button click it will take userName and userEmail values
    // and find user in local storage and if they found then it will change the 
    // password and if they don't find the user then we get alert.
    $('#btnChange').click(function () {
        if (isUserVerified) {

            // taking values from modal
            const userName = $('#modalUserName').val().trim();
            const newUserPassword = $('#modalUserNewPassword').val().trim();

            // parsing data from local storage.
            const myRecords = JSON.parse(localStorage.getItem('myRecords') || '[]');

            // find the user with its name
            myRecords.find((user) => {
                if (userName === user['userName']) {

                    // check whether newUserPassword doesn't contain null string or oldpassword.
                    if (newUserPassword !== "" && newUserPassword !== user['userPassword']) {
                        user['userPassword'] = newUserPassword;
                    }
                    return true;
                }
            });

            // storing data in localstorage after updating the password.
            localStorage.setItem('myRecords', JSON.stringify(myRecords));
        }
    });

    // Checking the value when user enter name and email if it correct and match with userData then
    // we show enter new password field.
    $('#btnCheck').click(function () {
        // Taking values 
        const userName = $('#modalUserName').val().trim();
        const userEmail = $('#modalUserEmail').val().trim();

        // Parsing localStorage json object data.
        const myRecords = JSON.parse(localStorage.getItem('myRecords') || '[]');
        const user = myRecords.find((user) => {
            return userEmail === user['userEmail'] && userName === user['userName'];
        });

        // if user is there in localstorage.
        if (user !== undefined) {
            isUserVerified = true;
            $('#modalPassword').show();
        } else {
            // else it will hide password field and give alert.
            isUserVerified = false;
            $('#modalPassword').hide(function () {
                alert('Enter correct details');
            });
        }
    });
});