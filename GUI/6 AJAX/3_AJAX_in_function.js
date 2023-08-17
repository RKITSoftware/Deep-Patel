const URL = 'https://jsonplaceholder.typicode.com/posts';

function sendRequest(method, url) {
    return new Promise((resolve, reject) => {
        const xhr = new XMLHttpRequest();
        xhr.open(method, url);

        xhr.onload = function () {
            if (xhr.status >= 200 && xhr.status < 300) {
                resolve(xhr.response);
            } else {
                reject(new Error("Invalid Resonse."));
            }
        }

        xhr.onerror = () => {
            reject('Network error');
        }

        xhr.send();
    });
}

sendRequest('GET', URL)
    .then(response => {
        const data = JSON.parse(response);
        return data;
    })
    .then(data => {
        // console.log(data[3].id);
        return data[3].id;
    })
    .then(id => {
        const url = `${URL}/${id}`;
        console.log(url);
        return sendRequest("GET", url);
    })
    .then(newResponse => {
        const newData = JSON.parse(newResponse);
        console.log(newData);
    })
    .catch(error => {
        console.log(error);
    })