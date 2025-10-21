using System;
using System.Reflection;

namespace fanfic.bible.tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Тестовый проект запущен!");
            Console.WriteLine($"Версия .NET: {Environment.Version}");
            Console.WriteLine();

            try
            {
                // Проверим, загружается ли сборка
                var assembly = Assembly.GetExecutingAssembly();
                Console.WriteLine($"Загружена сборка: {assembly.FullName}");
                Console.WriteLine();

                TestRunner.RunAllTests();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка: {ex.Message}");
                Console.WriteLine($"Детали: {ex.StackTrace}");
            }

            Console.WriteLine();
            Console.WriteLine("Программа завершена. Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}