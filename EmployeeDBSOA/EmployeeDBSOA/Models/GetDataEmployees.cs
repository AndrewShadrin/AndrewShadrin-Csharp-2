using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EmployeeDBSOA.Models
{
    public class DataEmployees
    {
        private SqlConnection sqlConnection;

        public DataEmployees()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\EmployeeDB.mdf;Integrated Security=True;Connect Timeout=30";

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

        }

        public List<Employee> GetList()
        {
            List<Employee> list = new List<Employee>();

            string sql = @"SELECT * FROM ViewEmployees";

            using (SqlCommand com = new SqlCommand(sql, sqlConnection))
            {
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var temp = new Employee();

                        temp.PersID = (int)reader["PersId"];
                        //temp.Birthday = (DateTime)reader["Birthday"];
                        temp.FirstName = reader["FirstName"].ToString();
                        temp.LastName = reader["LastName"].ToString();
                        temp.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                        temp.DepartmentName = reader["DepartmentName"].ToString();
                        try
                        {
                            temp.Salary = reader.GetInt32(reader.GetOrdinal("Salary"));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        list.Add(temp);
                    }
                }

            }

            return list;
        }

        public Employee GetEmployeeId(int Id)
        {
            string sql = $@"SELECT * FROM ViewEmployees WHERE PersId={Id}";
            Employee temp = null;
            using (SqlCommand com = new SqlCommand(sql, sqlConnection))
            {
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        temp = new Employee();

                        temp.PersID = (int)reader["PersId"];
                        //temp.Birthday = (DateTime)reader["Birthday"];
                        temp.FirstName = reader["FirstName"].ToString();
                        temp.LastName = reader["LastName"].ToString();
                        temp.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                        temp.DepartmentName = reader["DepartmentName"].ToString();
                        try
                        {
                            temp.Salary = reader.GetInt32(reader.GetOrdinal("Salary"));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }

            }
            return temp;
        }

        public bool AddEmployee(Employee employee)
        {
            try
            {
                string sqlAdd = $@" INSERT INTO Employees(FirstName, Birthday, LastName, Department, Salary)";
                //               VALUES(N'{employee.FirstName}',
                //                      N'{employee.Birthday}',
                //                      N'{employee.LastName}',
                //                      N'{employee.Department}',
                //                      N'{employee.Salary}' ) ";

                //Console.WriteLine(sqlAdd);

                using (var com = new SqlCommand(sqlAdd, sqlConnection))
                {
                    com.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}