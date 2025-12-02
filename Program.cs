using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba6
{
    /// <summary>
    /// Основной класс программы с меню выбора заданий.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        private static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine("1. Задание 1: Коты");
                Console.WriteLine("2. Задание 2: Дроби");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите задание: ");

                string input = Console.ReadLine();
                bool isChoiceValid = int.TryParse(input, out int choice);

                if (!isChoiceValid || choice < 0 || choice > 2)
                {
                    Console.WriteLine("Неверный выбор! Введите число от 0 до 2.");
                    continue;
                }

                if (choice == 0)
                {
                    isRunning = false;
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            RunCatsMenu();
                            break;
                        case 2:
                            RunFractionsMenu();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка выполнения: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Запускает меню для работы с котами.
        /// </summary>
        private static void RunCatsMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ЗАДАНИЯ 1 (КОТЫ) ---");
                Console.WriteLine("1. Создание кота и мяуканье");
                Console.WriteLine("2. Коллекция котов");
                Console.WriteLine("3. Подсчет мяуканий");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите подзадание: ");

                string input = Console.ReadLine();
                bool isChoiceValid = int.TryParse(input, out int choice);

                if (!isChoiceValid || choice < 0 || choice > 3)
                {
                    Console.WriteLine("Неверный выбор! Введите число от 0 до 3.");
                    continue;
                }

                if (choice == 0)
                {
                    break;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            CatTasks.RunTask1();
                            break;
                        case 2:
                            CatTasks.RunTask2();
                            break;
                        case 3:
                            CatTasks.RunTask3();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка выполнения: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Запускает меню для работы с дробями.
        /// </summary>
        private static void RunFractionsMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ЗАДАНИЯ 2 (ДРОБИ) ---");
                Console.WriteLine("1. Операции с дробями");
                Console.WriteLine("2. Сравнение дробей");
                Console.WriteLine("3. Клонирование дробей");
                Console.WriteLine("4. Кэширование вещественного значения");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите подзадание: ");

                string input = Console.ReadLine();
                bool isChoiceValid = int.TryParse(input, out int choice);

                if (!isChoiceValid || choice < 0 || choice > 4)
                {
                    Console.WriteLine("Неверный выбор! Введите число от 0 до 4.");
                    continue;
                }

                if (choice == 0)
                {
                    break;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            FractionTasks.RunTask1();
                            break;
                        case 2:
                            FractionTasks.RunTask2();
                            break;
                        case 3:
                            FractionTasks.RunTask3();
                            break;
                        case 4:
                            FractionTasks.RunTask4();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка выполнения: {ex.Message}");
                }
            }
        }
    }
}