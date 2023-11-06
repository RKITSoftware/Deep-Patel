namespace PartialClassDemo
{
    public class Student
    {
        #region Private Members

        private string _firstName;
        private string _lastName;

        #endregion

        #region Public Properties

        public string FirstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                this._firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                this._lastName = value;
            }
        }

        #endregion

        #region Public Methods

        public string GetCompleteName()
        {
            return this.FirstName + " " + this.LastName;
        }

        #endregion
    }
}
