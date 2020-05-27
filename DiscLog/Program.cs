using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscLog
{
    class Program
    {
        static int y = 0, a = 0, p = 0, mode = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("y=a^x mod(p)");// напоминание формулы

            while (y < 1) //ввод переменной y
            {
                Console.WriteLine("y =");
                int.TryParse(Console.ReadLine(), out y);
            }

            while (a < 1)// ввод переменной a
            {
                Console.WriteLine("a =");
                int.TryParse(Console.ReadLine(), out a);
            }

            while (p < 1)// ввод переменной p
            {
                Console.WriteLine("p =");
                int.TryParse(Console.ReadLine(), out p);
            }

            while (mode < 1 || mode > 2)// выбор режима программы, 1 - шаг младенца шаг великана, 2 - полный перебор
            {
                Console.WriteLine("1 - «Шаг младенца шаг великана», 2 - полный перебор");
                int.TryParse(Console.ReadLine(), out mode);
            }

            switch (mode)// переключатель режимов работы программы
            {
                case 1:
                    {
                        Console.WriteLine($"x = {MladVel()}");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine($"x = {PolnPereb()}");
                        break;
                    }
            }

            Console.ReadKey();
        }

        static int Step(int num, int pow)// функция для возведения в степень
        {
            if (pow == 0) return 1;
            int ret = num;
            for (int i = 1; i < pow; i++)
            {
                ret = (ret * num) % p;
            }
            return ret;
        }

        static int MladVel()// шаг младенца шаг великана
        {
            int m = (int)Math.Ceiling(Math.Sqrt(p));// определение значений m и k
            int k = m;// взять их одинаковыми
            if (!(m * k > p)) m++;// дополнительная проверка
            // создание двух массивов через листы
            List<int> ay_list = new List<int>();
            List<int> a_list = new List<int>();

            for (int i = 0; i < m; i++)// формирование ряда a^(m-1)*y
            {
                ay_list.Add((Step(a, i) * y) % p);
                Console.WriteLine($"a^{i}*y mod(p) = {ay_list.Last()}");
            }
            Console.WriteLine();
            for (int i = 1; i <= k; i++)// формирование ряда a^km
            {
                a_list.Add(Step(a, i * m));
                Console.WriteLine($"a^({i}m) mod(p) = {a_list.Last()}");
            }

            for (int j = 0; j < m; j++)// поиск значений i и j, при которых (a^j)*y = a^(im)
                for (int i = 1; i <= k; i++)
                {
                    if (ay_list[j] == a_list[i - 1]) return i * m - j;
                }
            throw new Exception("Ошибка");
        }

        static int PolnPereb()//метод полного перебора, перебираем все значения до победного конца
        {
            for (int i = 0; ; i++)
            {
                int result = Step(a, i);
                Console.WriteLine($"a^{i} mod(p) = {result}");
                if (result == y) return i;
            }
        }
    }
}