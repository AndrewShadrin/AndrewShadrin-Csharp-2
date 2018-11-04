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
        private string firstName;
        private string lastName;
        private int salary;
        
        /// <summary>
        /// Хранилище максимального табельного номера
        /// </summary>
        public static int maxID = 0;

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
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }


        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime Birthday { get; set; }
        
        /// <summary>
        /// Домашний адрес
        /// </summary>
        public string Adress { get; set; }
        
        /// <summary>
        /// Номер паспорта
        /// </summary>
        public string Passport { get; set; }
        
        /// <summary>
        /// Подразделение организации
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// Дата приема в организацию
        /// </summary>
        public DateTime DateOfEmployment { get; set; }

        /// <summary>
        /// Оклад
        /// </summary>
        public int Salary
        {
            get
            {
                return salary;
            }
            set
            {
                salary = value;
                OnPropertyChanged("Salary");
            }
        }


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

        /// <summary>
        /// Формирует строковое представление
        /// </summary>
        /// <returns>Строковое представление</returns>
        //public override string ToString()
        //{
        //    return $"{PersID,3}: {FirstName} {LastName}, {Department}";
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
