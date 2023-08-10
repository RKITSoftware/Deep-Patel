$(document).ready(function () {

    // Success Msg Hide
    $('#successMsg').hide();

    // Danger Msg Hide
    $('#dangerMsg').hide();

    // Checks whether string containes whitespace or not.
    function hasWhiteSpace(string) {
        return string.indexOf(' ') >= 0;
    }

    // Checks whether string containes @ or not.
    function hasSpecialsymbol(string) {
        return string.indexOf('@') >= 0;
    }

    // First hide userNameErrorMessage.
    $('#userNameErrorMessage').hide();
    let isNameValid = true;

    // when user writing the username at that time it validate whether it is correct or not.
    // if isUserNameValid is correct then it will not show error message.
    // else it will show.
    $('#regFormUserName').keyup(function () {
        let userName = $('#regFormUserName').val().trim();
        let userNameCheck = /^[a-zA-Z]+ [a-zA-Z]+$/;

        if (!userNameCheck.test(userName)) {
            $('#userNameErrorMessage').show();
            $('#userNameErrorMessage').html('Name is Invalid');
            isNameValid = false;
        } else {
            $('#userNameErrorMessage').hide();
            isNameValid = true;
        }
    });

    // First it hide email error message.
    $('#userEmailErrorMessage').hide();
    let isUserEmailValid = true;

    // when user focusout email input tag then it will perform function whether it is correct or not
    // if isUserEmailValid is true then it will don't show error caption.
    // else it will show.
    $('#regFormUserEmail').blur(function () {
        const userEmail = $('#regFormUserEmail').val().trim();
        let regex = /^([_\-\.0-9a-zA-Z]+)@([_\-\.0-9a-zA-Z]+)\.([a-zA-Z]){2,7}$/;

        if ($.trim(userEmail).match(regex)) {
            isUserEmailValid = true;
            $('#userEmailErrorMessage').hide();
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
    $('#regFormUserPassword').on('keyup', function () {
        const userPassword = $('#regFormUserPassword').val().trim();

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

    // Insert data on local storage.
    function insertDataOnLocal() {
        const userName = $('#regFormUserName').val().trim();
        const userEmail = $('#regFormUserEmail').val().trim();
        const userPassword = $('#regFormUserPassword').val().trim();

        // Creating object of user information.
        const myObj = {
            'userName': userName,
            'userEmail': userEmail,
            'userPassword': userPassword,
        };

        // Storing values in arrays og object form.
        var myRecords = JSON.parse(localStorage.getItem('myRecords') || '[]');
        myRecords.push(myObj);
        localStorage.setItem('myRecords', JSON.stringify(myRecords));
    }

    // Reseting input values.
    function resetDetails() {
        $('#regFormUserName').val("");
        $('#regFormUserEmail').val("");
        $('#regFormUserPassword').val("");
    }

    // Check whether user data is valid or not.
    // if valid then store it on local storage.
    // else give danger alert message.
    $('#btnRegForm').click(function () {
        if (isNameValid && isUserEmailValid && isPasswordValid) {
            insertDataOnLocal();
            $('#successMsg').show();
        } else {
            $('#dangerMsg').show();
        }

        // Clear the details
        resetDetails();
    });

    // When mouse over a eye icon it will show password.
    $('#passwordShower').mouseover(function () {
        $('#eyeIcon').removeClass('fa-eye-slash');
        $('#eyeIcon').addClass('fa-eye');
        $('#regFormUserPassword').attr('type', 'text');
    });

    // When mouse out a eye icon it will hide password.
    $('#passwordShower').mouseout(function () {
        $('#eyeIcon').removeClass('fa-eye');
        $('#eyeIcon').addClass('fa-eye-slash');
        $('#regFormUserPassword').attr('type', 'password');
    });
});