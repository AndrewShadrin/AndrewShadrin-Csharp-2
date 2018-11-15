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
using System.Windows.Shapes;

namespace SOAClient
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EmployeeDBSOA.Models.Employee employee { get; set; }

        public EditWindow(EmployeeDBSOA.Models.Employee empl)
        {
            InitializeComponent();
            employee = empl;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PersId.Text = employee.PersID.ToString();
            //PeopleId.Text = employee.People_Id.ToString();
            //DepartmentId.Text = employee.DepartmentId.ToString();
            //DepartmentName.Text = employee.DepartmentName;
            DepartmentName.Text = employee.Department.Name;
            FirstName.Text = employee.FirstName;
            LastName.Text = employee.LastName;
            Salary.Text = employee.Salary.ToString();
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            employee.PersID = Convert.ToInt32(PersId.Text);
            //employee["People_Id"] = PeopleId.Text;
            //employee.DepartmentId = Convert.ToInt32(DepartmentId.Text);
            employee.Salary = Convert.ToInt32(Salary.Text);
            this.DialogResult = true;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
