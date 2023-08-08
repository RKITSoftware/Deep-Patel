// async function runs after all script execute.
async function asyncExample() {
    let delhiWeather = new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve('27 deg')
        }, 4000);
    });

    let mumbaiWeather = new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve('21 deg')
        }, 4000);
    });

    // delhiWeather.then(alert);
    // mumbaiWeather.then(alert);

    // await stops execution until the given Promise resolve or reject.

    console.log("fetching Delhi weather");
    let delhiW = await delhiWeather;
    console.log("completed Delhi weather");

    console.log("fetching mumbai weather");
    let mumbaiW = await mumbaiWeather;
    console.log("completed mumbai weather");

    return [delhiW, mumbaiW];
}

let arrayAsyncValue = asyncExample();
arrayAsyncValue.then((value) => {
    console.log(value);
});