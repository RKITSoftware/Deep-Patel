$(document).ready(function () {

    function asyncEvent() {
        var dfd = $.Deferred();

        setTimeout(function () {
            dfd.resolve('hurray');
        }, 1000);

        setTimeout(function () {
            dfd.reject('sorry');
        }, 2000);

        return dfd.promise();
    }

    $.when(asyncEvent()).then(
        // Attach a Done
        function (resolve) {
            console.log(resolve + ', things are going well');
        },

        // Fail
        function (reject) {
            console.log(reject + ', you fail this time');
        },
    );
});