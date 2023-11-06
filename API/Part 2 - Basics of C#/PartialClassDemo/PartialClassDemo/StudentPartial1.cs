namespace PartialClassDemo
{
    public partial class StudentPartial
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
    }
}
