// Taking btnLogOut from form
const logOutBtn = document.getElementById('btnLogOut');

// Logout user from site.
logOutBtn.addEventListener('click', () => {
    sessionStorage.clear();
    window.location.replace(`${window.location.origin}/index.html`);
});