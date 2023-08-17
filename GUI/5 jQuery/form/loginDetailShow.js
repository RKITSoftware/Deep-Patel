$(document).ready(function () {
    let users = JSON.parse(localStorage.getItem('users') || '[]');

    // It will append the table row to the table.
    $.each(users, function (index, value) {
        $('#loginDetailTable').append(`<tr class="row">
            <td class="col-4 pl-4 text-center">${+index + 1}</td>
            <td class="col-4 text-center">${value.userName}</td>
            <td class="col-4 text-center">${value.userEmail}</td>
            </tr>`);
    });
    
    // for (let index in users) {
    //     console.log(index);
    //     $('#loginDetailTable').append(`<tr class="row">
    //         <td class="col-4 pl-4 text-center">${+index + 1}</td>
    //         <td class="col-4 text-center">${users[index].userName}</td>
    //         <td class="col-4 text-center">${users[index].userEmail}</td>
    //         </tr>`);
    // }
});