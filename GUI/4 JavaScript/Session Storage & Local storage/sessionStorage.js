// Selecting Id from Html
let incrementSessionButton = document.getElementById('btnIncrementSession');
let decrementSessionButton = document.getElementById('btnDecrementSession');
let counterValueSession = document.getElementById('counterValueSession');

// Adding click event for increment and decrement counter value.
incrementSessionButton.addEventListener("click", IncrementValueSession);
decrementSessionButton.addEventListener("click", DecrementValueSession);

// Increment counter value on session storage.
function IncrementValueSession() {
    if (sessionStorage.counter) {
        sessionStorage.counter = Number(sessionStorage.getItem('counter')) + 1;
        // sessionStorage.setItem('counter', Number(counterVal.innerText)); 
    } else {
        sessionStorage.counter = 1;
    }
    counterValueSession.textContent = Number(sessionStorage.counter);
}

// Increment counter value on session storage.
function DecrementValueSession() {
    if (sessionStorage.counter) {
        sessionStorage.counter = Number(sessionStorage.getItem('counter')) - 1;
        // localStorage.setItem('counter', Number(counterVal.innerText)); 
    } else {
        sessionStorage.counter = -1;
    }
    counterValueSession.textContent = Number(sessionStorage.counter);
}