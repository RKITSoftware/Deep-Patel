using System.Data;

namespace LinqDataTableDemo
{
    /// <summary>
    /// Employee model using as Datatable
    /// </summary>
    public class EMP01 : DataTable
    {
        /// <summary>
        /// Initialize <see cref="EMP01"/> properties and fields.
        /// </summary>
        public EMP01()
        {
            Columns.Add("P01F01", typeof(int));
            Columns.Add("P01F02", typeof(string));
            Columns.Add("P01F03", typeof(int));
            Columns.Add("P01F04", typeof(string));
            Columns.Add("P01F05", typeof(int));
        }
    }
}
