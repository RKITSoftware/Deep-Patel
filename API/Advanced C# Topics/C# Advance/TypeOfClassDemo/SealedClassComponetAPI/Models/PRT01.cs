namespace SealedClassComponetAPI.Models
{
    public class PRT01
    {
        #region Public Properties

        /// <summary>
        /// Parent Id
        /// </summary>
        public int T01F01 { get; set; }

        /// <summary>
        /// Parent Name
        /// </summary>
        public string T01F02 { get; set; }

        /// <summary>
        /// Parent Age
        /// </summary>
        public int T01F03 { get; set; }

        #endregion

        #region Constructor

        public PRT01() { }

        public PRT01(int id, string name, int age)
        {
            this.T01F01 = id;
            this.T01F02 = name;
            this.T01F03 = age;
        }

        #endregion
    }
}