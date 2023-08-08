// arrow functions

// odd or even
// input : 1 number
// output : true, false

const isEven = (number) => {
    // if (number % 2 === 0) {
    //     return true;
    // }

    // return false;

    return number % 2 === 0;
}

console.log(isEven(3));
console.log(isEven(4));

// input: string
// output: firstCharacter

const FirstCharacter = (anyString) => {
    return anyString[0];
}