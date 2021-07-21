using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleAsyncAwait
{
    class Program
    {
        static void Factorial()
        {
            int result = 1;
            for(int i=1; i<=6; i++)
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
