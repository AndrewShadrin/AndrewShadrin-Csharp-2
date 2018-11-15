using System;

namespace EmployeeDBSOA.Models
{
    public class Employee 
    {
        #region Поля класса

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
        //private int departmentId;
        //private string departmentName;

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
        public int PersID { get; set; }

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
            }
        }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime Birthday { get => _birthday; set { _birthday = value; } }

        /// <summary>
        /// Домашний адрес
        /// </summary>
        public string Adress { get => _adress; set { _adress = value; } }

        /// <summary>
        /// Номер паспорта
        /// </summary>
        public string Passport { get => _passport; set { _passport = value; } }

        /// <summary>
        /// Подразделение организации / убрано в связи с проблемой сериализации до выяснения
        /// </summary>
        public Department Department { get => department; set { department = value; } }
        //public int DepartmentId { get => departmentId; set { departmentId = value; } }
        //public string DepartmentName { get => departmentName; set { departmentName = value; } }

        /// <summary>
        /// Дата приема в организацию
        /// </summary>
        public DateTime DateOfEmployment { get => _dateOfEmployment; set { _dateOfEmployment = value; } }

        /// <summary>
        /// Оклад
        /// </summary>
        public int Salary { get => salary; set { salary = value;} }

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
        public Employee(int persId, string firstName, string lastName,  int salary, string departmentName, int depId, DateTime birthday, DateTime dateEmpl)
        {
            PersID = persId;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            DateOfEmployment = dateEmpl;
            Salary = salary;
            Department = new Department(depId,departmentName);
            //DepartmentId = depId;
            //DepartmentName = departmentName;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Employee()
        {

        }
    }
}