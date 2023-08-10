$(document).ready(function () {

    function asyncEvent() {
        var dfd = $.Deferred();

        setTimeout(function () {
            dfd.resolve('hurray');
        }, 5000);

        setTimeout(function () {
            dfd.reject('sorry');
        }, 4000);

        setTimeout(function working() {
            if (dfd.state === "pending") {
                dfd.notify('working...');
                setTimeout(working, 500);
            }
        }, 1000);

        return dfd.promise();
    }

    $.when(asyncEvent()).then(
        // Attach a Done
        function (success) {
            console.log(success + ', things are foing well');
        },

        // Fail
        function (fail) {
            console.log(fail + ', you fail this time');
        },

        // Progress Handler
        function (status) {
            $('body').append(status);
        }
    );
});