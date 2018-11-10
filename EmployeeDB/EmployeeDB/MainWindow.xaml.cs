using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Data.SqlClient;
using System.Data;

namespace EmployeeDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable dt;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionstring = Properties.Settings.Default.EmployeeDBConnectionString;
            connection = new SqlConnection(connectionstring);
            adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT " +
                "    Employees.Id as Id," +
                "    Employees.Salary as Salary," +
                "    Employees.People_Id as People_Id," +
                "    Department_Id as Department_Id," +
                "    Departments.Name as Department," +
                "    Peoples.FirstName as FirstName," +
                "    Peoples.LastName as LastName" +
                "    FROM Employees" +
                "    inner join Departments on Employees.Department_Id = Departments.Id" +
                "    inner join Peoples on Employees.People_Id = Peoples.Id", connection); 
                //SELECT Id, People_Id, Department_Id, Salary FROM Employees", connection);
            adapter.SelectCommand = command;

            //insert
            command = new SqlCommand(@"INSERT INTO Employees (People_Id, Department_Id, Salary) 
                          VALUES (@ID, @People_ID, @Department_ID, @Salary); SET @ID = @@IDENTITY;",
                          connection);

            command.Parameters.Add("@People_ID", SqlDbType.Int, 0,"People_ID");
            command.Parameters.Add("@Department_ID", SqlDbType.Int, 0, "Department_ID");
            command.Parameters.Add("@Salary", SqlDbType.Int, 0, "Salary");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            adapter.InsertCommand = command;

            // update
            command = new SqlCommand(@"UPDATE Employees SET People_ID = @People_ID, Department_ID = @Department_ID, Salary = @Salary WHERE ID = @ID", connection);

            command.Parameters.Add("@People_ID", SqlDbType.Int, 0, "People_ID");
            command.Parameters.Add("@Department_ID", SqlDbType.Int, 0, "Department_ID");
            command.Parameters.Add("@Salary", SqlDbType.Int, 0, "Salary");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapter.UpdateCommand = command;
            
            //delete
            command = new SqlCommand("DELETE FROM Employees WHERE ID = @ID", connection);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapter.DeleteCommand = command;

            dt = new DataTable();

            adapter.Fill(dt);
            EmployeeDataGrid.DataContext = dt.DefaultView;

        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // добавим новую строку
            DataRow newRow = dt.NewRow();
            EditWindow editWindow = new EditWindow(newRow);
            editWindow.ShowDialog();

            if (editWindow.DialogResult.Value)
            {
                dt.Rows.Add(editWindow.resultRow);
                adapter.Update(dt);
            }
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)EmployeeDataGrid.SelectedItem;
            newRow.BeginEdit();

            EditWindow editWindow = new EditWindow(newRow.Row);
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                newRow.EndEdit();
                adapter.Update(dt);
            }
            else
            {
                newRow.CancelEdit();
            }
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)EmployeeDataGrid.SelectedItem;

            newRow.Row.Delete();
            adapter.Update(dt);
        }
    }
}
