using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Регулярка_От_Жигулина__29._03._2021_
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введи текст - ");
                string text = Console.ReadLine();

                text = UnZip(text, 1);

                Console.WriteLine(text + "\n");
            }
        } // Обработка текста

        static string UnZip(string text, int multiply)
        {

            try // Отловить ошибку, когда подстрока пуста
            {
                for (int inBrackets = text.IndexOf('['); inBrackets != -1; inBrackets = text.IndexOf('[', inBrackets + 1))
                {
                    int outBrackets;
                    string number = "";

                    // Нахождение внешних скобок и проверка на их логичность (Иначе воспринимать их как обычные символы)
                    {
                        // Проверка на логичность скобки (Есть ли число перед ней)
                        if (inBrackets - 1 == -1) continue;
                        if (FromCharToInt(text[inBrackets - 1]) == -1) continue;
                        /////////////////////////////////////////////////////////

                        // Поиск ближайщей закрывающей скобки и проверка на ее присутствие
                        outBrackets = text.IndexOf(']', inBrackets + 1);
                        if (outBrackets == -1) break;
                        /////////////////////////////

                        // Уточнение эта ли закрывающая скобка нужна (Если есть вложенные скобки)
                        for (int internalInBrackets = text.IndexOf('[', inBrackets + 1); internalInBrackets != -1; internalInBrackets = text.IndexOf('[', internalInBrackets + 1))
                        {
                            // Проверка на логичность внутренней скобки
                            if (FromCharToInt(text[internalInBrackets - 1]) == -1) continue;
                            if (Math.Sign(outBrackets - internalInBrackets) == -1) break; // Если внутренняя скобка за внешней скобкой
                                                                                          /////////////////////////////////////////////////////////////

                            // Когда есть внутренние скобки, то переключиться на следующую закрывающую скобку
                            if (text.IndexOf(']', outBrackets + 1) != -1)
                            {
                                outBrackets = text.IndexOf(']', outBrackets + 1);
                            }
                        }
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    // Нахождение числа
                    try
                    {
                        for (int indexNum = inBrackets - 1; FromCharToInt(text[indexNum]) != -1; indexNum--)
                        {
                            number += text[indexNum];
                        }
                    }
                    catch (IndexOutOfRangeException) { }
                    number = ReverseString(number);
                    ///////////////////////////////

                    // Извлечение и распаковка подстроки
                    string substring = UnZip(text.Substring(inBrackets + 1, outBrackets - inBrackets - 1), int.Parse(number));

                    // Удаление скобок и числа из строки (Это для того, чтобы на их место поставить подстроку)
                    text = text.Remove(inBrackets - number.Length, outBrackets - inBrackets + 1 + number.Length);

                    // Вставка подстроки (На место числа и скобок
                    text = text.Insert(inBrackets - number.Length, substring);
                }
            }
            catch (Exception) { }

            // Умножение подстроки на число
            text = MultiplyString(text, multiply);

            return text;
        }
        static string MultiplyString(string text, int number)
        {
            string result = "";
            for (int k = 0; k < number; k++)
            {
                result += text;
            }
            return result;
        }
        static int FromCharToInt(char symbol)
        {
            switch (symbol)
            {
                case '0':
                    return 0;
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 7;
                case '8':
                    return 8;
                case '9':
                    return 9;
                default:
                    return -1;
            }
        } 
        static string ReverseString(string text)
        {
            string result = "";
            for(int k = text.Length - 1; k > -1; k--)
            {
                result += text[k];
            }
            return result;
        }
    }
}

