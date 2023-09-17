using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTableDemo
{
    internal class Program
    {
        /// <summary>
        /// Create a Employee Table Using DataTable
        /// </summary>
        private static void EmployeeTableExample()
        {
            DataTable employee = new DataTable();

            employee.Columns.Add("ID");
            employee.Columns.Add("Name");
            employee.Columns.Add("Salary");

            Console.WriteLine("Enter Id, Name, and Salary Seperated by Commas.");

            for (int i = 0; i < 5; i++)
            {
                DataRow row = employee.NewRow();

                string data = Console.ReadLine();
                string[] values = data.Split(',');

                row["ID"] = Convert.ToInt32(values[0]);
                row["Name"] = values[1];
                row["Salary"] = Convert.ToInt32(values[2]);

                employee.Rows.Add(row);
            }

            foreach (DataRow row in employee.Rows)
            {
                Console.WriteLine($"{row["ID"]} {row["Name"]} {row["Salary"]}");
            }
        }

        /// <summary>
        /// Creating a Order Table
        /// </summary>
        /// <returns>Order Table</returns>
        private static DataTable CreateOrderTable()
        {
            DataTable orderTable = new DataTable(tableName: "Order");

            // Define Column
            DataColumn colId = new DataColumn(columnName: "OrderId", dataType: typeof(string));
            DataColumn colDate = new DataColumn(columnName: "OrderDate", dataType: typeof(DateTime));

            // Adding column to the table
            orderTable.Columns.Add(column: colId);
            orderTable.Columns.Add(column: colDate);

            // Setting Primary Key
            orderTable.PrimaryKey = new DataColumn[] { colId };

            return orderTable;
        }

        /// <summary>
        /// Create a Order Detail Table
        /// </summary>
        /// <returns>Order Detail Table</returns>
        private static DataTable CreateOrderDetailTable()
        {
            DataTable orderDetailTable = new DataTable("OrderDetail");

            // Define all the columns once
            DataColumn[] cols =
            {
                new DataColumn(columnName: "OrderDetailId",dataType: typeof(Int32)),
                new DataColumn(columnName: "OrderId",dataType: typeof(String)),
                new DataColumn(columnName: "Product",dataType: typeof(String)),
                new DataColumn(columnName: "UnitPrice",dataType: typeof(Decimal)),
                new DataColumn(columnName: "OrderQty",dataType: typeof(Int32)),
                new DataColumn(columnName: "LineTotal",dataType: typeof(Decimal), expr: "UnitPrice * OrderQty")
            };

            // Add Columns to the table
            orderDetailTable.Columns.AddRange(cols);

            // Setting Primary Key
            orderDetailTable.PrimaryKey = new DataColumn[] { orderDetailTable.Columns["OrderDetailId"] };

            return orderDetailTable;
        }

        /// <summary>
        /// Adding Rows to the orderTable
        /// </summary>
        /// <param name="orderTable">Order Table</param>
        private static void InsertOrders(DataTable orderTable)
        {
            // Adding Rows one by one
            DataRow row1 = orderTable.NewRow();
            row1["OrderId"] = "O0001";
            row1["OrderDate"] = new DateTime(year: 2013, month: 3, day: 1);
            orderTable.Rows.Add(row: row1);

            DataRow row2 = orderTable.NewRow();
            row2["OrderId"] = "O0002";
            row2["OrderDate"] = new DateTime(year: 2013, month: 3, day: 12);
            orderTable.Rows.Add(row: row2);

            DataRow row3 = orderTable.NewRow();
            row3["OrderId"] = "O0003";
            row3["OrderDate"] = new DateTime(year: 2013, month: 3, day: 20);
            orderTable.Rows.Add(row: row3);
        }

        /// <summary>
        /// Inserts rows to the Order Detail Table
        /// </summary>
        /// <param name="orderDetailTable">Order Deatil Table</param>
        private static void InsertOrderDetails(DataTable orderDetailTable)
        {
            // Using an Object array to insert all items.
            Object[] rows =
            {
                new Object[] {1, "O0001", "Mountain Bike", 1419.5, 36},
                new Object[] {2, "O0001", "Road Bike", 1233.6, 16},
                new Object[] {3, "O0001", "Touring Bike", 1653.3, 32},
                new Object[] {4, "O0002", "Mountain Bike", 1419.5, 24},
                new Object[] {5, "O0002", "Road Bike", 1233.6, 12},
                new Object[] {6, "O0003", "Mountain Bike", 1419.5, 48},
                new Object[] {7, "O0003", "Touring Bike", 1653.3, 8},
            };

            foreach (Object[] row in rows)
            {
                orderDetailTable.Rows.Add(row);
            }
        }

        /// <summary>
        /// Prints the Table
        /// </summary>
        /// <param name="table">Table</param>
        private static void ShowTable(DataTable table)
        {
            foreach (DataColumn col in table.Columns)
            {
                Console.Write("{0,-14}", col.ColumnName);
            }
            Console.WriteLine();

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    if (col.DataType.Equals(typeof(DateTime)))
                    {
                        Console.Write("{0,-14:d}", row[col]);
                    }
                    else if (col.DataType.Equals(typeof(Decimal)))
                    {
                        Console.Write("{0,-14:C}", row[col]);
                    }
                    else
                    {
                        Console.Write("{0,-14}", row[col]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Create a Order Table Example Using DataTable and DataSet
        /// </summary>
        public static void OrderTableExample()
        {
            DataTable orderTable = CreateOrderTable();
            DataTable orderDetailTable = CreateOrderDetailTable();
            DataSet salesSet = new DataSet();
            salesSet.Tables.Add(orderTable);
            salesSet.Tables.Add(orderDetailTable);

            // Adding Relations Between them.
            salesSet.Relations.Add(name: "OrderOrderDetail", parentColumn: orderTable.Columns["OrderId"],
                childColumn: orderDetailTable.Columns["OrderId"], createConstraints: true);

            Console.WriteLine("After Creating Foreign key constraint, you will see " +
                "the following type of error if inserting order detail with the wrong OrderId: ");
            try
            {
                DataRow errorRow = orderDetailTable.NewRow();
                errorRow[0] = 1;
                errorRow[1] = "O0007";
                orderDetailTable.Rows.Add(errorRow);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();

            // Insert the Rows into the table
            InsertOrders(orderTable);
            InsertOrderDetails(orderDetailTable);

            Console.WriteLine("The initail Order table.");
            ShowTable(orderTable);

            Console.WriteLine("The OrderDetail table.");
            ShowTable(orderDetailTable);

            // Use the aggregate sum on the child table column to get the result.
            DataColumn colSub = new DataColumn(columnName: "SubTotal", dataType: typeof(Decimal), expr: "Sum(Child.LineTotal)");
            orderTable.Columns.Add(colSub);

            // Compute the tax by referencing the SubTotal expression column.
            DataColumn colTax = new DataColumn(columnName: "Tax", dataType: typeof(Decimal), expr: "SubTotal * 0.1");
            orderTable.Columns.Add(colTax);

            // If the OrderId is 'Total', compute the due on all orders; or compute the due on this order.
            DataColumn colTotal = new DataColumn(columnName: "TotalDue", dataType: typeof(Decimal), expr: "IIF(OrderId='Total', Sum(SubTotal)+Sum(Tax), SubTotal+Tax)");
            orderTable.Columns.Add(colTotal);

            DataRow row = orderTable.NewRow();
            row["OrderId"] = "Total";
            orderTable.Rows.Add(row);

            Console.WriteLine("The Order table with expression columns.");
            ShowTable(orderTable);
        }

        static void Main(string[] args)
        {
            // EmployeeTableExample();
            OrderTableExample();
        }
    }
}
