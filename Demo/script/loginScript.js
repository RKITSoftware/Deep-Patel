$(document).ready(function () {

    // Defining jQuery variable
    let $dangerMsg = $('#dangerMsg');
    let $loginUserEmail = $('#loginUserEmail');
    let $loginUserPassword = $('#loginUserPassword');
    let $btnLogIn = $('#btnLogIn');
    let $eyeIcon = $('#eyeIcon');
    let $passwordShower = $('#passwordShower');

    // Danger Msg Hide.
    $dangerMsg.hide();

    // Check user is valid or not
    function CheckInLocal() {

        // Getting values of user email and password
        const userEmail = $loginUserEmail.val().trim();
        const userPassword = $loginUserPassword.val().trim();

        // Finding the user details on localStorage of myRecords.
        const myRecords = JSON.parse(localStorage.getItem('myRecords') || '[]');
        const user = myRecords.find((user) => {
            return userEmail === user['userEmail'] && userPassword === user['userPassword'];
        });

        return user;
    }

    // Reset input element value.
    function resetDetails() {
        $loginUserEmail.val("");
        $loginUserPassword.val("");
    }

    // Login button click event
    $btnLogIn.click(function () {

        // Check user Data in Local Storage.
        const userData = CheckInLocal();

        // If user is valid then add details into session storage
        if (userData !== undefined) {

            // Setting the session storage values
            sessionStorage.setItem('userName', userData['userName']);
            sessionStorage.setItem('userEmail', userData['userEmail']);
            sessionStorage.setItem('userPassword', userData['userPassword']);

            // Replace the window location with home page.
            window.location.replace('http://127.0.0.1:5500/templates/home.html');
        } else {
            // Showing error msg and reseting the details.
            $dangerMsg.show();
            resetDetails();
        }
    });

    // When mouse over on div it will show password and change eye icon.
    $passwordShower.mouseover(function () {
        $eyeIcon.removeClass('fa-eye-slash');
        $eyeIcon.addClass('fa-eye');
        $loginUserPassword.attr('type', 'text');
    });

    // When mouse out on div it will show password and change eye icon.
    $passwordShower.mouseout(function () {
        $eyeIcon.removeClass('fa-eye');
        $eyeIcon.addClass('fa-eye-slash');
        $loginUserPassword.attr('type', 'password');
    });
});