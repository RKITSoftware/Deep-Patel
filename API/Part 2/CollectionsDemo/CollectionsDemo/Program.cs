using System;
using System.Collections;
using System.Collections.Generic;

namespace CollectionsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            int[] myArray = new int[3] {10, 20, 30};
            
            // Changing the array size using Array Class Resize Method
            Array.Resize(ref myArray, 4);

            myArray[3] = 40;
            Console.WriteLine(myArray[3]);
            */



            // ArrayList
            ArrayList myList = new ArrayList();

            // Default Capacity
            // ArrayList myList = new ArrayList(10);

            Console.WriteLine("Before adding any element to list :- {0}.", myList.Capacity);
            myList.Add(10);
            myList.Add("Deep");
            myList.Add(21);

            // 0 4 8 16 32 -- Double each time
            Console.WriteLine("After adding 3 element to list :- {0}.", myList.Capacity);

            // Printing the myList
            foreach (object obj in myList)
            {
                Console.Write(obj + " ");
            }
            Console.WriteLine();

            // Inserting element at 2nd index
            myList.Insert(2, "Patel");

            // Printing the myList
            foreach (object obj in myList)
            {
                Console.Write(obj + " ");
            }
            Console.WriteLine();

            // Removing Deep Patel from myList.
            myList.Remove("Patel");
            myList.RemoveAt(1);

            // Printing the myList
            foreach (object obj in myList)
            {
                Console.Write(obj + " ");
            }
            Console.WriteLine();



            // Stack
            Stack myStack = new Stack();

            // Pushing elements to the myStack
            myStack.Push(1);
            myStack.Push(2);
            myStack.Push(2);
            myStack.Push(25);

            // myStack Size
            Console.WriteLine("Stack Size is :- {0}\n", myStack.Count);

            // Printing the stack elements
            foreach (object element in myStack)
            {
                Console.WriteLine("Element is :- {0}", element);
            }

            // Popping the element
            Console.WriteLine("\nPopped elements is :- {0}", myStack.Pop());

            // Peek element of stack
            Console.WriteLine("\nPeek element is :- {0}", myStack.Peek());

            // Clearing stack elements.
            myStack.Clear();
            Console.WriteLine("After clearing the stack element Size is :- {0}\n", myStack.Count);



            // Queue
            Queue myQueue = new Queue();

            // Adding elements into queue
            myQueue.Enqueue(10);
            myQueue.Enqueue(20);
            myQueue.Enqueue(30);
            myQueue.Enqueue(40);

            // Printing myQueue Elements
            foreach (object element in myQueue)
            {
                Console.WriteLine("Queue Element is :- {0}", element);
            }

            // Count the elements of the myQueue
            Console.WriteLine("\nThe size of myQueue is :- {0}", myQueue.Count);

            // Removes and returns the first item of myQueue
            Console.WriteLine("\nRemoved item {0}", myQueue.Dequeue());

            // Returns peek element
            Console.WriteLine("Peek element :- {0}", myQueue.Peek());

            // clear queue elements
            myQueue.Clear();

            // Printing myQueue Elements
            foreach (object element in myQueue)
            {
                Console.WriteLine("Queue Element is :- {0}", element);
            }


            //Hash-Table
            Hashtable myHT = new Hashtable
            {
                { "Id", 1 },
                { "Name", "Deep Patel" },
                { "Salary", 50000.00f },
                { "Designation", "Full Stack Developer" }
            };

            // add item in hashtable
            myHT.Add("isMarried", false);

            Console.WriteLine(myHT["Id"]);
            Console.WriteLine(myHT["Salary"]);
            Console.WriteLine();

            // remove item from hashtable
            myHT.Remove("Designation");

            // Print hashtable
            foreach (object key in myHT.Keys)
            {
                Console.WriteLine("{0} : {1}", key, myHT[key]);
            }

            // Contains
            Console.WriteLine(myHT.ContainsKey("Name"));
            Console.WriteLine(myHT.ContainsValue("Deep Patel"));

            // GetHashCode
            Console.WriteLine("Hashcode of name is :- {0}", "Name".GetHashCode());


            // Generic Collection


            // List
            List<int> myNumbers = new List<int>
            {
                1,
                4,
                7
            };

            myNumbers.Add(5);

            List<int> myList2 = new List<int>()
            {
                6, 5, 3
            };

            // Insert
            myNumbers.Insert(3, 50);
            myNumbers.InsertRange(4, myList2);

            // Remove
            myNumbers.Remove(50);
            myNumbers.RemoveAt(1);
            myNumbers.RemoveRange(3, 2);
            myNumbers.RemoveAll(item => item < 5);

            // AddRange
            myNumbers.AddRange(myList2);

            // Capacity of list
            Console.WriteLine("Capacity of list {0}", myNumbers.Capacity);

            // Sort the list
            myNumbers.Sort();

            // Contains
            Console.WriteLine("44 contains :- {0}", myNumbers.Contains(44));

            // Printing the list
            foreach (int item in myNumbers)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            // Reverse the list
            myNumbers.Reverse();

            // Printing the list
            foreach (int item in myNumbers)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            // IndexOf
            Console.WriteLine("Index of 3 is :- {0}", myNumbers.IndexOf(3));

            // Find
            Console.WriteLine("Find  :- {0}", myNumbers.Find(ele => ele == 10));


            // Dictionary
            Dictionary<string, string> myDict = new Dictionary<string, string>();
            myDict.Add(key: "username", value: "Deep Patel");
            myDict.Add(key: "password", value: "Deep2513");

            Console.WriteLine(myDict[key: "username"]);

            //foreach (KeyValuePair<string, string> item in myDict)
            //{
            //    Console.WriteLine($"Key is {item.Key} and value is {item.Value}.");
            //}

            //foreach(string key in myDict.Keys)
            //{
            //    Console.WriteLine(key);
            //}

            foreach (var value in myDict.Values)
            {
                Console.WriteLine(value);
            }

            string val;
            myDict.TryGetValue("username", out val);
            Console.WriteLine(val);
        }
    }
}
