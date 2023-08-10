import { CheckUserLogin } from "./checkUserLogin.js";

// check user login
CheckUserLogin();

$(document).ready(function () {
    // Getting the values form session storage.
    let userName = sessionStorage.getItem('userName').split(' ')[0];
    let userEmail = sessionStorage.getItem('userEmail').slice(0, 5);

    // creating localname.
    let localName = userName + userEmail;
    const userExpenseData = JSON.parse(localStorage.getItem(localName) || '[]');

    // showing the user expense data to user.
    let totalAmount = 0;
    console.log(totalAmount);
    $.each(userExpenseData, function (index, item) {
        let appendString = `<tr>
        <th scope="row">${+index + 1}</th>
        <td>${item.itemName}</td>
        <td>${item.itemAddedDate}</td>`;

        if (item.itemAction === "Debit") {
            totalAmount -= Number(item.itemValue);
            appendString += `<td>${item.itemValue}</td>
                <td>-</td>`;
        } else {
            totalAmount += Number(item.itemValue);
            appendString += `<td>-</td>
                <td>${item.itemValue}</td>`;
        }

        appendString += `<td>${totalAmount}</td>
                        </tr>`;

        $('#viewExpenseTableBody').append(appendString);
    });
});