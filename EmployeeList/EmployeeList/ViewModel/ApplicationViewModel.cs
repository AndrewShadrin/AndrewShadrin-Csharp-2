using EmployeeList.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeList
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Список подразделений
        /// </summary>
        public ObservableCollection<Department> Departments { get; set; }

        /// <summary>
        /// Список сотрудников
        /// </summary>
        public ObservableCollection<Employee> Employees { get; set; }

        private Employee _selectedEmployee;

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set { _selectedEmployee = value; OnPropertyChanged("SelectedEmployee"); }
        }

        public ApplicationViewModel()
        {
            Departments = new ObservableCollection<Department>();
            Employees = new ObservableCollection<Employee>();
            InitData();
        }

        /// <summary>
        /// Выполняет заполнение данных
        /// </summary>
        private void InitData()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                Departments.Add(new Department("Department " + (i + 1)));
            }
            for (int i = 0; i < 30; i++)
            {
                Employees.Add(new Employee($"Иван {Employee.maxID + 1}", $"Иванов  {Employee.maxID + 1}", DateTime.Now.AddYears(-rnd.Next(20, 45)), DateTime.Now, rnd.Next(20000, 80000), Departments[rnd.Next(1, 10)]));
            }
        }

        #region Описание команд
        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Employee employee = new Employee("имя", "фамилия", DateTime.MinValue, DateTime.Today, 0, null);
                      Employees.Add(employee);
                      SelectedEmployee = employee;
                  }));
            }
        }

        // команда удаления
        private RelayCommand removeCommand;

        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Employee employee = obj as Employee;
                      if (employee != null)
                      {
                          Employees.Remove(employee);
                      }
                  },
                 (obj) => Employees.Count > 0));
            }
        }
        #endregion


        #region Реализация интерфейса INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}
