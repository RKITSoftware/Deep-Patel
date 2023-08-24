using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkSheetCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name");
            string studentName = Console.ReadLine();

            Console.WriteLine("Enter your roll number");
            string rollNo = Console.ReadLine();

            Console.WriteLine("Enter your standard");
            string standard = Console.ReadLine();

            Console.WriteLine("Enter maths name");
            int mathMarks = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter physics name");
            int physicsMarks = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter chemistry name");
            int chemistryMarks = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter english name");
            int englishMarks = int.Parse(Console.ReadLine());

            int obtainMarks = mathMarks + physicsMarks + chemistryMarks + englishMarks;
            int percentage = obtainMarks * 100 / 400;

            Console.WriteLine("Your name is : {0}", studentName);
            Console.WriteLine("Your roll number is : {0}", rollNo);
            Console.WriteLine("Your class is : {0}", standard);
            Console.WriteLine("Your obtain marks are {0} and percentage is {1}", obtainMarks, percentage);

            if(percentage >= 80)
            {
                Console.WriteLine("Grade - A");
            }
            else if(percentage >= 60)
            {
                Console.WriteLine("Grade - B");
            }
            else if(percentage >= 40)
            {
                Console.WriteLine("Grade - C");
            }
            else
            {
                Console.WriteLine("Fail");
            }

            int supply = 0;

            if(mathMarks < 33)
            {
                supply++;
                Console.WriteLine("You have failed in math");
            }

            if(physicsMarks < 33)
            {
                supply++;
                Console.WriteLine("You have failed in physics");
            }

            if(chemistryMarks < 33)
            {
                supply++;
                Console.WriteLine("You have failed in chemistry");
            }

            if(englishMarks < 33)
            {
                supply++;
                Console.WriteLine("You have failed in english");
            }
        }
    }
}
