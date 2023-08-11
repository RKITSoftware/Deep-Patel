// Taking btnLogOut from form
const logOutBtn = document.getElementById('btnLogOut');

// Logout user from site.
logOutBtn.addEventListener('click', () => {
    sessionStorage.clear();
    window.location.replace('http://127.0.0.1:5500/index.html');
});