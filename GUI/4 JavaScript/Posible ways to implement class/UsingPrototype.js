const Methods = {
    about: function () {
        return `${this.firstName} is ${this.age} years`;
    },

    is18: function () {
        return this.age >= 18;
    },

    sing: function () {
        return 'sa re ga ma pa';
    }
};

function CreateUser(firstName, lastName, email, age, addres) {
    // proto gives reference to Methods object

    // bind user object with Methods object.
    // const user = Object.create(Methods);

    // creating object with Object class and bind with the function prototype.
    const user = Object.create(CreateUser.prototype);
    // const user = {};

    user.firstName = firstName;
    user.lastName = lastName;
    user.email = email;
    user.age = age;
    user.addres = addres;

    return user;
}

CreateUser.prototype.about = function () {
    return `${this.firstName} is ${this.age} years`;
}

CreateUser.prototype.is18 = function () {
    return this.age >= 18;
}

CreateUser.prototype.sing = function () {
    return 'sa re ga ma pa';
}

console.log(CreateUser.prototype);
const user1 = CreateUser('Deep', 'Patel', 'deep@gmail.com', 21, 'myAddress');
const user2 = CreateUser('harshit', 'vashist', 'vashist@gmail.com', 25, 'myAddress');
const user3 = CreateUser('vishal', 'gohil', 'vishal@gmail.com', 35, 'myAddress');

console.log(user2.about());
console.log(user2.sing());