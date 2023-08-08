const URL = 'https://jsonplaceholder.typicode.com/posts';
const xhr = new XMLHttpRequest();

xhr.open('GET', URL);

// xhr.onreadystatechange = function () {
//     // console.log(xhr.readyState);
//     if (xhr.readyState === 4) {
//         console.log(xhr.status);
//         const response = xhr.response;
//         const data = JSON.parse(response);
//         console.log(data);
//     }
// }

// readyState value
// 0 - unsent
// 1 - opened
// 2 - headers_received
// 3 - loading
// 4 - done

xhr.onload = function () {
    console.log(xhr.readyState);
    const response = xhr.response;
    const data = JSON.parse(response);
    console.log(data);
}

xhr.send();