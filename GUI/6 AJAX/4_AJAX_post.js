// fetch

const URL = 'https://jsonplaceholder.typicode.com/posts';

fetch(URL, {
    method: 'POST',
    body: JSON.stringify([{
        title: 'patel',
        body: 'bar',
        userId: 1,
    },
    {
        title: 'patel2',
        body: 'bar2',
        userId: 2,
    }]),

    headers: {
        'Content-type': 'application/json; charset=UTF-8',
    },
}).then(response => {
    if (response.ok) {
        return response.json();
    } else {
        throw new Error('Something went wrong.');
    }
}).then(data => {
    console.log(data);
}).catch(error => {
    console.log('catch block');
    console.log(error);
})