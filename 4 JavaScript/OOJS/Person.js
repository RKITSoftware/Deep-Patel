// Creating class of Person
class Person {

    // Initialize values when instance is created.
    constructor(firstName, lastName, age) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
    }

    // can access by Class Name
    // It don't need object to access.
    static desc = 'hello';

    static classInfo() {
        return 'this is person class';
    }

    // return full name of the person.
    get fullName() {
        return `${this.firstName} ${this.lastName}`;
    }

    // setting the firstname and lastname of the person.
    // input: fullName String
    set fullName(fullName) {
        const [firstName, lastName] = fullName.split(' ');
        this.firstName = firstName;
        this.lastName = lastName;
    }

    // Method of Person
    eat() {
        return `${this.firstName} is eating`;
    }

    isSuperCute() {
        return this.age <= 1;
    }

    isCute() {
        return true;
    }
}

// Creatinf=g Object of Person Class.
const person1 = new Person('deep', 'patel', 21);
console.log(person1.eat());
// console.log(person1.classInfo());

// static method can access by class name not objects or instance.
console.log(Person.classInfo());
console.log(Person.desc);