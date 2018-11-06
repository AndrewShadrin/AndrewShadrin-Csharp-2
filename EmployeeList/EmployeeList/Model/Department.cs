
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeList
{
    /// <summary>
    /// Представляет описание класса "Подразделение"
    /// </summary>
    public class Department : INotifyPropertyChanged
    {
        /// <summary>
        /// Хранилище максимального номера
        /// </summary>
        public static int MaxID = 0;

        /// <summary>
        /// Код подразделения
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Наименование подразделения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Конструктор подразделения
        /// </summary>
        /// <param name="name">Наименование подразделения</param>
        public Department(string name)
        {
            ID = ++MaxID;
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


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
