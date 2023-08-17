// async function runs after all script execute.
async function asyncExample() {
    let delhiWeather = new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve('27 deg')
        }, 4000);
    });

    // delhiWeather.then(alert);

    // await stops execution until the given Promise resolve or reject.

    console.log("fetching Delhi weather");
    let delhiW = await delhiWeather;
    console.log("completed Delhi weather");

    return [delhiW];
}

let arrayAsyncValue = asyncExample();
arrayAsyncValue.then((value) => {
    console.log(value);
});