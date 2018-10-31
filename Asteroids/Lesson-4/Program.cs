using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_4
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Task 2
            // 2.Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:

            List<int> a = new List<int> { 1, 2, 4, 3, 5, 4, 4, 2, 6 };
            foreach (int val in a.Distinct().OrderBy(e => e))
            {
                Console.WriteLine(val + " - " + a.Where(x => x == val).Count() + " раз");
            }
            Console.ReadLine();
            List<string> b = new List<string> { "One", "Two", "Four", "Three", "Five", "Four", "Four", "Two", "Six" };
            foreach (string val in b.Distinct().OrderBy(e => e))
            {
                Console.WriteLine($"{val,6} - {b.Where(x => x == val).Count()} раз");
            }
            Console.ReadLine();

            #endregion

            #region Task 3
            // а) Свернуть обращение к OrderBy с использованием лямбда - выражения $.
            // б) *Развернуть обращение к OrderBy с использованием делегата Predicate<T>.

            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two",2 },
                {"one",1 },
                {"three",3 },
            };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            Console.WriteLine();

            var d1 = dict.OrderBy(e => e.Value );
            foreach (var pair in d1)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            Console.WriteLine();

            Console.ReadLine();

            #endregion
        }
    }
}
