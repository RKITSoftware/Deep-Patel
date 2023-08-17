// Check User Login
function CheckUserLogin() {
    if (sessionStorage.getItem('userName') === null) {
        alert('Please Login');
        window.location.replace(`${window.location.origin}/index.html`);
    } else {
        document.getElementById('welcomeText').innerText = sessionStorage.getItem('userName');
    }
}

// Exporting userLogin function for outer usage.
export { CheckUserLogin };