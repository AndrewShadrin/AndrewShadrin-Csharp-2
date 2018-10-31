using System;

namespace EmployeeList
{
    /// <summary>
    /// Предоставляет описание сотрудника организации
    /// </summary>
    class Employee
    {
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
        public string FirstName { get; }
        
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string LastName { get; }
        
        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime Birthday { get; }
        
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
        /// Конструктор сотрудника
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="birthday">День рождения</param>
        public Employee(string firstName, string lastName, DateTime birthday, Department department = null)
        {
            PersID = ++maxID;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            Department = department;
        }

        public override string ToString()
        {
            return $"{PersID,3}: {FirstName} {LastName}, {Department}";
        }

    }
}
