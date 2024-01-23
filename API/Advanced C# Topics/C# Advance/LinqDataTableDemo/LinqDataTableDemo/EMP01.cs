using System.Data;

namespace LinqDataTableDemo
{
    public class EMP01 : DataTable
    {
        public EMP01()
        {
            this.Columns.Add("P01F01", typeof(int));
            this.Columns.Add("P01F02", typeof(string));
            this.Columns.Add("P01F03", typeof(int));
            this.Columns.Add("P01F04", typeof(string));
            this.Columns.Add("P01F05", typeof(int));
        }
    }
}
