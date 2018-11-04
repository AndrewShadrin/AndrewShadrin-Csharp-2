using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeList
{
    /// <summary>
    /// Предоставляет описание класса "Организация"
    /// </summary>
    public class Organization
    {
        /// <summary>
        /// Код организации
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Юридический адрес
        /// </summary>
        public string LegalAdress { get; set; }

        /// <summary>
        /// Список подразделений
        /// </summary>
        public ObservableCollection<Department> Departments;

        /// <summary>
        /// Список сотрудников
        /// </summary>
        public ObservableCollection<Employee> Employees;

        public Organization(int id, string name, string adress="")
        {
            ID = id;
            Name = name;
            LegalAdress = adress;
            Departments = new ObservableCollection<Department>();
            Employees = new ObservableCollection<Employee>();
        }
    }
}
