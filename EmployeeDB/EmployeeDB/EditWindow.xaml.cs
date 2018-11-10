using System.Data;
using System.Windows;

namespace EmployeeDB
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public DataRow resultRow { get; set; }

        public EditWindow(DataRow dataRow)
        {
            InitializeComponent();
            resultRow = dataRow;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PersId.Text = resultRow["Id"].ToString();
            PeopleId.Text = resultRow["People_Id"].ToString();
            DepartmentId.Text = resultRow["Department_Id"].ToString();
            DepartmentName.Text = resultRow["Department"].ToString();
            FirstName.Text = resultRow["FirstName"].ToString();
            LastName.Text = resultRow["LastName"].ToString();
            Salary.Text = resultRow["Salary"].ToString();
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            resultRow["Id"] = PersId.Text;
            resultRow["People_Id"] = PeopleId.Text;
            resultRow["Department_Id"] = DepartmentId.Text;
            resultRow["Salary"] = Salary.Text;
            this.DialogResult = true;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
