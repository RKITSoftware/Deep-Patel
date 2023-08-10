import { CheckUserLogin } from "./checkUserLogin.js";

CheckUserLogin();

const addExpenseForm = document.getElementById('addExpenseForm');

addExpenseForm.addEventListener('submit', () => {
    let userName = sessionStorage.getItem('userName').split(' ')[0];
    let userEmail = sessionStorage.getItem('userEmail').slice(0, 5);

    let localName = userName + userEmail;
    const date = new Date();
    const todayDate = date.getDate() + '-' + (date.getMonth() + 1) + '-' + date.getFullYear();

    const myObj = {
        "itemName": $('#txtItemName').val().trim(),
        "itemValue": $('#numItemVal').val(),
        "itemAction": $('input[name="chooseAction"]:checked').val(),
        'itemAddedDate': todayDate
    }

    const userExpenseData = JSON.parse(localStorage.getItem(localName) || '[]');
    userExpenseData.push(myObj);
    localStorage.setItem(localName, JSON.stringify(userExpenseData));
});