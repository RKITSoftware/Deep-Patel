using System;

namespace SerializationDemo
{
    [Serializable]
    internal class Employee
    {
        #region Private Members

        private int _id;
        public string _name;

        #endregion

        #region Public Properties

        public int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        #endregion

        #region Constructors

        public Employee(int id, string name)
        {
            this._id = id;
            this._name = name;
        }

        #endregion
    }
}
