using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lr_16
{


    public class NumberSystem
    {
        private int _base;
        private List<int> _digits;

        //Конструктори
        public NumberSystem(int @base, List<int> digits)
        {
            _base = @base;
            _digits = digits;

        }

        public NumberSystem(int @base, int number)
        {
            _base = @base;
            _digits = new List<int>();

            while (number > 0)
            {
                _digits.Add(number % @base);
                number /= @base;
            }

            _digits.Reverse();
        }

        //Конструктор копіювання
        public NumberSystem(NumberSystem other)
        {
            _base = other._base;
            _digits = new List<int>(other._digits);
        }

        //Деструктор
        ~NumberSystem()
        {
            //Звільнення ресурсів, які були захоплені під час роботи із обєктом
        }

        //Методи класу
        public NumberSystem Power(int exponent)
        {
            NumberSystem result = new NumberSystem(_base, 1);

            for (int i = 0; i < exponent; i++)
            {
                result = result.Multiply(this);
            }

            return result;
        }

        public NumberSystem Multiply(NumberSystem other)
        {
            List<int> resultDigits = new List<int>();

            int carry = 0;
            int thisIndex = _digits.Count - 1;
            int otherIndex = other._digits.Count - 1;

            while (thisIndex >= 0 || otherIndex >= 0 || carry > 0)
            {
                int thisDigit = thisIndex >= 0 ? _digits[thisIndex] : 0;
                int otherDigit = otherIndex >= 0 ? other._digits[otherIndex] : 0;

                int product = thisDigit + otherDigit + carry;
                int resultDigit = product % _base;
                carry = product / _base;

                resultDigits.Insert(0, resultDigit);

                thisIndex--;
                otherIndex--;
            }

            return new NumberSystem(_base, resultDigits);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (int digit in _digits)
            {
                result.Append(digit.ToString());

            }

            return result.ToString();
        }
        //метод додавання

        public static NumberSystem Add(NumberSystem a, NumberSystem b)
        {
            int carry = 0;
            List<int> resultDigits = new List<int>();

            int aIndex = a._digits.Count - 1;
            int bIndex = b._digits.Count - 1;

            while (aIndex >= 0 || bIndex >= 0 || carry > 0)
            {
                int aDigit = aIndex >= 0 ? a._digits[aIndex] : 0;
                int bDigit = bIndex >= 0 ? b._digits[bIndex] : 0;

                int sum = aDigit + bDigit + carry;
                int resultDigit = sum % a._base;
                carry = sum / a._base;

                resultDigits.Insert(0, resultDigit);

                aIndex--;
                bIndex--;



            }

            return new NumberSystem(a._base, resultDigits);
        }
        //метод віднімання
        public NumberSystem Subtract(NumberSystem other)
        {

            List<int> resultDigits = new List<int>();
            int borrow = 0;
            int thisIndex = _digits.Count - 1;
            int otherIndex = other._digits.Count - 1;

            while (thisIndex >= 0 || otherIndex >= 0 || borrow < 0)
            {
                int thisDigit = thisIndex >= 0 ? _digits[thisIndex] : 0;
                int otherDigit = otherIndex >= 0 ? other._digits[otherIndex] : 0;

                int difference = thisDigit - otherDigit - borrow;
                if (difference < 0)
                {
                    difference += _base;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }

                resultDigits.Insert(0, difference);

                thisIndex--;
                otherIndex--;


            }

            return new NumberSystem(_base, resultDigits);



        }

        //метод множення
        public static NumberSystem Multiply(NumberSystem num1, NumberSystem num2)
        {
            List<int> resultDigits = new List<int>();

            int carry = 0;
            int num1Index = num1._digits.Count - 1;

            while (num1Index >= 0)
            {
                NumberSystem tempResult = new NumberSystem(num1._base, 0);
                List<int> tempResultDigits = new List<int>();
                for (int i = 0; i < num1._digits.Count - num1Index - 1; i++);
                {
                    tempResultDigits.Insert(0, 0);
                }

                int num2Index = num2._digits.Count - 1;

                while (num2Index >= 0 || carry > 0)
                {
                    int num2Digit = num2Index >= 0 ? num2._digits[num2Index] : 0;

                    int product = num1._digits[num1Index] * num2Digit + carry;
                    int resultDigit = product % num1._base;
                    carry = product / num1._base;

                    tempResultDigits.Insert(0, resultDigit);

                    num2Index--;
                }

                tempResult._digits = tempResultDigits;
                resultDigits = Add(resultDigits, tempResult._digits);
                num1Index--;
            }

            return new NumberSystem(num1._base, resultDigits);

        }
        private static List<int> Add(List<int> num1, List<int> num2)
        {
            List<int> resultDigits = new List<int>();

            int carry = 0;
            int num1Index = num1.Count - 1;
            int num2Index = num2.Count - 1;

            while (num1Index >= 0 || num2Index >= 0 || carry > 0)
            {
                int num1Digit = num1Index >= 0 ? num1[num1Index] : 0;
                int num2Digit = num2Index >= 0 ? num2[num2Index] : 0;

                int sum = num1Digit + num2Digit + carry;
                int resultDigit = sum % num1.Count;
                carry = sum / num1.Count;

                resultDigits.Insert(0, resultDigit);

                num1Index--;
                num2Index--;
            }

            return resultDigits;
        }

        public int CompareTo(NumberSystem other)
        {
            //порівняння кількості цифр в числах
            if (_digits.Count < other._digits.Count)
            {
                return -1;

            }
            else if(_digits.Count>other._digits.Count)
            {
                return 1;

            }

            //порівняння цифр в числах посимвольно, починаючи з молодшиз розярядів
            for (int i=0; i < _digits.Count; i++)
            {
                if (_digits[i]< other._digits[i])
                {
                    return -1;
                }
                else if (_digits[i] > other._digits[i])
                {
                    return 1;
                }
            }


            //якщо всі цифри рівні, то числа рівні

            return 0;
        }

        //метод ділення
        public static NumberSystem Divide(NumberSystem dividend, NumberSystem divisor)
        {
            // Перевірка на нульовий дільник
            if (divisor._digits.Count == 1 && divisor._digits[0] == 0)
            {
                throw new DivideByZeroException("Ділення на нуль");
            }

            // Перевірка на те, що дільник менший діленого
            if (dividend.CompareTo(divisor) < 0)
            {
                return new NumberSystem(dividend._base, 0);
            }

            // Ініціалізація змінних
            int basePower = 0;
            NumberSystem remainder = new NumberSystem(dividend);
            NumberSystem quotient = new NumberSystem(dividend._base, 0);

            // Знаходження найбільшої степеня основи, яка менша або дорівнює діленому
            while (remainder.CompareTo(divisor.Power(basePower + 1)) >= 0)
            {
                basePower++;
            }

            // Виконання ділення
            while (basePower >= 0)
            {
                NumberSystem power = divisor.Power(basePower);
                int quotientDigit = 0;

                while (remainder.CompareTo(power) >= 0)
                {
                    remainder = remainder.Subtract(power);
                    quotientDigit++;
                }

                quotient._digits.Add(quotientDigit);
                basePower--;
            }

            quotient._digits.Reverse();
            return quotient;
        }

        //метод відносин
        public int Check(NumberSystem other)
        {
            //Якщо основа чисел не співпадає, то порівняння неможливе
            if(_base != other._base)
            {
                throw new ArgumentException("Неможливо порівняти");
            }

            //Якщо кількість розрядів не співпадає, порівняння за кількістю розрядів
            if (_digits.Count != other._digits.Count)
            {
                return _digits.Count.CompareTo(other._digits.Count);
            }

            for (int i = 0; i < _digits.Count; i++)
            {
                int comparisonResult = _digits[i].CompareTo(other._digits[i]);

                if (comparisonResult != 0)
                {
                    return comparisonResult;
                }
            }


            return 0;
        }
    }
}