using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba6
{
    /// <summary>
    /// Интерфейс для объектов, которые могут мяукать.
    /// </summary>
    public interface IMeowable
    {
        /// <summary>
        /// Вызывает мяуканье объекта.
        /// </summary>
        void Meow();
    }

    /// <summary>
    /// Класс, представляющий кота.
    /// </summary>
    public class Cat : IMeowable
    {
        /// <summary>
        /// Имя кота.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Создает экземпляр кота с указанным именем.
        /// </summary>
        /// <param name="name">Имя кота</param>
        /// <exception cref="ArgumentException">Если имя пустое.</exception>
        public Cat(string name)
        {
            if (name == null || name == "")
            {
                throw new ArgumentException("Имя кота не может быть пустым");
            }

            Name = name;
        }

        /// <inheritdoc/>
        public void Meow()
        {
            Console.WriteLine($"{Name}: мяу!");
        }

        /// <summary>
        /// Выводит на экран "Имя: мяу-мяу-...-мяу!" N раз.
        /// </summary>
        /// <param name="n">Количество повторений, должно быть больше 0.</param>
        /// <exception cref="ArgumentException">Если n <= 0.</exception>
        public void Meow(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentException("Количество мяуканий должно быть больше 0");
            }

            // Используем простой цикл вместо Enumerable.Repeat
            string result = "мяу";
            for (int i = 1; i < n; i++)
            {
                result = result + "-мяу";
            }
            Console.WriteLine($"{Name}: {result}!");
        }
    }

    /// <summary>
    /// Обертка для подсчета количества вызовов метода Meow.
    /// </summary>
    /// <typeparam name="T">Тип объекта, реализующего IMeowable</typeparam>
    public class MeowCounter<T> : IMeowable where T : IMeowable
    {
        private readonly T _meowable;

        /// <summary>
        /// Количество вызовов метода Meow.
        /// </summary>
        public int CallCount { get; private set; }

        /// <summary>
        /// Получает оригинальный объект, который мяукает.
        /// </summary>
        public T OriginalObject => _meowable;

        /// <summary>
        /// Создает обертку для подсчета вызовов.
        /// </summary>
        /// <param name="meowable">Объект для отслеживания</param>
        public MeowCounter(T meowable)
        {
            if (meowable == null)
            {
                throw new ArgumentNullException(nameof(meowable));
            }

            _meowable = meowable;
            CallCount = 0;
        }

        /// <inheritdoc/>
        public void Meow()
        {
            _meowable.Meow();
            CallCount = CallCount + 1;
        }
    }

    /// <summary>
    /// Вспомогательные методы для работы с мяукающими объектами.
    /// </summary>
    public static class MeowHelper
    {
        /// <summary>
        /// Вызывает метод Meow для всех объектов в коллекции.
        /// </summary>
        /// <param name="meowables">Коллекция мяукающих объектов</param>
        /// <exception cref="ArgumentNullException">Если коллекция null</exception>
        public static void MeowAll(IEnumerable<IMeowable> meowables)
        {
            if (meowables == null)
            {
                throw new ArgumentNullException(nameof(meowables));
            }

            foreach (var meowable in meowables)
            {
                if (meowable != null)
                {
                    meowable.Meow();
                }
            }
        }

        /// <summary>
        /// Вызывает метод Meow у объекта 5 раз.
        /// </summary>
        /// <param name="meowable">Объект для мяуканья</param>
        /// <exception cref="ArgumentNullException">Если объект null</exception>
        public static void MeowFiveTimes(IMeowable meowable)
        {
            if (meowable == null)
            {
                throw new ArgumentNullException(nameof(meowable));
            }

            int i = 0;
            while (i < 5)
            {
                meowable.Meow();
                i = i + 1;
            }
        }
    }

    /// <summary>
    /// Класс для запуска заданий, связанных с котами.
    /// </summary>
    public static class CatTasks
    {
        /// <summary>
        /// Запускает задание 1: создание кота и мяуканье.
        /// </summary>
        /// <returns>Счетчик для созданного кота</returns>
        public static MeowCounter<Cat> RunTask1()
        {
            Console.WriteLine("\n--- ЗАДАНИЕ 1.1: СОЗДАНИЕ КОТА И МЯУКАНЬЕ ---");
            Console.Write("Введите имя кота: ");
            string name = Console.ReadLine();

            Cat cat = new Cat(name);
            MeowCounter<Cat> counter = new MeowCounter<Cat>(cat);

            Console.WriteLine("\nОдно мяуканье:");
            counter.Meow(); // Используем счетчик вместо прямого вызова

            Console.Write("\nСколько раз кот должен мяукнуть? ");
            try
            {
                int count = int.Parse(Console.ReadLine());

                if (count <= 0)
                {
                    Console.WriteLine("Неверное количество. Используем значение по умолчанию (3):");
                    for (int i = 0; i < 3; i++)
                    {
                        counter.Meow();
                    }
                }
                else
                {
                    Console.WriteLine($"\n{count} мяуканий:");
                    for (int i = 0; i < count; i++)
                    {
                        counter.Meow();
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный формат числа. Используем значение по умолчанию (3):");
                for (int i = 0; i < 3; i++)
                {
                    counter.Meow();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return counter;
        }

        /// <summary>
        /// Запускает задание 2: коллекция котов.
        /// </summary>
        public static void RunTask2()
        {
            Console.WriteLine("\n--- ЗАДАНИЕ 1.2: КОЛЛЕКЦИЯ КОТОВ ---");
            Console.Write("Сколько котов создать? ");
            int count;

            try
            {
                count = int.Parse(Console.ReadLine());
                if (count <= 0)
                {
                    Console.WriteLine("Неверное количество. Создадим 2 котов.");
                    count = 2;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный формат числа. Создадим 2 котов.");
                count = 2;
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка ввода. Создадим 2 котов.");
                count = 2;
            }

            List<IMeowable> catsWithCounters = new List<IMeowable>();
            int createdCats = 0;
            int attempts = 0;

            while (createdCats < count && attempts < 10)
            {
                attempts = attempts + 1;
                Console.Write($"Введите имя {createdCats + 1}-го кота: ");
                string name = Console.ReadLine();

                try
                {
                    Cat newCat = new Cat(name);
                    MeowCounter<Cat> counter = new MeowCounter<Cat>(newCat);
                    catsWithCounters.Add(counter);
                    createdCats = createdCats + 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при создании кота: {ex.Message}");
                }
            }

            Console.WriteLine("\nМяуканье всех котов:");
            MeowHelper.MeowAll(catsWithCounters);
        }

        /// <summary>
        /// Запускает задание 3: подсчет мяуканий.
        /// </summary>
        /// <param name="existingCatCounter">Счетчик уже существующего кота из предыдущих заданий</param>
        public static void RunTask3(MeowCounter<Cat> existingCatCounter)
        {
            Console.WriteLine("\n--- ЗАДАНИЕ 1.3: ПОДСЧЕТ МЯУКАНИЙ ---");

            if (existingCatCounter == null)
            {
                Console.WriteLine("Нет созданного кота. Сначала создайте кота в задании 1.");
                Console.Write("Введите имя для нового кота: ");
                string name = Console.ReadLine();
                Cat cat = new Cat(name);
                existingCatCounter = new MeowCounter<Cat>(cat);
            }

            // Используем свойство OriginalObject для получения имени кота
            Console.WriteLine($"Кот {existingCatCounter.OriginalObject.Name} мяукал {existingCatCounter.CallCount} раз");

            // Добавим еще несколько мяуканий для демонстрации
            Console.WriteLine("\nДобавим еще 2 мяуканья для демонстрации:");
            existingCatCounter.Meow();
            existingCatCounter.Meow();

            Console.WriteLine($"\nТеперь кот мяукал {existingCatCounter.CallCount} раз");
        }
    }
}
