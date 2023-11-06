namespace PartialClassDemo
{
    public partial class StudentPartial
    {
        #region Public Methods

        public string GetCompleteName()
        {
            return this.FirstName + " " + this.LastName;
        }

        #endregion
    }
}
