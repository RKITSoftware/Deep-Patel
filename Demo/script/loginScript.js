$(document).ready(function () {

    // Danger Msg Hide.
    $('#dangerMsg').hide();

    // Check the user in local storage.
    // if user found then return it's value otherwise return undefined.
    function CheckInLocal() {
        // Getting values of user email and password
        const userEmail = $('#loginUserEmail').val().trim();
        const userPassword = $('#loginUserPassword').val().trim();

        // finding the user details on localStorage of myRecords.
        const myRecords = JSON.parse(localStorage.getItem('myRecords') || '[]');
        const user = myRecords.find((user) => {
            return userEmail === user['userEmail'] && userPassword === user['userPassword'];
        });

        return user;
    }

    // Reset input element value.
    function resetDetails() {
        $('#loginUserEmail').val("");
        $('#loginUserPassword').val("");
    }

    // user click login button at that time it check user in local storage and
    // if it find then it will go to home page
    // else it will shoe danger message and reset input element value.
    $('#btnLogIn').click(function () {
        const userData = CheckInLocal();
        if (userData !== undefined) {
            // setting the session storage values
            sessionStorage.setItem('userName', userData['userName']);
            sessionStorage.setItem('userEmail', userData['userEmail']);
            sessionStorage.setItem('userPassword', userData['userPassword']);

            // Replace the window location with home page.
            window.location.replace('http://127.0.0.1:5500/templates/home.html');
        } else {
            // Showing error msg and reseting the details.
            $('#dangerMsg').show();
            resetDetails();
        }
    });

    // When mouse over on div it will show password and change icon.
    $('#passwordShower').mouseover(function () {
        $('#eyeIcon').removeClass('fa-eye-slash');
        $('#eyeIcon').addClass('fa-eye');
        $('#loginUserPassword').attr('type', 'text');
    });

    // When mouse out on div it will show password and change icon.
    $('#passwordShower').mouseout(function () {
        $('#eyeIcon').removeClass('fa-eye');
        $('#eyeIcon').addClass('fa-eye-slash');
        $('#loginUserPassword').attr('type', 'password');
    });
});