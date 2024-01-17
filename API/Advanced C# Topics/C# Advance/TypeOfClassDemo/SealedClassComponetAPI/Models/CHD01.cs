namespace SealedClassComponetAPI.Models
{
    public sealed class CHD01
    {
        #region Public Properties

        /// <summary>
        /// Children Id
        /// </summary>
        public int D01F01 { get; set; }

        /// <summary>
        /// Children Name
        /// </summary>
        public string D01F02 { get; set; }

        /// <summary>
        /// Children Age
        /// </summary>
        public int D01F03 { get; set; }

        /// <summary>
        /// Children's Parent
        /// </summary>
        public PRT01 D01F04 { get; set; }

        #endregion

        #region Constructor

        public CHD01() { }

        public CHD01(int id, string name, int age, PRT01 parent)
        {
            this.D01F01 = id;
            this.D01F02 = name;
            this.D01F03 = age;
            this.D01F04 = parent;
        }

        #endregion
    }
}