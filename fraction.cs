using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba6
{
    /// <summary>
    /// Представляет дробь с целочисленным числителем и знаменателем.
    /// </summary>
    public class Fraction : ICloneable
    {
        /// <summary>
        /// Числитель дроби.
        /// </summary>
        public int Numerator { get; }

        /// <summary>
        /// Знаменатель дроби.
        /// </summary>
        public int Denominator { get; }

        /// <summary>
        /// Создает дробь с указанным числителем и знаменателем.
        /// </summary>
        /// <param name="numerator">Числитель дроби</param>
        /// <param name="denominator">Знаменатель дроби (должен быть ненулевым)</param>
        /// <exception cref="ArgumentException">Если знаменатель равен нулю</exception>
        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Знаменатель не может быть равен нулю");
            }

            // Нормализация знака (знак хранится в числителе)
            int sign = 1;
            if (denominator < 0)
            {
                sign = -1;
            }

            Numerator = numerator * sign;
            Denominator = Math.Abs(denominator);
        }

        /// <summary>
        /// Возвращает строковое представление дроби в формате "числитель/знаменатель".
        /// </summary>
        /// <returns>Строка вида "числитель/знаменатель"</returns>
        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        /// <summary>
        /// Складывает текущую дробь с другой дробью.
        /// </summary>
        /// <param name="other">Дробь для сложения</param>
        /// <returns>Новая дробь - результат сложения</returns>
        public Fraction Add(Fraction other)
        {
            int newNumerator = Numerator * other.Denominator + other.Numerator * Denominator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Складывает текущую дробь с целым числом.
        /// </summary>
        /// <param name="value">Целое число для сложения</param>
        /// <returns>Новая дробь - результат сложения</returns>
        public Fraction Add(int value)
        {
            return Add(new Fraction(value, 1));
        }

        /// <summary>
        /// Вычитает из текущей дроби другую дробь.
        /// </summary>
        /// <param name="other">Дробь для вычитания</param>
        /// <returns>Новая дробь - результат вычитания</returns>
        public Fraction Subtract(Fraction other)
        {
            int newNumerator = Numerator * other.Denominator - other.Numerator * Denominator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Умножает текущую дробь на другую дробь.
        /// </summary>
        /// <param name="other">Дробь для умножения</param>
        /// <returns>Новая дробь - результат умножения</returns>
        public Fraction Multiply(Fraction other)
        {
            int newNumerator = Numerator * other.Numerator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Делит текущую дробь на другую дробь.
        /// </summary>
        /// <param name="other">Дробь для деления</param>
        /// <returns>Новая дробь - результат деления</returns>
        /// <exception cref="ArgumentException">Если делитель равен нулю</exception>
        public Fraction Divide(Fraction other)
        {
            if (other.Numerator == 0)
            {
                throw new ArgumentException("Нельзя делить на ноль");
            }

            return Multiply(new Fraction(other.Denominator, other.Numerator));
        }

        /// <summary>
        /// Сравнивает текущую дробь с другой дробью.
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>True, если дроби равны, иначе False</returns>
        public override bool Equals(object obj)
        {
            if (obj is Fraction other)
            {
                // Сравнение после приведения к общему знаменателю
                return Numerator * other.Denominator == other.Numerator * Denominator;
            }
            return false;
        }

        /// <summary>
        /// Возвращает хеш-код для дроби.
        /// </summary>
        /// <returns>Хеш-код дроби</returns>
        public override int GetHashCode()
        {
            return (Numerator, Denominator).GetHashCode();
        }

        /// <summary>
        /// Клонирует текущую дробь.
        /// </summary>
        /// <returns>Новая копия дроби</returns>
        public object Clone()
        {
            return new Fraction(Numerator, Denominator);
        }
    }

    /// <summary>
    /// Интерфейс для кэширования вещественного значения дроби.
    /// </summary>
    public interface ICachedDouble
    {
        /// <summary>
        /// Получает кэшированное вещественное значение дроби.
        /// </summary>
        /// <returns>Вещественное значение дроби</returns>
        double GetCachedValue();
    }

    /// <summary>
    /// Обобщенная версия дроби с кэшированием вещественного значения.
    /// </summary>
    /// <typeparam name="T">Тип дроби, должен наследоваться от Fraction</typeparam>
    public class CachedFraction<T> : Fraction, ICachedDouble where T : Fraction
    {
        private double _cachedValue;

        /// <summary>
        /// Создает кэширующую дробь на основе существующей дроби.
        /// </summary>
        /// <param name="fraction">Исходная дробь</param>
        public CachedFraction(T fraction) : base(fraction.Numerator, fraction.Denominator)
        {
            _cachedValue = (double)Numerator / Denominator;
        }

        /// <inheritdoc/>
        public double GetCachedValue()
        {
            return _cachedValue;
        }
    }

    /// <summary>
    /// Класс для запуска заданий, связанных с дробями.
    /// </summary>
    public static class FractionTasks
    {
        /// <summary>
        /// Запускает задание 1: операции с дробями.
        /// </summary>
        public static void RunTask1()
        {
            Console.WriteLine("\n--- ЗАДАНИЕ 2.1: ОПЕРАЦИИ С ДРОБЯМИ ---");

            Console.Write("Введите числитель первой дроби: ");
            int num1 = int.Parse(Console.ReadLine());
            Console.Write("Введите знаменатель первой дроби: ");
            int den1 = int.Parse(Console.ReadLine());
            Fraction f1 = new Fraction(num1, den1);

            Console.Write("Введите числитель второй дроби: ");
            int num2 = int.Parse(Console.ReadLine());
            Console.Write("Введите знаменатель второй дроби: ");
            int den2 = int.Parse(Console.ReadLine());
            Fraction f2 = new Fraction(num2, den2);

            Console.Write("Введите целое число: ");
            int intValue = int.Parse(Console.ReadLine());

            Console.WriteLine($"\nДроби: {f1} и {f2}, целое число: {intValue}");
            Console.WriteLine($"Сложение: {f1} + {f2} = {f1.Add(f2)}");
            Console.WriteLine($"Вычитание: {f1} - {f2} = {f1.Subtract(f2)}");
            Console.WriteLine($"Умножение: {f1} * {f2} = {f1.Multiply(f2)}");
            Console.WriteLine($"Деление: {f1} / {f2} = {f1.Divide(f2)}");
            Console.WriteLine($"Сложение с целым: {f1} + {intValue} = {f1.Add(intValue)}");
        }

        /// <summary>
        /// Запускает задание 2: сравнение дробей.
        /// </summary>
        public static void RunTask2()
        {
            Console.WriteLine("\n--- ЗАДАНИЕ 2.2: СРАВНЕНИЕ ДРОБЕЙ ---");

            Console.Write("Введите числитель первой дроби: ");
            int num1 = int.Parse(Console.ReadLine());
            Console.Write("Введите знаменатель первой дроби: ");
            int den1 = int.Parse(Console.ReadLine());
            Fraction f1 = new Fraction(num1, den1);

            Console.Write("Введите числитель второй дроби: ");
            int num2 = int.Parse(Console.ReadLine());
            Console.Write("Введите знаменатель второй дроби: ");
            int den2 = int.Parse(Console.ReadLine());
            Fraction f2 = new Fraction(num2, den2);

            Console.WriteLine($"\nДроби: {f1} и {f2}");
            Console.WriteLine($"f1 == f2: {f1.Equals(f2)}");
            Console.WriteLine($"f1 != f2: {!f1.Equals(f2)}");

            Fraction f3 = new Fraction(1, 2);
            Fraction f4 = new Fraction(2, 4);
            Console.WriteLine($"\nПример: {f3} и {f4} равны? {f3.Equals(f4)}");
        }

        /// <summary>
        /// Запускает задание 3: клонирование дробей.
        /// </summary>
        public static void RunTask3()
        {
            Console.WriteLine("\n--- ЗАДАНИЕ 2.3: КЛОНИРОВАНИЕ ДРОБЕЙ ---");

            Console.Write("Введите числитель дроби: ");
            int num = int.Parse(Console.ReadLine());
            Console.Write("Введите знаменатель дроби: ");
            int den = int.Parse(Console.ReadLine());
            Fraction original = new Fraction(num, den);

            Fraction clone = (Fraction)original.Clone();

            Console.WriteLine($"\nОригинал: {original}");
            Console.WriteLine($"Клон: {clone}");
            Console.WriteLine($"Ссылки равны? {object.ReferenceEquals(original, clone)}");
            Console.WriteLine($"Значения равны? {original.Equals(clone)}");
        }

        /// <summary>
        /// Запускает задание 4: кэширование вещественного значения.
        /// </summary>
        public static void RunTask4()
        {
            Console.WriteLine("\n--- ЗАДАНИЕ 2.4: КЭШИРОВАНИЕ ВЕЩЕСТВЕННОГО ЗНАЧЕНИЯ ---");

            Console.Write("Введите числитель дроби: ");
            int num = int.Parse(Console.ReadLine());
            Console.Write("Введите знаменатель дроби: ");
            int den = int.Parse(Console.ReadLine());
            Fraction fraction = new Fraction(num, den);

            CachedFraction<Fraction> cachedFraction = new CachedFraction<Fraction>(fraction);

            Console.WriteLine($"\nДробь: {fraction}");
            Console.WriteLine($"Вещественное значение (кэшированное): {cachedFraction.GetCachedValue()}");
            Console.WriteLine($"Формат: «{fraction}»");
        }
    }
}
