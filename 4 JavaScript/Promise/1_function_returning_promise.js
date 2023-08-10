// function returning promise

const bucket = ['coffee', 'chips', 'vegetables', 'salt', 'rice'];

function ricePromise() {
    return new Promise((resolve, reject) => {
        if (bucket.includes('vegetables') && bucket.includes('salt') && bucket.includes('rice')) {
            resolve('Fried Rice');
        } else {
            reject(new Error('something missing from bucket'));
        }
    });
}

ricePromise().then(
    (myFriendRice) => {
        console.log("lets eat", myFriendRice);
    }
).catch((error) => {
    console.log(error);
});