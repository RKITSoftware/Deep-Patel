$(document).ready(function () {

    // UserName caption show at starting.
    $('#userNameErrorMessage').show();
    let isUserNameValid = true;

    // Checks whether string containes whitespace or not.
    function hasWhiteSpace(string) {
        return string.indexOf(' ') >= 0;
    }

    // checks whether string containes @ or not.
    function hasSpecialsymbol(string) {
        return string.indexOf('@') >= 0;
    }

    // when user writing the username at that time it validate whether it is correct or not.
    // if isUserNameValid is correct then it will not show error message.
    // else it will show.
    $('#txtUserName').keyup(function () {
        let userName = $('#txtUserName').val().trim();

        if (userName.length == "") {
            $('#userNameErrorMessage').show();
            isUserNameValid = false;
        } else if (hasWhiteSpace(userName)) {
            $('#userNameErrorMessage').show();
            $('#userNameErrorMessage').html("Username can't contain spaces.");
            isUserNameValid = false;
        } else if (userName.length <= 2 || userName.length > 12) {
            $('#userNameErrorMessage').show();
            $('#userNameErrorMessage').html("Username length should between 3 to 12 Characters.");
            isUserNameValid = false;
        } else {
            isUserNameValid = true;
            $('#userNameErrorMessage').hide();
        }
    });

    // First it hide email error message.
    $('#userEmailErrorMessage').hide();
    let isUserEmailValid = true;

    // when user focusout email input tag then it will perform function whether it is correct or not
    // if isUserEmailValid is true then it will don't show error caption.
    // else it will show.
    $('#emlUserEmail').blur(function () {
        const userEmail = $('#emlUserEmail').val().trim();
        let regex = /^([_\-\.0-9a-zA-Z]+)@([_\-\.0-9a-zA-Z]+)\.([a-zA-Z]){2,7}$/;

        if ($.trim(userEmail).match(regex)) {
            isUserEmailValid = true;
        } else {
            $('#userEmailErrorMessage').show();
            $('#userEmailErrorMessage').text('Your Email is InCorrect');
            isUserEmailValid = false;
        }
    });

    // Rules for password.
    $('#passwordRuleLength').show();
    $('#passwordRuleSymbol').show();
    $('#passwordRuleWhiteSpace').show();
    let isPasswordValid = true;

    // if userPassword satisfies all condition of password then it all conditions become green.
    // else it will be red.
    $('#pwdUserPassword').on('keyup', function () {
        const userPassword = $('#pwdUserPassword').val().trim();

        // Check Whitespace
        if (hasWhiteSpace(userPassword)) {
            $('#passwordRuleWhiteSpace').css('color', 'red');
            isPasswordValid = false;
        } else {
            $('#passwordRuleWhiteSpace').css('color', 'green');
            isPasswordValid = true;
        }

        // Check Special Symbol
        if (hasSpecialsymbol(userPassword)) {
            $('#passwordRuleSymbol').css('color', 'green');
            isPasswordValid = true;
        } else {
            $('#passwordRuleSymbol').css('color', 'red');
            isPasswordValid = false;
        }

        // Check password length
        if (userPassword.length >= 8) {
            $('#passwordRuleLength').css('color', 'green');
            isPasswordValid = true;
        } else {
            $('#passwordRuleLength').css('color', 'red');
            isPasswordValid = false;
        }
    });

    // Show confirm password error message.
    $('#confirmPasswordErrorMessage').show();
    let isSecondPasswordMatch = true;

    // it will check whether password and confirm paassword are same or not.
    $('#confirmPassword').keyup(function () {
        let userPassword = $('#pwdUserPassword').val().trim();
        let confirmPassword = $('#confirmPassword').val().trim();

        if (confirmPassword == "" ||
            confirmPassword !== userPassword) {
            isSecondPasswordMatch = false;
            $('#confirmPasswordErrorMessage').show();
        } else {
            isSecondPasswordMatch = true;
            $('#confirmPasswordErrorMessage').hide();
        }
    });

    // Insert data on local storage.
    function insertDataOnLocal() {
        const userName = $('#txtUserName').val().trim();
        const userPassword = $('#pwdUserPassword').val().trim();
        const userEmail = $('#emlUserEmail').val().trim();

        // Creating object of user information.
        const myObj = {
            'userName': userName,
            'userPassword': userPassword,
            'userEmail': userEmail
        };

        // Storing values in arrays og object form.
        var users = JSON.parse(localStorage.getItem('users') || '[]');
        users.push(myObj);
        localStorage.setItem('users', JSON.stringify(users));
    }

    // when user submit the form it will check whether it have all details correct or not.
    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        if (isUserNameValid && isUserEmailValid && isPasswordValid && isSecondPasswordMatch) {
            insertDataOnLocal();
            alert('Your Details are correct');
        } else {
            alert('Details are incorrect');
        }
    });

    // When mouse over on div it will show password and change icon.
    $('#passwordShower').mouseover(function () {
        $('#eyeIcon').removeClass('fa-eye');
        $('#eyeIcon').addClass('fa-eye-slash');
        $('#pwdUserPassword').attr('type', 'text');
    });

    // When mouse out on div it will show password and change icon.
    $('#passwordShower').mouseout(function () {
        $('#eyeIcon').removeClass('fa-eye-slash');
        $('#eyeIcon').addClass('fa-eye');
        $('#pwdUserPassword').attr('type', 'password');
    });
});