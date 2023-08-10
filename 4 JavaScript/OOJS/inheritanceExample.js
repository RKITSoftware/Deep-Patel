// same method in subclass

class Animal {
    constructor(name, age) {
        this.name = name;
        this.age = age;
        this.gender = 'male';
    }

    eat() {
        return `${this.name} is eating`;
    }

    isSuperCute() {
        return this.age <= 1;
    }

    isCute() {
        return true;
    }
}

class Dog extends Animal {
    constructor(name, age, speed) {
        super(name, age);
        this.speed = speed;
    }

    eat() {
        console.log(this.gender);
        return `modified eat : ${this.name} is eating`;
    }

    run() {
        return `${this.name} is running at ${this.speed}kmph`;
    }
}

const tommy = new Dog('puppy', 10, 20);
console.log(tommy);
console.log(tommy.run());
console.log(tommy.eat());