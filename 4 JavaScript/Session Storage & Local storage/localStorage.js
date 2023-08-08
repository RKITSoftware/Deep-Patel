// Selecting Id from Html
let incrementLocalButton = document.getElementById('btnIncrementLocal');
let decrementLocalButton = document.getElementById('btnDecrementLocal');
let counterValueLocal = document.getElementById('counterValueLocal');

// localStorage.setItem('counter', 0);
// localStorage.getItem('counter');
// localStorage.removeItem('counter');

// Adding click event for increment and decrement counter value.
incrementLocalButton.addEventListener("click", IncrementValueLocal);
decrementLocalButton.addEventListener("click", DecrementValueLocal);

// If exists counter in local storage then inrement the counter value
// else create counter value with 1 in local storage.
function IncrementValueLocal() {
    if (localStorage.counter) {
        localStorage.counter = Number(localStorage.getItem('counter')) + 1;
        // localStorage.setItem('counter', Number(counterVal.innerText)); 
    } else {
        localStorage.counter = 1;
    }

    counterValueLocal.textContent = Number(localStorage.counter);
}

// If exists counter in local storage then decrement the counter value
// else create counter value with -1 in local storage.
function DecrementValueLocal() {
    if (localStorage.counter) {
        localStorage.counter = Number(localStorage.getItem('counter')) - 1;
        // localStorage.setItem('counter', Number(counterVal.innerText)); 
    } else {
        localStorage.counter = -1;
    }

    counterValueLocal.textContent = Number(localStorage.counter);
}
