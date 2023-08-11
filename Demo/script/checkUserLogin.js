// Check User Login
function CheckUserLogin() {
    if (sessionStorage.getItem('userName') === null) {
        alert('Please Login');
        window.location.replace('http://127.0.0.1:5500/index.html');
    } else {
        document.getElementById('welcomeText').innerText = sessionStorage.getItem('userName');
    }
}

// Exporting userLogin function for outer usage.
export { CheckUserLogin };