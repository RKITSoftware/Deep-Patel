import { CheckUserLogin } from "./checkUserLogin.js";

// Checking user is logged or not.
CheckUserLogin();

// Taking form id.
const addExpenseForm = document.getElementById('addExpenseForm');

// when submit the form it will create it's local Storag name for storing the 
// data in local storage.
addExpenseForm.addEventListener('submit', () => {

    // Getting the userName and userEmail from login
    let userName = sessionStorage.getItem('userName').split(' ')[0];
    let userEmail = sessionStorage.getItem('userEmail').slice(0, 5);

    // Created local name for local storage.
    let localName = userName + userEmail;

    // Today Date.
    const date = new Date();
    const todayDate = date.getDate() + '-' + (date.getMonth() + 1) + '-' + date.getFullYear();

    // myObj stores form input data.
    const myObj = {
        "itemName": $('#txtItemName').val().trim(),
        "itemValue": $('#numItemVal').val(),
        "itemAction": $('input[name="chooseAction"]:checked').val(),
        'itemAddedDate': todayDate
    }

    // Getting localName from storage if exist.
    // if it not the create a localStorage for it.
    // and Adding the myObj data to localStorage.
    const userExpenseData = JSON.parse(localStorage.getItem(localName) || '[]');
    userExpenseData.push(myObj);
    localStorage.setItem(localName, JSON.stringify(userExpenseData));
});