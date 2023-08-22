const URL = 'https://jsonplaceholder.typicode.com/posts';

fetch(`${URL}/1`, {
    method: 'DELETE',
}).then(response => response.json())
    .then(data => console.log(data))
    .catch(error => console.log(error));