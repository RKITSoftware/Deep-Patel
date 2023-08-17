let txtUserName = document.getElementById('txtUserName');
let txtUserPassword = document.getElementById('txtUserPassword');

// When page loads at that time it will ask username and password value and set cookie.
// if cookie exist then it will overwrite it.
window.addEventListener('load', () => {
    const userName = prompt('Enter username');
    const password = prompt('Enter password');

    document.cookie = `${userName}=${password}`;
});

// when input element focusout at that time it will check whether
// it have coookie name or not in Storage.
txtUserName.addEventListener('focusout', () => {
    const txtUserNameValue = txtUserName.value;
    const username = document.cookie.split(';').find(cookie => cookie.includes(txtUserNameValue)).split('=')[1];
    txtUserPassword.value = username;
})