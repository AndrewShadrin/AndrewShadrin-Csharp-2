
namespace EmployeeDBSOA.Models
{
    /// <summary>
    /// Представляет описание класса "Подразделение"
    /// </summary>
    public class Department 
    {

        /// <summary>
        /// Код подразделения
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Наименование подразделения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Конструктор подразделения
        /// </summary>
        /// <param name="name">Наименование подразделения</param>
        public Department(int id, string name)
        {
            ID = id;
            Name = name;
        }

        /// <summary>
        /// Формирует строковое представление
        /// </summary>
        /// <returns>Строковое представление</returns>
        public override string ToString()
        {
            return $"{ID}: {Name}";
        }
    }
}
