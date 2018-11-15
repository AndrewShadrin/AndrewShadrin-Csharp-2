using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using Newtonsoft.Json;

namespace SOAClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:64143/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void loadAllButton_Click(object sender, RoutedEventArgs e)
        {
            List<EmployeeDBSOA.Models.Employee> employees = await GetEmployeesAsync(client.BaseAddress + "api/employees");
            EmployeeDataGrid.ItemsSource = employees;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDBSOA.Models.Employee employee = (EmployeeDBSOA.Models.Employee)EmployeeDataGrid.SelectedItem;

            EditWindow editWindow = new EditWindow(employee);
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                loadAllButton_Click(loadAllButton,null);
            }
            else
            {
            }
        }

        /// <summary>
        /// Производит чтение всех сотрудников из сервиса
        /// </summary>
        /// <param name="path">строка для контроллера web-api вида 'api/employees'</param>
        /// <returns>Возвращает список сотрудников</returns>
        static async Task<List<EmployeeDBSOA.Models.Employee>> GetEmployeesAsync(string path)
        {
            List<EmployeeDBSOA.Models.Employee> employees = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    employees = await response.Content.ReadAsAsync<List<EmployeeDBSOA.Models.Employee>>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return employees;
        }

        /// <summary>
        /// Производит чтение 1 сотрудника из сервиса
        /// </summary>
        /// <param name="path">строка для контроллера web-api вида 'api/employees/{Id}'</param>
        /// <returns>Возвращает сотрудника</returns>
        static async Task<EmployeeDBSOA.Models.Employee> GetEmployeeAsync(string path)
        {
            EmployeeDBSOA.Models.Employee employee = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    employee = await response.Content.ReadAsAsync<EmployeeDBSOA.Models.Employee>();
                }
            }
            catch (Exception)
            {
            }
            return employee;
        }

    }
}
