const logOutBtn = document.getElementById('btnLogOut');

logOutBtn.addEventListener('click', () => {
    sessionStorage.clear();
    window.location.replace('http://127.0.0.1:5500/index.html');
});