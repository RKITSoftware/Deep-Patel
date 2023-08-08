// function (that function create object)
// 2.) add key value pair
// 3.) return object

function CreateUser(firstName, lastName, email, age, addres) {
    const user = {};

    user.firstName = firstName;
    user.lastName = lastName;
    user.email = email;
    user.age = age;
    user.addres = addres;
    user.about = function () {
        return `${this.firstName} is ${this.age} years`;
    };
    user.is18 = function () {
        return this.age >= 18;
    };

    return user;
}

const user1 = CreateUser('harshit', 'vashist', 'vashist@gmail.com', 5, 'myAddress');
console.log(user1);

const is18 = user1.is18();
console.log(is18);

const about = user1.about();
console.log(about);