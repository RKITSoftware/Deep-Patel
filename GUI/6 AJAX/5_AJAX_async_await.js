// async await
const URL = 'https://jsonplaceholder.typicode.com/posts';

// fetch(URL)
//     .then(res => res.json())
//     .then(data => {
//         console.log(data);
//     });

console.log('script start');

// async function getPost() {
//     const response = await fetch(URL);
//     if (!response.ok) {
//         throw new Error('Someting went wrong.');
//     }
//     const data = await response.json();
//     return data;
// }

const getPost = async () => {
    const response = await fetch(URL);
    if (!response.ok) {
        throw new Error('Someting went wrong.');
    }
    const data = await response.json();
    return data;
}

// const myData = getPost();
// console.log(myData);
// console.log(returned);

getPost()
    .then(myData => {
        console.log(myData);
    })
    .catch(error => {
        console.log(error);
    });

console.log('script end');