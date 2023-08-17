$(document).ready(function () {
    $('#demoForm').validate({
        rules: {
            userEmail: {
                required: true,
                email: true
            },
            userPassword: {
                required: true,
                minlength: 8,
                maxlength: 16
            }
        },
        messages: {
            userEmail: {
                required: "please enter email",
                email: "enter valid email"
            },
            userPassword: {
                required: "Password required",
                minlength: "Password length must be greater than 8",
                maxlength: "Password length must be less than 16"
            }
        }
    });
});