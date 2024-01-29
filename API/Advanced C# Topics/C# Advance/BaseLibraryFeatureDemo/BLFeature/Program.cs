using System;

namespace BLFeature
{
    public class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Override GetHashCode method
        public override int GetHashCode()
        {
            // Custom logic to generate a hash code based on the properties of the object
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + (Name != null ? Name.GetHashCode() : 0);
                return hash;
            }
        }

        // Override Equals method
        public override bool Equals(object obj)
        {
            if (obj is MyClass other)
            {
                return Id == other.Id && Name == other.Name;
            }
            return false;
        }
    }

    public class Program
    {
        static void Main()
        {
            MyClass obj1 = new MyClass { Id = 1, Name = "John" };
            MyClass obj2 = new MyClass { Id = 2, Name = "John" };

            // Check equality using overridden Equals method
            bool areEqual = obj1.Equals(obj2);
            Console.WriteLine($"Objects are equal: {areEqual}");

            // Use overridden GetHashCode method
            int hashCode1 = obj1.GetHashCode();
            int hashCode2 = obj2.GetHashCode();

            Console.WriteLine($"HashCode of obj1: {hashCode1}");
            Console.WriteLine($"HashCode of obj2: {hashCode2}");

            // Use the hash codes in your application as needed
        }
    }

}
