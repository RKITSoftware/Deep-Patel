// Selecting Id from Html
let incrementLocalButton = document.getElementById('btnIncrementLocal');
let decrementLocalButton = document.getElementById('btnDecrementLocal');
let counterValueLocal = document.getElementById('counterValueLocal');

// Adding click event for increment and decrement counter value.
incrementLocalButton.addEventListener("click", IncrementValueLocal);
decrementLocalButton.addEventListener("click", DecrementValueLocal);

// Increment counter value on Local
function IncrementValueLocal() {
    if (localStorage.counter) {
        localStorage.counter = Number(localStorage.getItem('counter')) + 1;
        // localStorage.setItem('counter', Number(counterVal.innerText)); 
    } else {
        localStorage.counter = 1;
    }

    counterValueLocal.textContent = Number(localStorage.counter);
}

// Decrement counter value on Local
function DecrementValueLocal() {
    if (localStorage.counter) {
        localStorage.counter = Number(localStorage.getItem('counter')) - 1;
        // localStorage.setItem('counter', Number(counterVal.innerText)); 
    } else {
        localStorage.counter = -1;
    }
    counterValueLocal.textContent = Number(localStorage.counter);
}