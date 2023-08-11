$(document).ready(function () {

    // Defining jQuery variable
    let $regFormUserName = $('#regFormUserName');
    let $regFormUserEmail = $('#regFormUserEmail');
    let $regFormUserPassword = $('#regFormUserPassword');
    let $successMsg = $('#successMsg');
    let $dangerMsg = $('#dangerMsg');
    let $userNameErrorMessage = $('#userNameErrorMessage');
    let $userEmailErrorMessage = $('#userEmailErrorMessage');
    let $passwordRuleLength = $('#passwordRuleLength');
    let $passwordRuleSymbol = $('#passwordRuleSymbol');
    let $passwordRuleWhiteSpace = $('$passwordRuleWhiteSpace');
    let $btnRegForm = $('#btnRegForm');
    let $eyeIcon = $('#eyeIcon');
    let $passwordShower = $('#passwordShower');

    // Success Msg Hide
    $successMsg.hide();

    // Danger Msg Hide
    $dangerMsg.hide();

    // Checks whether string containes whitespace or not.
    function hasWhiteSpace(string) {
        return string.indexOf(' ') >= 0;
    }

    // Checks whether string containes @ or not.
    function hasSpecialsymbol(string) {
        return string.indexOf('@') >= 0;
    }

    // First hide userNameErrorMessage.
    $userNameErrorMessage.hide();
    let isNameValid = true;

    // Checks userName when key is up
    $regFormUserName.keyup(function () {
        let userName = $regFormUserName.val().trim();
        let userNameCheck = /^[a-zA-Z]+ [a-zA-Z]+$/;

        if (!userNameCheck.test(userName)) {
            $userNameErrorMessage.show();
            $userNameErrorMessage.html('Name is Invalid');
            isNameValid = false;
        } else {
            $userNameErrorMessage.hide();
            isNameValid = true;
        }
    });

    // First it hide email error message.
    $userEmailErrorMessage.hide();
    let isUserEmailValid = true;

    // Check userEmail value when it focusout
    $regFormUserEmail.blur(function () {
        const userEmail = $regFormUserEmail.val().trim();
        let regex = /^([_\-\.0-9a-zA-Z]+)@([_\-\.0-9a-zA-Z]+)\.([a-zA-Z]){2,7}$/;

        // Check userEmail
        if ($.trim(userEmail).match(regex)) {
            // Email is valid.
            isUserEmailValid = true;
            $userEmailErrorMessage.hide();
        } else {
            // Email is invalid.
            $userEmailErrorMessage.show();
            $userEmailErrorMessage.text('Your Email is InCorrect');
            isUserEmailValid = false;
        }
    });

    // Rules for password.
    $passwordRuleLength.show();
    $passwordRuleSymbol.show();
    $passwordRuleWhiteSpace.show();
    let isPasswordValid = true;

    // Add event when keyboard key is up.
    $regFormUserPassword.on('keyup', function () {
        const userPassword = $regFormUserPassword.val().trim();

        // Check Whitespace
        if (hasWhiteSpace(userPassword)) {
            $passwordRuleWhiteSpace.css('color', 'red');
            isPasswordValid = false;
        } else {
            $passwordRuleWhiteSpace.css('color', 'green');
            isPasswordValid = true;
        }

        // Check Special Symbol
        if (hasSpecialsymbol(userPassword)) {
            $passwordRuleSymbol.css('color', 'green');
            isPasswordValid = true;
        } else {
            $passwordRuleSymbol.css('color', 'red');
            isPasswordValid = false;
        }

        // Check password length
        if (userPassword.length >= 8) {
            $passwordRuleLength.css('color', 'green');
            isPasswordValid = true;
        } else {
            $passwordRuleLength.css('color', 'red');
            isPasswordValid = false;
        }
    });

    // Insert data on local storage.
    function insertDataOnLocal() {
        const userName = $regFormUserName.val().trim();
        const userEmail = $regFormUserEmail.val().trim();
        const userPassword = $regFormUserPassword.val().trim();

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
        $regFormUserName.val("");
        $regFormUserEmail.val("");
        $regFormUserPassword.val("");
    }

    // Check user deatils are correct or not
    $btnRegForm.click(function () {
        if (isNameValid && isUserEmailValid && isPasswordValid) {
            // If correct the store on local storage.
            insertDataOnLocal();
            $successMsg.show();
        } else {
            // Show error
            $dangerMsg.show();
        }

        // Clear the details
        resetDetails();
    });

    // When mouse over a eye icon it will show password.
    $passwordShower.mouseover(function () {
        $eyeIcon.removeClass('fa-eye-slash');
        $eyeIcon.addClass('fa-eye');
        $regFormUserPassword.attr('type', 'text');
    });

    // When mouse out a eye icon it will hide password.
    $passwordShower.mouseout(function () {
        $eyeIcon.removeClass('fa-eye');
        $eyeIcon.addClass('fa-eye-slash');
        $regFormUserPassword.attr('type', 'password');
    });
});