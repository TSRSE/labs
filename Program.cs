﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace lab1_sort
{
    class ReadFromFileRaw
    {
        public static int[] writeSingleArray;
        public static int[,] writeDuoArray1, writeDuoArray2;
        public static int[][] writeStepArray;

        public static bool readingfromfile = false;

        public static void ReadSingleLinedArray()
        {
            string[] LinesToRead;
            string _Number = "";
            string DefaultExeption = "Ошибка записи в файле, пожалуйста, проверьте введенные данные...\nПример верного ввода: 3; -1; 0; 80; ";
            List<int> NumbersList = new List<int>();
            string default_path = @"Arrays\single.txt"; //Прямой путь к файлу, типизация файла: a; b; c;
            try
            {
                LinesToRead = File.ReadAllLines(default_path);
            }

            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка в пути к файлу!\nВ папке Arrays не было найдено файла single.txt! Создаем его...");
                File.WriteAllText(@"Arrays\single.txt", "-2; 0; 2;");
                Console.ResetColor();
                Console.WriteLine("Нажмите любую клавишу для возвращения в главное меню...");
                Console.ReadKey();
                Program.Menu();
            }

            LinesToRead = File.ReadAllLines(default_path);
            string FullLineOfArray="";
            try { 
                FullLineOfArray = LinesToRead[0];
            }
            catch
            {
                Console.WriteLine("Не удалось считать файл");
            }

            if (!FullLineOfArray.Contains(";"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DefaultExeption);
                Console.ResetColor();
                Console.WriteLine("Нажмите любую клавишу для возвращения в главное меню...");
                Console.ReadKey();
                Program.Menu();
            }

            //Чтение из файла
            for (int i = 0; i < FullLineOfArray.LastIndexOf(';') + 1; i++)
            {
                for (int j = 0; j < FullLineOfArray.IndexOf(';'); j++)
                {
                    _Number += FullLineOfArray.ElementAt(j);
                    FullLineOfArray.Replace(FullLineOfArray.ElementAt(j).ToString(), "");
                }

                try { 
                NumbersList.Add(int.Parse(_Number));
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(DefaultExeption);
                    Console.ResetColor();
                    Console.WriteLine("Нажмите любую клавишу для возвращения в главное меню...");
                    Console.ReadKey();
                    Program.Menu();
                }
                FullLineOfArray = FullLineOfArray.Remove(0, _Number.Length + 1);

                _Number = "";
            }
            //Смотрим, что получилось
            try
            {
                Console.Clear();
                NumbersList.Add(Int32.Parse(FullLineOfArray.ToString().Replace(";", "")));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Got some trubles here...");
                System.Threading.Thread.Sleep(500);
                Program.Menu();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done Sucsessfully!");
                System.Threading.Thread.Sleep(1000);
                Console.ResetColor();
                Console.Clear();
            }
            //Запись из листа в массив
            writeSingleArray = NumbersList.ToArray();
        } //Чтение одномерного массива из файла
        public static void ReadDuoLinedArray(int InCaseOfArray)
        {
            string DefaultExeption = "Ошибка записи в файле, пожалуйста, проверьте введенные данные...\nПример верного ввода: \n3; -1; 0; 80;\n5; -11; 103; 11; \nЗаметьте, что если элементов для ровного массива не будет хватать, пустые места займут нули";
            string[] LinesToRead;
            string FullLineOfArray = "";
            string _Number = "";
            string default_path;
            List<int> NumbersList = new List<int>();
            int maxElemsInLines = 0;
            if (InCaseOfArray == 1)
            {
                default_path = @"Arrays\duo_first.txt"; //Прямой путь к файлу, типизация файла: a; b; c;\na1; a2; a3;
                try
                {
                    LinesToRead = File.ReadAllLines(default_path);
                }

                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка в пути к файлу!\nВ папке Arrays не было найдено файла duo_first.txt! Создаем его...");
                    File.WriteAllText(@"Arrays\duo_first.txt", "-1; 0; 1;\n-2; -0; 2;");
                    Console.ResetColor();
                    Console.WriteLine("Нажмите любую клавишу для возвращения в главное меню...");
                    Console.ReadKey();
                    Program.Menu();
                } //Исключение на путь
            }
            else
            {
                default_path = @"Arrays\duo_second.txt"; //Прямой путь к файлу, типизация файла: a; b; c;\na1; a2; a3;
                try
                {
                    LinesToRead = File.ReadAllLines(default_path);
                }

                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка в пути к файлу!\nВ папке Arrays не было найдено файла duo_second.txt! Создаем его...");
                    File.WriteAllText(@"Arrays\duo_second.txt", "1; 0; -1;\n2; -0; -2;");
                    Console.ResetColor();
                    Console.WriteLine("Нажмите любую клавишу для возвращения в главное меню...");
                    Console.ReadKey();
                    Program.Menu();
                } //Исключение на путь
            }
            LinesToRead = File.ReadAllLines(default_path);

            //Проверка на хоть какую-то верность ввода данных пользователем в файл
            for (int i = 0; i < LinesToRead.Length; i++)
            {
                FullLineOfArray += LinesToRead[i];
            }

            if (!FullLineOfArray.Contains("; "))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DefaultExeption);
                Console.ResetColor();
                Console.WriteLine("Нажмите любую клавишу для возвращения в главное меню...");
                Console.ReadKey();
                Program.Menu();
            }

            //Счетчик макс. кол-ва элементов в строках в массиве
            int numbersInLineCounter = 0;
            for (int i = 0; i < LinesToRead.Length; i++)
            {
                foreach (char separator in LinesToRead[i])
                {
                    if (separator == ';') numbersInLineCounter++;
                }
                if (maxElemsInLines < numbersInLineCounter) maxElemsInLines = numbersInLineCounter;
                numbersInLineCounter = 0;
            }
            //Счетчик закончился
            FullLineOfArray = "";
            LinesToRead = File.ReadAllLines(default_path);

            //С каким массивом мы работаем на данный момент?
            if (InCaseOfArray == 1) writeDuoArray1 = new int[LinesToRead.Length, maxElemsInLines];
            else if (InCaseOfArray == 2) writeDuoArray2 = new int[LinesToRead.Length, maxElemsInLines];

            //Присвоение элементов из файла в лист
            numbersInLineCounter = 0;
            for (int i = 0; i < LinesToRead.Length; i++) //На Каждую строку
            {
                for (int k = 0; k < LinesToRead[i].LastIndexOf(';') + 1; k++) // Сколько там этих ваших
                {
                    for (int j = 0; j < LinesToRead[i].IndexOf(';'); j++)
                    {
                        _Number += LinesToRead[i].ElementAt(j);
                        LinesToRead[i].Replace(LinesToRead[i].ElementAt(j).ToString(), "");
                    }
                    try { 
                        NumbersList.Add(int.Parse(_Number));
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(DefaultExeption);
                        Console.ResetColor();
                        Console.WriteLine("Нажмите любую клавишу для возвращения в главное меню...");
                        Console.ReadKey();
                        Program.Menu();
                    }
                    LinesToRead[i] = LinesToRead[i].Remove(0, _Number.Length + 1);

                    _Number = "";
                }

                //Присвоение элементов из листа массиву
                if (numbersInLineCounter <= i)
                {
                    for (int l = 0; l < maxElemsInLines; l++)
                    {
                        try
                        {
                            if (InCaseOfArray == 1) writeDuoArray1[numbersInLineCounter, l] = NumbersList.ElementAt(l);
                            else if (InCaseOfArray == 2) writeDuoArray2[numbersInLineCounter, l] = NumbersList.ElementAt(l);
                        }
                        catch //если в данной строке нехватает элементов, заполню нулями
                        {
                            if (InCaseOfArray == 1) writeDuoArray1[numbersInLineCounter, l] = 0;
                            else if (InCaseOfArray == 2) writeDuoArray2[numbersInLineCounter, l] = 0;
                        }
                    }
                    numbersInLineCounter++;
                    NumbersList.Clear();
                }
            }


            //Все, мы записали, выдаем результат
            try
            {
                Console.Clear();
                NumbersList.Add(Int32.Parse(FullLineOfArray.ToString().Replace(";", "")));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Got some trubles here...");
                System.Threading.Thread.Sleep(500);
                Program.Menu();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done Sucsessfully!");
                System.Threading.Thread.Sleep(1000);
                Console.ResetColor();
                Console.Clear();
            }
        } //Чтение двумерного массива из файла 1ый или 2ой
        public static void ReadStepLinedArray()
        {
            string DefaultExeption = "Ошибка записи в файле, пожалуйста, проверьте введенные данные...\nПример верного ввода: \n3; -1; 0; 80;\n5; -11;";
            string[] LinesToRead;
            string FullLineOfArray = "";
            string _Number = "";
            List<int> NumbersList = new List<int>();
            int maxElemsInLines = 0;

            string default_path = @"Arrays\multy.txt"; //Прямой путь к файлу, типизация файла: a; b; c;\na1; a2; a3;
            try
            {
                LinesToRead = File.ReadAllLines(default_path);
            }

            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка в пути к файлу!\nВ папке Arrays не было найдено файла multy.txt! Создаем его...");
                File.WriteAllText(@"Arrays\multy.txt", "1; 2; 3;\n4; 5; 6; 7;");
                Console.ResetColor();
                Console.WriteLine("Нажмите любую клавишу для возвращения в главное меню...");
                Console.ReadKey();
                Program.Menu();
            } 
            LinesToRead = File.ReadAllLines(default_path);

            for (int i = 0; i < LinesToRead.Length; i++)// Запись всего файла в одну строку для проверки на наличие хотя бы одной ;
            {
                FullLineOfArray += LinesToRead[i];
            }

            //Простая проверка на данные в файле
            if (!FullLineOfArray.Contains("; "))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DefaultExeption);
                Console.ResetColor();
                Console.WriteLine("Нажмите любую клавишу для возвращения в главное меню...");
                Console.ReadKey();
                Program.Menu();
            } 

            //Важная штука, счетчик элементов в каждой строке
            int linesToListCounter = 0;
            for (int i = 0; i < LinesToRead.Length; i++) //На Каждую строку
            {
                while (LinesToRead[i].Length != 0)
                {
                    for (int j = 0; j < LinesToRead[i].IndexOf(';'); j++)
                    {
                        _Number += LinesToRead[i].ElementAt(j);
                        LinesToRead[i].Replace(LinesToRead[i].ElementAt(j).ToString(), "");
                    }
                    try
                    {
                        NumbersList.Add(int.Parse(_Number));
                    }
                    catch
                    {
                        //Я просто считаю...
                    }
                    LinesToRead[i] = LinesToRead[i].Remove(0, _Number.Length + 1);

                    _Number = "";
                }
                if (linesToListCounter < i)
                {
                    linesToListCounter++;
                    NumbersList.Clear();
                }
                if (maxElemsInLines < NumbersList.Count)
                    maxElemsInLines = NumbersList.Count;
            }
            //счетчик закончился

            FullLineOfArray = "";
            LinesToRead = File.ReadAllLines(default_path);
            for (int i = 0; i < LinesToRead.Length; i++)
                FullLineOfArray += LinesToRead[i];
            writeStepArray = new int[LinesToRead.Length][];

            //Запись
            linesToListCounter = 0;
            for (int i = 0; i < LinesToRead.Length; i++) //На Каждую строку
            {
                while (LinesToRead[i].Length != 0)
                {
                    for (int j = 0; j < LinesToRead[i].IndexOf(';'); j++) //Символов до ';'
                    {
                        _Number += LinesToRead[i].ElementAt(j);
                        LinesToRead[i].Replace(LinesToRead[i].ElementAt(j).ToString(), "");
                    }

                    try
                    {
                        NumbersList.Add(int.Parse(_Number));
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("В {0} строке файла была допущена ошибка, \nвведите недостоющее значение или сотрите лишнюю ';'", i);
                        Console.ReadKey();
                        Program.Menu();
                    }

                    LinesToRead[i] = LinesToRead[i].Remove(0, _Number.Length + 1);

                    _Number = "";
                }
                //Запись в массив
                if (linesToListCounter <= i)
                {

                    for (int l = 0; l < maxElemsInLines; l++)
                    {
                        try
                        {
                            writeStepArray[linesToListCounter] = NumbersList.ToArray();
                        }
                        catch
                        {
                            writeStepArray[linesToListCounter][l] = int.Parse(LinesToRead[l].ToString().Replace(";", ""));
                        }
                    }
                    linesToListCounter++;
                    NumbersList.Clear();
                }
            }
            //Запись закончена, выводим сообщение о готовности
            try
            {
                Console.Clear();
                NumbersList.Add(Int32.Parse(FullLineOfArray.ToString().Replace(";", "")));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Got some trubles here...");
                System.Threading.Thread.Sleep(500);
                Program.Menu();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done Sucsessfully!");
                System.Threading.Thread.Sleep(1000);
                Console.ResetColor();
                Console.Clear();
            }
        } //Чтение зубчатого массива из файла
    }
    class Program
    {
        static void AskForFileInput(string addString)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Вы хотите ввести данные {0}из файла?[Y/n] ", addString);

                string Answer = Console.ReadLine();

                if (Answer.ToUpper() == "Y")
                {
                    ReadFromFileRaw.readingfromfile = true;
                    break;
                }
                if (Answer.ToUpper() == "N")
                {
                    ReadFromFileRaw.readingfromfile = false;
                    break;
                }
            }
            Console.Clear();
        } //Запрос на чтение из файла
        public static void Menu()
        {
            Directory.CreateDirectory("Arrays");
            Console.Title = "Лабораторная работа 1 | Сортировка массивов | Удалых Максим БПИ 20-9";
            Int32 Coice = 0, key;
            do
            {
                Console.Clear();
                Console.WriteLine("Выбор сортировки");
                Console.WriteLine(((Coice == 0) ? ">> " : " ") + "Сортировка одномерного массива");
                Console.WriteLine(((Coice == 1) ? ">> " : " ") + "Сортировка одномерного массива с использованием свойств и методов");
                Console.WriteLine(((Coice == 2) ? ">> " : " ") + "Сортировка двумерного массива");
                Console.WriteLine(((Coice == 3) ? ">> " : " ") + "Сортировка ступенчатого массива");

                key = (int)Console.ReadKey().Key;
                if (key == 38) Coice--;
                if (key == 40) Coice++;
                if (key == 13 || key == 27) break;

                if (Coice < 0) Coice = 3;
                if (Coice > 3) Coice = 0;

            } while (key != 27);

            if (key == 27)
            {
                Process.GetCurrentProcess().Kill();
            }
            if (Coice == 0)
            {
                SortByCodeSingle();
            }
            Console.WriteLine(Coice);
            if (Coice == 1)
            {
                SortByCodeArraySingle();
            }
            Console.WriteLine(Coice);
            if (Coice == 2)
            {
                SortByCodeDouble();
            }
            Console.WriteLine(Coice);
            if (Coice == 3)
            {
                SortByCodeStep();
            }
        } //Меню
        static void SortByCodeSingle()
        {
            bool error;
            int[] a_sortByCode;
            int inMin = 0;
            int inMax = 0;
            int Min;
            int Max;
            int n = 0;
            Console.Clear();

            AskForFileInput("");
            if (ReadFromFileRaw.readingfromfile)
            {
                ReadFromFileRaw.ReadSingleLinedArray();
                a_sortByCode = ReadFromFileRaw.writeSingleArray;
                n = a_sortByCode.Length;
            }
            else
            {
                while (true)
                {
                    Console.Clear();

                    Console.Write("Введите нужное количество элементов в массиве:");

                    error = int.TryParse(Console.ReadLine(), out n);
                    if (!error || n < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Кол-во элементов не может быть меньше 1 или иметь буквенный размер");
                        Console.ResetColor();
                        Console.WriteLine("\nНажмите любую кнопку для продолжения...");
                        Console.ReadKey();
                    }
                    else break;
                }

                a_sortByCode = new int[n];

                //try-catch только хуже
                for (int i = 0; i < n; i++)
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.Write("Введите {0} элемент: ", i + 1);

                        error = int.TryParse(Console.ReadLine(), out a_sortByCode[i]);
                        if (error == false)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Произошла ошибка, повторите ввод...");
                            Console.ResetColor();
                            Console.WriteLine("\nНажмите любую кнопку для продолжения...");
                            Console.ReadKey();
                        }
                        else break;
                    }
                }
            }

            Min = a_sortByCode[0];
            Max = a_sortByCode[0];
            //MinMax
            for (int i = 0; i < a_sortByCode.Length; i++)
            {
                if (Max < a_sortByCode[i])
                {
                    Max = a_sortByCode[i];
                    inMax = i;
                }

                if (Min >= a_sortByCode[i])
                {
                    Min = a_sortByCode[i];
                    inMin = i;
                }
            }
            //Output
            Console.Clear();
            Console.WriteLine("Вывожу массив");
            foreach (int element in a_sortByCode)
            {
                Console.Write("{0}\t", element);
            }

            Console.WriteLine("\n\nСамое большое число\nНомер:{2} \tИндекс:{0}\tЗначение:{1}", inMax, Max, inMax + 1);
            Console.WriteLine("\nСамое маленькое число\nНомер:{2} \tИндекс:{0}\tЗначение:{1}", inMin, Min, inMin + 1);

            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    if (a_sortByCode[i] > a_sortByCode[j])
                    {
                        int temp = a_sortByCode[i];
                        a_sortByCode[i] = a_sortByCode[j];
                        a_sortByCode[j] = temp;
                    }
                }
            }

            Console.WriteLine("\n\nПо возрастанию");
            foreach (int element in a_sortByCode)
            {
                Console.Write("{0}\t", element);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    if (a_sortByCode[i] < a_sortByCode[j])
                    {
                        int temp = a_sortByCode[i];
                        a_sortByCode[i] = a_sortByCode[j];
                        a_sortByCode[j] = temp;
                    }
                }
            }
            Console.WriteLine("\n\nПо убыванию");
            foreach (int element in a_sortByCode)
            {
                Console.Write("{0}\t", element);
            }

            Console.WriteLine("\n\nЧетные");
            int n2 = 0;
            for (int i = 0; i < n; i++)
            {
                if (a_sortByCode[i] % 2 == 0)
                    n2++;
            }
            if (n2 == 0)
                Console.WriteLine("В массиве не было четных чисел.");
            else
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = i; j < n; j++)
                    {
                        if (a_sortByCode[i] > a_sortByCode[j])
                        {
                            int temp = a_sortByCode[i];
                            a_sortByCode[i] = a_sortByCode[j];
                            a_sortByCode[j] = temp;
                        }
                    }
                }
                Array.Sort(a_sortByCode);//Сортируем массив, потом записываем четные в новый массив, чтобы красиво было
                int[] a_sortByCode_even = new int[n2];
                int f = 0;
                for (int k = 0; k < n; k++)
                {
                    if (a_sortByCode[k] % 2 == 0)
                    {
                        a_sortByCode_even[f] = a_sortByCode[k];
                        f++;
                    }
                    else { }
                }
                for (int i = 0; i < n2; i++)
                {
                    Console.Write("{0}\t", a_sortByCode_even[i]);
                }

            }
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("\n\nНажмите любую клавишу для возвращения в главное меню");
            Console.ReadKey();
            Menu();
        } //Сортировка одномерного массива вручную
        static void SortByCodeArraySingle()
        {
            bool error;
            int n;
            int[] a_sortByCodeArray;

            Console.Clear();
            AskForFileInput("для первого массива");

            //InputChoice
            if (ReadFromFileRaw.readingfromfile)
            {
                ReadFromFileRaw.ReadSingleLinedArray();
                a_sortByCodeArray = ReadFromFileRaw.writeSingleArray;
                n = a_sortByCodeArray.Length;
            }
            else
            {
                while (true)
                {

                    Console.Clear();

                    Console.Write("Введите нужное количество элементов в массиве: ");

                    error = int.TryParse(Console.ReadLine(), out n);
                    if (error == false || n < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Произошла ошибка, повторите ввод...\nВозможные причины: количества элементов меньше 1 или вы ввели не целочисленное значение");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine("\nНажмите любую кнопку для продолжения...");
                        Console.ReadKey();
                    }
                    else break;
                }

                a_sortByCodeArray = new int[n];

                for (int i = 0; i < n; i++)
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.Write("Введите {0} элемент: ", i + 1);
                        error = int.TryParse(Console.ReadLine(), out a_sortByCodeArray[i]);
                        if (error == false)
                        {
                            Console.WriteLine("Произошла ошибка, повторите ввод...");
                            System.Threading.Thread.Sleep(500);
                        }
                        else break;
                    }
                }
            }
            //Вывод
            Console.Clear();
            Console.WriteLine("Вывожу массив");
            foreach (var item in a_sortByCodeArray)
            {
                Console.Write("{0}\t", item);
            }

            Console.WriteLine("\n\nМаксимальное значение: \nНомер:{2} \tИндекс:{0}\tЗначение:{1}", Array.IndexOf(a_sortByCodeArray, a_sortByCodeArray.Max()), a_sortByCodeArray.Max(), Array.IndexOf(a_sortByCodeArray, a_sortByCodeArray.Max()) + 1);
            Console.WriteLine("\n\nМинимальное значение: \nНомер:{2} \tИндекс:{0}\tЗначение:{1}", Array.IndexOf(a_sortByCodeArray, a_sortByCodeArray.Min()), a_sortByCodeArray.Min(), Array.IndexOf(a_sortByCodeArray, a_sortByCodeArray.Min()) + 1);

            Array.Sort(a_sortByCodeArray);
            Console.WriteLine("\n\nСортировка по возрастанию");
            foreach (var item in a_sortByCodeArray)
            {
                Console.Write("{0}\t", item);
            }

            Array.Reverse(a_sortByCodeArray);
            Console.WriteLine("\n\nСортировка по убыванию");
            foreach (var item in a_sortByCodeArray)
            {
                Console.Write("{0}\t", item);
            }

            Console.WriteLine("\n\nВывод четных чисел массива");
            Array.Sort(a_sortByCodeArray);
            int counter = 0;
            for (int i = 0; i < a_sortByCodeArray.Length; i++)
            {
                if (a_sortByCodeArray[i] % 2 == 0)
                {
                    counter++;
                }
            }
            if (counter == 0)
                Console.WriteLine("В массиве нет четных чисел.");
            else
            {
                int[] a_sortByCodeArray_even = new int[counter];
                counter = 0;
                for (int i = 0; i < a_sortByCodeArray.Length; i++)
                {
                    if (a_sortByCodeArray[i] % 2 == 0)
                    {
                        a_sortByCodeArray_even[counter] = a_sortByCodeArray[i];
                        counter++;
                    }
                }

                foreach (var item in a_sortByCodeArray_even)
                {
                    Console.Write("{0}\t", item);
                }

            }
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("\n\nНажмите любую клавишу для возвращения в главное меню");
            Console.ReadKey();
            Menu();
        } //Сортировка одномерного массива с использованием методов сортировки Sys.Array
        static void SortByCodeDouble()
        {
            string DefaultExeption = "Произошла ошибка, повторите ввод...\nВозможные причины: количества элементов меньше 1 или вы ввели не целочисленное значение";
            int n,m,n2,m2;
            bool error;
            int[,] a_sortByCodeDouble;
            int[,] a_sortByCodeDouble2;
            int max = 0,
            min = 0;
            int pillar_maxInd = 0,
            pillar_minInd = 0,
            l_maxInd = 0,
            l_minInd = 0;

            Console.Clear();
            AskForFileInput("для первого массива");

            #region first
            //InputCoice
            if (ReadFromFileRaw.readingfromfile)
            {
                ReadFromFileRaw.ReadDuoLinedArray(1);
                a_sortByCodeDouble = ReadFromFileRaw.writeDuoArray1;
                n = ReadFromFileRaw.writeDuoArray1.GetLength(0);
                m = ReadFromFileRaw.writeDuoArray1.GetLength(1);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        max = a_sortByCodeDouble[i, j];
                        min = a_sortByCodeDouble[i, j];
                        pillar_maxInd = j;
                        pillar_minInd = j;
                        l_maxInd = i;
                        l_minInd = i;
                    }
                }
            }
            else
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Параметры для первого массива");
                    Console.Write("\nВведите нужное количество строк в массиве 1: ");
                    error = int.TryParse(Console.ReadLine(), out n);
                    if (error == false || n < 1)
                    {
                        Console.Clear();
                        Console.WriteLine(DefaultExeption);
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine("\nНажмите любую кнопку для продолжения...");
                        Console.ReadKey();
                    }
                    else break;
                }
                while (true)
                {
                    Console.Clear();
                    Console.Write("\nВведите нужное количество столбцов в массиве 1:");
                    error = int.TryParse(Console.ReadLine(), out m);
                    if (error == false || n < 1)
                    {
                        Console.WriteLine(DefaultExeption);
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine("\nНажмите любую кнопку для продолжения...");
                        Console.ReadKey();
                    }
                    else break;
                }

                a_sortByCodeDouble = new int[n, m];

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.Write("Массив 1\nВведите элемент для {0} строки, {1} столбца: ", i + 1, j + 1);

                            error = int.TryParse(Console.ReadLine(), out a_sortByCodeDouble[i, j]);
                            if (error == false)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Произошла ошибка, повторите ввод...");
                                Console.ResetColor();
                                System.Threading.Thread.Sleep(500);
                            }
                            else
                            {
                                max = a_sortByCodeDouble[i, j];
                                min = a_sortByCodeDouble[i, j];
                                pillar_maxInd = j;
                                pillar_minInd = j;
                                l_maxInd = i;
                                l_minInd = i;
                                break;
                            }
                        }
                    }
                }
                Console.Clear();
            }
            #endregion

            #region second
            //InputSecondArray
            AskForFileInput("для второго массива");
            if (ReadFromFileRaw.readingfromfile)
            {
                ReadFromFileRaw.ReadDuoLinedArray(2);
                a_sortByCodeDouble2 = ReadFromFileRaw.writeDuoArray2;
                n2 = ReadFromFileRaw.writeDuoArray2.GetLength(0);
                m2 = ReadFromFileRaw.writeDuoArray2.GetLength(1);
            }
            else
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Параметры для второго массива");

                    Console.Write("Введите нужное количество строк в массиве 2:");
                    error = int.TryParse(Console.ReadLine(), out n2);
                    if (error == false || n < 1)
                    {
                        Console.Clear();
                        Console.WriteLine(DefaultExeption);
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine("\nНажмите любую кнопку для продолжения...");
                        Console.ReadKey();
                    }
                    else break;
                }
                while (true)
                {
                    Console.Clear();
                    Console.Write("Введите нужное количество столбцов в массиве 2: ");
                    error = int.TryParse(Console.ReadLine(), out m2);
                    if (error == false || n < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(DefaultExeption);
                        Console.ResetColor();
                        Console.WriteLine("\nНажмите любую кнопку для продолжения...");
                        Console.ReadKey();
                    }
                    else break;
                }

                a_sortByCodeDouble2 = new int[n2, m2];
                #endregion

                Console.Clear();
                for (int i = 0; i < n2; i++)
                {
                    for (int j = 0; j < m2; j++)
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.Write("Массив 2\nВведите элемент для {0} строки, {1} столбца: ", i + 1, j + 1);

                            error = int.TryParse(Console.ReadLine(), out a_sortByCodeDouble2[i, j]);
                            if (error == false)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Произошла ошибка, повторите ввод...");
                                Console.ResetColor();
                                Console.WriteLine("\nНажмите любую кнопку для продолжения...");
                                Console.ReadKey();
                            }
                            else break;
                        }
                    }
                }
                Console.Clear();
            }
            //Output
            Console.WriteLine("Вывожу массив 1");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < m; j++)
                {
                    Console.Write("{0}\t", a_sortByCodeDouble[i, j]);
                }
            }

            Console.WriteLine();
            Console.WriteLine("\nВывожу массив 2");
            for (int i = 0; i < n2; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < m2; j++)
                {
                    Console.Write("{0}\t", a_sortByCodeDouble2[i, j]);
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (max < a_sortByCodeDouble[i, j])
                    {
                        max = a_sortByCodeDouble[i, j];
                        l_maxInd = i;
                        pillar_maxInd = j;
                    }
                    if (min > a_sortByCodeDouble[i, j])
                    {
                        min = a_sortByCodeDouble[i, j];
                        l_minInd = i;
                        pillar_minInd = j;
                    }
                }
            }

            Console.WriteLine("\n\nМаксимальное значение в первом массиве: \nНомер: {3};{4} \tИндекс: {0};{1}\tЗначение: {2}", l_maxInd, pillar_maxInd, max, l_maxInd + 1, pillar_maxInd + 1);
            Console.WriteLine("\nМинимальное значение в первом массиве:  \nНомер: {3};{4} \tИндекс: {0};{1}\tЗначение: {2}", l_minInd, pillar_minInd, min, l_minInd + 1, pillar_minInd + 1);
            int summ_a1 = 0,
            summ_a2 = 0;
            int mult_a = 0;

            foreach (int el in a_sortByCodeDouble)
            {
                summ_a1 += el;
                mult_a *= el;
            }
            foreach (int el in a_sortByCodeDouble2)
            {
                summ_a2 += el;
                mult_a *= el;
            }

            Console.WriteLine("\nПроизведение двух массивов: {0}\nРазность двух массивов: {1}\nСумма двух массивов: {2}", summ_a1 * summ_a2, summ_a1 - summ_a2, summ_a1 + summ_a2);

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("\n\nНажмите любую клавишу для возвращения в главное меню");
            Console.ReadKey();
            Menu();
        } //Сортировка двумерного массива 
        static void SortByCodeStep()
        {
            bool error;
            int _index = 0;
            int[][] a_sortbysteps;
            int min = 0;
            int max = 0;
            int l = 0,
            n = 0;

            Console.Clear();
            AskForFileInput("");
            //InputChoice
            if (ReadFromFileRaw.readingfromfile)
            {
                ReadFromFileRaw.ReadStepLinedArray();
                a_sortbysteps = ReadFromFileRaw.writeStepArray;
                max = a_sortbysteps[0][0];
                min = a_sortbysteps[0][0];
                n = a_sortbysteps.GetLength(0);
                l = ReadFromFileRaw.writeStepArray[0].Length;
            }
            else
            {
                while (true)
                {
                    Console.Clear();
                    Console.Write("Выберите количество массивов для массива: ");

                    error = int.TryParse(Console.ReadLine(), out n);
                    if (error == false || n < 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Произошла ошибка, повторите ввод...\nВозможные причины: количества элементов меньше 1 или вы ввели не целочисленное значение");
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine("\nНажмите любую кнопку для продолжения...");
                        Console.ReadKey();
                    }
                    else break;
                }
                a_sortbysteps = new int[n][];

                for (int i = 0; i < n; i++)
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.Write("Выберите кол-во элементов для массива {0}: ", i+1);

                        error = int.TryParse(Console.ReadLine(), out l);
                        if (error == false)
                        {
                            Console.WriteLine("Произошла ошибка, повторите ввод...");
                            System.Threading.Thread.Sleep(500);
                        }
                        else
                        {
                            a_sortbysteps[i] = new int[l];
                            break;
                        }

                    }
                }

                l = a_sortbysteps.Length;
                for (int i = 0; i < l; i++)
                {
                    for (int j = 0; j < a_sortbysteps[i].Length; j++)
                    {
                        while (true) {
                            Console.Clear();
                            Console.Write("\nМассив {0}, элемент {1}: ", i + 1, j + 1);

                            error = int.TryParse(Console.ReadLine(), out a_sortbysteps[i][j]);
                            if (error == false)
                            {
                                Console.WriteLine("Произошла ошибка, повторите ввод...");
                                System.Threading.Thread.Sleep(500);
                            }
                            else
                            {
                                max = a_sortbysteps[i][j];
                                min = a_sortbysteps[i][j];
                                break;
                            }
                        }
                    }
                }
            }
            Console.Clear();

            //Редактор массива
            while (true)
            {
                Console.Write("Перед тем, как продолжить, вы хотите изменить данные элементов в массиве?[Y/n]");
                string answer = Console.ReadLine();
                Console.Clear();
                if (answer.ToUpper() == "Y")
                {
                    bool ischanging = true;
                    int number = 0;
                    while (ischanging)
                    {

                        bool accepted = false;
                        while (accepted == false)
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.Write("Всего массивов: {0}\nВведите номер массива: ", a_sortbysteps.Length);
                                error = int.TryParse(Console.ReadLine(), out number);
                                if (error == false)
                                {
                                    Console.WriteLine("Произошла ошибка, повторите ввод...");
                                    System.Threading.Thread.Sleep(500);
                                }
                                else break;
                            }

                            if (number > a_sortbysteps.Length || number < 1)
                            {
                                accepted = false;
                                Console.Clear();
                                Console.WriteLine("Вы не можете ввести число большее кол-ва массивов и меньшее 1");
                            }

                            else accepted = true;

                            number--;
                        }
                        accepted = false;

                        while (accepted == false)
                        {
                            foreach (var item in a_sortbysteps[number])
                            {
                                Console.Write("{0}\t", item);
                            }

                            Console.Write("\nВведите номер элемента: ");
                            error = int.TryParse(Console.ReadLine(), out _index);
                            if (error == false)
                            {
                                Console.Clear();
                                Console.WriteLine("Произошла ошибка, повторите ввод...");
                                System.Threading.Thread.Sleep(500);
                            }

                            if (_index > a_sortbysteps[number].Length || _index < 1)
                            {
                                accepted = false;
                                Console.Clear();
                                Console.WriteLine("Вы не можете выбрать несуществующий элемент");
                            }
                            else
                            {
                                accepted = true;
                                break;
                            }
                        }
                        _index--;
                        int numToChange = a_sortbysteps[number][_index];//вынес, потому что в цикле значения обнуляются
                        while (true)
                        {
                            Console.Clear();
                            Console.Write("Заменить число {0} на ", numToChange);

                            error = int.TryParse(Console.ReadLine(), out a_sortbysteps[number][_index]);

                            if (error == false)
                            {
                                Console.WriteLine("\nИзвините, но это не число");
                                Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить");
                                Console.ReadKey();
                            }
                            else break;
                        }

                        Console.Clear();
                        Console.WriteLine("Введите Enter, чтобы продолжить и ESQ чтобы выйти из редактора массивов.");

                        int key = (int)Console.ReadKey().Key;
                        if (key == 27) ischanging = false;
                    }
                    break;
                }
                if (answer.ToUpper() == "N") break;
            }
            //Output
            Console.WriteLine("Вывожу массив...");
            for (int i = 0; i < a_sortbysteps.Length; i++)
            {
                Console.Write("\nМассив {0}\n", i+1);
                for (int j = 0; j < a_sortbysteps[i].Length; j++)
                {
                    Console.Write("{0}\t", a_sortbysteps[i][j]);
                    if (min > a_sortbysteps[i][j]) min = a_sortbysteps[i][j];
                    if (max < a_sortbysteps[i][j]) max = a_sortbysteps[i][j];
                }
            }

            Console.WriteLine("\n\nМаксимальное значение: {0}\nМинимальное значение: {1}", max, min);

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("\n\nНажмите любую клавишу для возвращения в главное меню");
            Console.ReadKey();
            Menu();
        } //Сортировка ступенчатого массива
        static void Main(string[] args) => Menu();
    }
}