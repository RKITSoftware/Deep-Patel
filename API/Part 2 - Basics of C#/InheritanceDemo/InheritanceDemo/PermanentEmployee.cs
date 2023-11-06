namespace InheritanceDemo
{
    // Derived Class
    internal class PermanentEmployee : Employee
    {
        #region Public Members

        public int permanentSalary;
        public int permanentHours;

        #endregion

        #region Constructors

        public PermanentEmployee()
        {

        }

        public PermanentEmployee (int empId, string empName, int empAge, int empContactNo, int permanentSalary, int permanentHours) 
            : base(empId, empName, empAge, empContactNo)
        {
            this.permanentHours = permanentHours;
            this.permanentSalary = permanentSalary;
        }

        #endregion
    }
}
