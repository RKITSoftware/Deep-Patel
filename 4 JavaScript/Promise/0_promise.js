// Promise

const bucket = ['coffee', 'chips', 'vegetables', 'salt', 'rice'];

// we know value in future.
// status : pending
// status : fulfilled

const friedRicePromise = new Promise((resolve, reject) => {
    if (bucket.includes('vegetables') && bucket.includes('salt') && bucket.includes('rice')) {
        resolve('Fried Rice');
    } else {
        reject(new Error('something missing from bucket'));
    }
});

// produce

// consume
// how to consume

// friedRicePromise.then(
//     // jab promise resolve hoga
//     (myFriendRice) => {
//         console.log("lets eat", myFriendRice);
//     },

//     // jab promise reject hoga
//     (error) => {
//         console.log(error);
//     }
// );

friedRicePromise.then((myFriedRice) => {
    console.log(myFriedRice);
}
).catch((error) => {
    console.log(error);
})

setTimeout(() => {
    console.log('hello from setTimeout')
}, 0);

// promise is browser's feature
// Promise add in microtask queue

for (let i = 0; i <= 100; i++) {
    console.log(Math.random(), i);
}

// Microtask queue >>> Callback queue