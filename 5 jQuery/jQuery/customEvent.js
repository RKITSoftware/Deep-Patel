$(document).ready(function () {

    // Enter key press then it will show alert message.
    $('textarea').on("enterKey", function (e) {
        alert("You have pressed Enter key!");
    });

    // Custom function when Enter key press it will call enterKey event.
    $('textarea').keyup(function (e) {
        if (e.keyCode == 13) {
            $(this).trigger("enterKey");
        }
    });

    // $('#txtCustomEvent').keyup(function (e) {
    //     $('#demo').text($(this).val());
    // })

    // $('#txtCustomEvent').on('change', function (e) {
    //     $(this).trigger('keyup');
    // })
});