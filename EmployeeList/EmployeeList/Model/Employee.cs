using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeList
{
    /// <summary>
    /// Предоставляет описание сотрудника организации
    /// </summary>
    public class Employee : INotifyPropertyChanged
    {
        #region Поля класса

        /// <summary>
        /// Хранилище максимального табельного номера
        /// </summary>
        public static int maxID = 0;

        /// <summary>
        /// Имя
        /// </summary>
        private string firstName;
        
        /// <summary>
        /// Фамилия
        /// </summary>
        private string lastName;
        
        /// <summary>
        /// Оклад
        /// </summary>
        private int salary;
        
        /// <summary>
        /// Подразделение
        /// </summary>
        private Department department;
        
        /// <summary>
        /// День рождения
        /// </summary>
        private DateTime _birthday;
        
        /// <summary>
        /// Адрес
        /// </summary>
        private string _adress;
        
        /// <summary>
        /// Паспорт
        /// </summary>
        private string _passport;
        
        /// <summary>
        /// Дата приема
        /// </summary>
        private DateTime _dateOfEmployment;

        #endregion

        #region Свойства класса

        /// <summary>
        /// Табельный номер
        /// </summary>
        public int PersID { get; }

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged("LastName"); }
        }

        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime Birthday { get => _birthday; set { _birthday = value; OnPropertyChanged("Birthday"); } }

        /// <summary>
        /// Домашний адрес
        /// </summary>
        public string Adress { get => _adress; set { _adress = value; OnPropertyChanged("Adress"); } }

        /// <summary>
        /// Номер паспорта
        /// </summary>
        public string Passport { get => _passport; set { _passport = value; OnPropertyChanged("Passport"); } }

        /// <summary>
        /// Подразделение организации
        /// </summary>
        public Department Department { get => department; set { department = value; OnPropertyChanged("Department"); } }

        /// <summary>
        /// Дата приема в организацию
        /// </summary>
        public DateTime DateOfEmployment { get => _dateOfEmployment; set { _dateOfEmployment = value; OnPropertyChanged("DateOfEmployment"); } }

        /// <summary>
        /// Оклад
        /// </summary>
        public int Salary { get => salary; set { salary = value; OnPropertyChanged("Salary"); } }

        #endregion

        /// <summary>
        /// Конструктор сотрудника
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="birthday">День рождения</param>
        /// <param name="dateEmpl">Дата приема</param>
        /// <param name="salary">Оклад</param>
        /// <param name="department">Подразделение</param>
        public Employee(string firstName, string lastName, DateTime birthday, DateTime dateEmpl, int salary, Department department)
        {
            PersID = ++maxID;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            DateOfEmployment = dateEmpl;
            Salary = salary;
            Department = department;
        }

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
