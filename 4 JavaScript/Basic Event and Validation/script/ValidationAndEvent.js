let txtFirstName = document.getElementById('txtFirstName');
let txtLastName = document.getElementById('txtLastName');
let formHeading = document.getElementById('formHeading');
let registrationForm = document.getElementById('registrationForm');

window.addEventListener('offline', OnOffline);
window.addEventListener("beforeprint", BeforePrint);
window.addEventListener("afterprint", AfterPrint);
formHeading.addEventListener("mouseover", MouseOver);
formHeading.addEventListener("mouseout", MouseOut);
formHeading.addEventListener("click", OnClick);
formHeading.addEventListener("dblclick", OnDoubleClick);

function OnOffline() {
    alert('Please make sure your internet connection is established.');
}

function AfterPrint() {
    alert('You printed the page.');
}

function BeforePrint() {
    alert('You are going to print the page.');
}

function MouseOver() {
    formHeading.style.color = "blue";
}

function MouseOut() {
    formHeading.style.color = "red";
}

function OnClick() {
    formHeading.style.fontSize = "10px";
}

function OnDoubleClick() {
    formHeading.style.fontSize = "40px";
}

registrationForm.addEventListener('submit', () => {
    let nameValidationPattern = "^[A-Za-z]{1,50}$";
    let firstNameValidateReg = new RegExp(nameValidationPattern);
    let lastNameValidateReg = new RegExp(nameValidationPattern);

    if (!firstNameValidateReg.test(txtFirstName.value)) {
        alert('First Name is Incorrect.');
    } else if (!lastNameValidateReg.test(txtLastName.value)) {
        alert('Last name is Incorrect.');
    } else {
        alert('All Details Are Correct.');
    }
});

// add txt for text
// btn for button
// sel for selection