/*
Задание 23(***).
Назначение.Сглаживание функции, заданной таблицей значений в равноотстоящих точках,
с помощью многочлена третьей степени, построенного по пяти последовательным точкам методом наименьших квадратов.
Описание параметров.
Входные параметры:
– вектор значений функции в порядке возрастания аргумента;
– количество значений функции;
Выходные параметры:
-вектор сглаженных значений функции;
-индикатор ошибки:
-сглаженные значения отличаются от исходных;
-сглаженные значения совпадают с исходными.
- сглаживание невозможно, во входном векторе меньше пяти значений.
Замечание.
Предусмотреть визуализацию полученных результатов.
Указание. См. []
настоящего пособия.
*/


namespace Labs.CHM.Lab4Vizualizer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}