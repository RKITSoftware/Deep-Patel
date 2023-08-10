// Promise.resolve
// Promise Chaining

// const MyPromise = Promise.resolve(5);
// Promise.resolve(5).then(value => {
//     console.log(value);
// });

// then()
// then method always return Promise

function MyPromise() {
    return new Promise((resolve, reject) => {
        resolve('foo');
    });
}

MyPromise().then(value => {
    console.log(value);
    value += 'bar';
    // return promise

    // Internal 
    // return Promise.resolve(value);

    // if you don't return anything then it will return undefined

    return value;
}).then(value => {
    console.log(value);
})