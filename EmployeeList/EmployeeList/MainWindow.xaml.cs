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
using System.Windows.Shapes;

namespace EmployeeList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Organization Company;
        public MainWindow()
        {
            InitializeComponent();
            Company = new Organization(1, "Geek university");
            ListEmployees.ItemsSource = Company.Employees;
            ListDepartments.ItemsSource = Company.Departments;
            ListEmployees.ItemTemplate = new DataTemplate();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Company.Departments.Add(new Department($"Department №{Department.MaxID+1}"));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ListDepartments.SelectedItem!=null)
            {
                Random rnd = new Random();
                Company.Employees.Add(new Employee($"Иван {Employee.maxID+1}", "Иванов", DateTime.Now.AddYears(-rnd.Next(20,45)), ListDepartments.SelectedItem as Department));

            }
            else
            {
                MessageBox.Show("Не выбрано подразделение");
            }
        }

        private void LastNameCM_Click(object sender, RoutedEventArgs e)
        {
            Company.Employees.GroupBy(et => et.LastName);
        }
    }
}
