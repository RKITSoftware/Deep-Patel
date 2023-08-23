import { CheckUserLogin } from "./checkUserLogin.js";

// Checking user login
CheckUserLogin();

// Taking form id
const addExpenseForm = document.getElementById('addExpenseForm');

// Add expenseData to local storage
addExpenseForm.addEventListener('submit', () => {

    // Getting the userName and userEmail from session storgage.
    let userName = sessionStorage.getItem('userName').split(' ')[0];
    let userEmail = sessionStorage.getItem('userEmail').slice(0, 5);

    // Created local name for local storage.
    let localName = userName + userEmail;

    // Creating today date in mm/dd/yyyy format.
    const date = new Date();
    const todayDate = date.toLocaleDateString();

    // myObj object stores form input data.
    const myObj = {
        "itemName": $('#txtItemName').val().trim(),
        "itemValue": $('#numItemVal').val(),
        "itemAction": $('input[name="chooseAction"]:checked').val(),
        'itemAddedDate': todayDate
    }

    // Getting userExpenseData and adding new data
    const userExpenseData = JSON.parse(localStorage.getItem(localName) || '[]');
    userExpenseData.push(myObj);
    localStorage.setItem(localName, JSON.stringify(userExpenseData));
});