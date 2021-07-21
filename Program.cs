using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleAsyncAwait
{
    class Program
    {
        static void Factorial(int x = 6)
        {
            int result = 1;
            for(int i=1; i<=x; i++)
            {
                result *= i;
            }

            Thread.Sleep(8000);
            Console.WriteLine($"Факториал {result}");
        }

        //Определение асинхронного метода async-await
        static async void FactorialAsync()
        {
            Console.WriteLine("Начало метода FactorialAsync"); // выполняется синхронно
            await Task.Run(() => Factorial());// выполняется асинхронно
            
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
            //Выполняется паралелльно
            Task t1 = Task.Run(() => Factorial(4));
            Task t2 = Task.Run(() => Factorial(3));
            Task t3 = Task.Run(() => Factorial(5));
            //await Task.WhenAll(new[] { t1, t2, t3 });
            await Task.WhenAll(new Task[] { t1, t2, t3 });
        }

        static void Main(string[] args)
        {
            FactorialAsync();  // Вызов асинхронного метода

            Console.WriteLine("Введите число:");
            int n = Int32.Parse(Console.ReadLine());
            Console.WriteLine($"Квадрат числа равен {n*n}");

            Console.Read();



        }
    }
}
