using System;

namespace InterfaceDemo
{
    internal class PartTimeEmployee : IEmployee
    {
        #region Public Methods
        public void Show()
        {
            Console.WriteLine("This is a method of IEmployee Interface");
        }

        #endregion
    }
}
