// promise and setTimeout

// I want to resolve / reject promise after 2 seconds

function MyPromise() {
    return new Promise((resolve, reject) => {
        const value = true;
        setTimeout(() => {
            if (value) {
                resolve();
            } else {
                reject();
            }
        }, 2000);
    });
}

MyPromise()
    .then(() => {
        console.log('resolved')
    })
    .catch(() => {
        console.log('rejected')
    });