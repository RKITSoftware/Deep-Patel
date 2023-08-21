const demoDiv = document.getElementById('demoDiv');

function writeHelloWorld() {
    console.log('hello');
}

$('#demoDiv').click(function () {
    console.log('using jquery.');
});

demoDiv.addEventListener('click', () => {
    console.log('using add event listener.');
});

demoDiv.onclick = () => {
    console.log('using element.event');
}