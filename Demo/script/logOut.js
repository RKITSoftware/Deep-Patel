const logOutBtn = document.getElementById('btnLogOut');

// when click on logout button it will delete login information of user and
// go back to login page.
logOutBtn.addEventListener('click', () => {
    sessionStorage.clear();
    window.location.replace('http://127.0.0.1:5500/index.html');
});