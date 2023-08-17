$(document).ready(function () {

    const myArr = [3, 5, 7, 1, 2, 4, 8, 10];
    const myObj = {
        firstName: 'Deep',
        lastName: 'Patel',
        age: 21
    }

    // jQuery each
    console.log("Printing Array using each");
    $.each(myArr, function (idx, val) {
        console.log(idx, val);
    });

    console.log("Printing the object using each");
    $.each(myObj, function (key, val) {
        console.log(key, val);
    });

    // jQuery extend
    let myObj1 = $.extend({ city: "Limbdi" }, myObj);
    console.log("After extending myObj", myObj1);

    // jQuery merge
    let myArr1 = $.merge(myArr, [3, 5, 7]);
    console.log('After merging with myArr', myArr1);

    // using grep on myArr1
    let myarr2 = $.grep(myArr1, function (val) {
        return val % 2 === 0;
    });
    console.log('After Filtering myArr1', myarr2);

    // jQuery map
    let myArr3 = $.map(myArr1, function (val) {
        return val * 2;
    });
    console.log('After mapping', myArr3);
});