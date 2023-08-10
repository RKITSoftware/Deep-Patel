// Check user login if user is login then it don't redirect to login page.
function CheckUserLogin() {
    if (sessionStorage.getItem('userName') === null) {
        alert('Please Login');
        window.location.replace('http://127.0.0.1:5500/index.html');
    } else {
        document.getElementById('welcomeText').innerText = sessionStorage.getItem('userName');
    }
}

// Exporting userLogin Function for outer usage.
export { CheckUserLogin };