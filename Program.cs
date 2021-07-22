using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleAsyncAwait
{
    class Program
    {
        static void Factorial(int n = 6)
        {
            if (n < 1) throw new Exception($"{n}: число не должно быть меньше 1");
            int result = 1;
            for(int i=1; i<=n; i++)
            {
                result *= i;
            }

            Thread.Sleep(8000);
            Console.WriteLine($"Факториал {result}");
        }

        //Определение асинхронного метода async-await
        static async void FactorialAsync(int n)
        {
            Console.WriteLine("Начало метода FactorialAsync"); // выполняется синхронно

            try
            {
                Task task = Task.Run(() => Factorial(n));// выполняется асинхронно
                await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
            Console.WriteLine("Конец метода FactorialAsync");
        }

        static async void FactorialAsyncInSeries()
        {
            //Выполняется последовательно
            await Task.Run(() => Factorial(4));
            await Task.Run(() => Factorial(3));
            await Task.Run(() => Factorial(5));
        }

        static async void FactorialAsyncInParalell()
        {
            Task alltasks = null;
            try
            {
                //Выполняется паралелльно
                Task t1 = Task.Run(() => Factorial(4));
                Task t2 = Task.Run(() => Factorial(3));
                Task t3 = Task.Run(() => Factorial(5));
                //await Task.WhenAll(new[] { t1, t2, t3 });
                alltasks = Task.WhenAll(new Task[] { t1, t2, t3 });
                await alltasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Исключение:"+ ex.Message);
                Console.WriteLine("IsFaulted=" + alltasks.IsFaulted);
                foreach(var inx in alltasks.Exception.InnerExceptions)
                {
                    Console.WriteLine("Внутреннее исключение:" + inx.Message);
                }
                
            }
        }

        static void Main(string[] args)
        {
            int n;            
            
            Console.WriteLine("Введите число:");
            n  = Int32.Parse(Console.ReadLine());
            
            FactorialAsync(n);  // Вызов асинхронного метода


            Console.WriteLine($"Квадрат числа равен {n*n}");

            Console.Read();



        }
    }
}
