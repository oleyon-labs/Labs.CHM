using System;
using System.IO;
using System.Linq;
namespace Labs.CHM.Lab1;

class Program
{
    //const int e = 100;
    static void Main(string[] args)
    {
        Console.WriteLine("Введите n:");
        int n = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите e:");
        int e = Convert.ToInt32(Console.ReadLine());
        double precision = 0;
        int testsCount = 10;
        //double totalPrecision = 0;
        for (int iter = 0; iter < testsCount; iter++)
        {
            double[][] vectors = new double[6][];
            for (int i = 0; i < 6; i++)
            {
                vectors[i] = GenerateVector(n, e);
            }
            vectors[0][0] = vectors[5][0];
            vectors[1][0] = vectors[4][0];
            vectors[1][1] = vectors[5][1];
            vectors[2][0] = vectors[4][1];
            vectors[2][1] = vectors[5][2];


            /*double[] a, b, c, f, p, q;
            a = new double[] { 1, -5, 7, 3, 4 };
            b = new double[] { 2, 10, -1, 4, 8, -7 };
            c = new double[] { 7, -3, -5, 4, -9 };
            p = new double[] { 2, 7, -1, 4, 4, 6 };
            q = new double[] { 1, 10, -3, -10, 2, 5 };
            */

            //var kor = ReadFromFile(n, "input.txt");
            double[] xses = new double[n];
            for (int i = 0; i < n; i++)
            {
                xses[i] = 1;
            }
            //double[] f2 = CalculateRightSide(a, b, c, p, q, xses);
            //double[] f3 = { 22, 5, -11, 15, 2, -3 };
            double[] f1 = CalculateRightSide(vectors[0], vectors[1], vectors[2], vectors[4], vectors[5], xses);
            //double[] f1 = CalculateRightSide(kor.a, kor.b, kor.c, kor.p, kor.q, xses);
            (double[] solvedX, double[] precisionX) = Solve(n, vectors[0], vectors[1], vectors[2], vectors[3], vectors[4], vectors[5], f1);
            //(double[] solvedX, double[] precisionX) = Solve(n, a, b, c, f2, p, q, f3);
            //(double[] solvedX, double[] precisionX) = Solve(n, new double[]{-5,-1,6,0,6,-5,7,-7,9 }, new double[] { 5,2,-7,-6,6,-1,-7,-9,-7,6 }, new double[] {-3,4,-3,-1,9,-10,3,6,-9 }, new double[] {-24,9,-33,-3,45,-15,-27,12,-69,45 }, new double[] {5,-3,1,-1,7,2,-9,-5,-4,-1 }, new double[] {-5,2,4,10,4,6,-8,-10,-10,10 });
            //double[] solvedX = Solve(n, new double[] { 5, 7, 3 }, new double[] {9,8,-6,4 }, new double[] {12,-5,-16 }, new double[] {3,6,10,-2 }, new double[] {9,12,10,1 }, new double[] {5,8,-5,6 });
            //(double[] solvedX, double[] precisionX) = Solve(n, kor.a, kor.b, kor.c, kor.f, kor.p, kor.q, f1);
            //Console.WriteLine(precisionX.Max());

            //precision = 0;
            //for(int i=0;i<precisionX.Length;i++)
            //{
            //    if(precision<Math.Abs(precisionX[i]-1))
            //    {
            //        precision = Math.Abs(precisionX[i] - 1);
            //    }
            //}
            //totalPrecision += precision;


            precision += Math.Abs(precisionX.Max() - 1);




            //Console.WriteLine("Решение:");
            /*for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"x{i} = {solvedX[i]};");
            }*/
        }
        //precision /= testsCount;
        Console.WriteLine($"precision = {Math.Abs(precision / testsCount)}");
        //Console.WriteLine($"precision = {Math.Abs(totalPrecision / testsCount)}");
        //WriteToFile(solvedX, "output.txt");
    }
    static (double[] solvedX, double[] precisionX) Solve(int n, double[] a, double[] b, double[] c, double[] f, double[] p, double[] q, double[] f1)
    {
        for (int i = n - 1; i > 1; i--)
        {
            if (i == 2)
                c[i - 1] = q[i];
            //1 шаг
            f[i] /= b[i];
            f1[i] /= b[i];//precision
            a[i - 1] /= b[i];
            //b[i] /= b[i];//
            //2 шаг b,c

            b[i - 1] -= c[i - 1] * a[i - 1];
            if (i != 2)
            {
                f[i - 1] -= c[i - 1] * f[i];
                f1[i - 1] -= c[i - 1] * f1[i];//precision
            }

            //c[i-1] = 0;

            //q
            q[i - 1] -= q[i] * a[i - 1];
            f[1] -= q[i] * f[i];
            f1[1] -= q[i] * f1[i];//precision
                                  //q[i] -= b[i]*q[i];//
                                  //p
            p[i - 1] -= p[i] * a[i - 1];
            f[0] -= p[i] * f[i];
            f1[0] -= p[i] * f1[i];//precision
                                  //p[i] -= p[i]*b[i];//
        }

        if (n > 0)
        {
            //c[1] = q[2];//
            //f[1] += q[2] * f[2];
            c[0] = p[1];

            a[0] /= b[1];
            f[1] /= b[1];
            f1[1] /= b[1];//precision
            q[0] = a[0];
            //b[1] = 1;

            b[0] -= c[0] * a[0];
            f[0] -= c[0] * f[1];
            f1[0] -= c[0] * f1[1];//precision
            p[0] = b[0];
            //p[1] = c[0] = 0;

        }

        f[0] /= b[0];
        f1[0] /= b[0];//precision
                      //b[0] /= b[0];//
                      //p[0]=b[0] = 1;



        //p[1] = c[0];
        //q[0] = a[0];
        //q[1] = b[1];
        //q[2] = c[1];
        double[] precisionX = new double[n];
        double[] solvedX = new double[n];
        solvedX[0] = f[0];
        precisionX[0] = f1[0];
        for (int i = 1; i < n; i++)
        {
            precisionX[i] = f1[i] - a[i - 1] * precisionX[i - 1];
            solvedX[i] = f[i] - a[i - 1] * solvedX[i - 1];
        }
        return (solvedX, precisionX);
    }
    static double[] CalculateRightSide(double[] a, double[] b, double[] c, double[] p, double[] q, double[] x)
    {
        int n = b.Length;
        double[] f = new double[n];
        for (int i = 0; i < n; i++)
        {
            f[i] = 0;
        }
        for (int i = 0; i < n; i++)
        {
            f[0] += p[i] * x[i];
        }
        for (int i = 0; i < n; i++)
        {
            f[1] += q[i] * x[i];
        }
        for (int j = 2; j < n - 1; j++)
        {
            f[j] = a[j - 1] * x[j - 1] + b[j] * x[j] + c[j] * x[j + 1];
        }
        f[n - 1] = a[n - 2] * x[n - 2] + b[n - 1] * x[n - 1];
        return f;
    }
    static double[] GenerateVector(int n, int e)
    {
        double[] arr = new double[n];
        Random random = new();
        for (int i = 0; i < n; i++)
        {
            double sign = random.Next(2) == 1 ? 1.0 : -1.0;
            arr[i] = sign * random.NextDouble() * e;
            //arr[i] = 1.0137 * random.Next(-e, e);
        }
        return arr;
    }
    static void WriteToFile(double[] info, string filename)
    {
        StreamWriter file = new(filename);
        for (int i = 0; i < info.Length; i++)
        {
            file.WriteLine($"x{i + 1} = {info[i]}");
        }
        file.Close();
    }
    static (double[] a, double[] b, double[] c, double[] f, double[] p, double[] q) ReadFromFile(int n, string filename)
    {
        StreamReader file = new(filename);

        double[] a, b, c, f, p, q, help;
        a = new double[n - 1];
        b = new double[n];
        c = new double[n - 1];
        f = new double[n];
        p = new double[n];
        q = new double[n];
        help = new double[n];

        for (int i = 0; i < n; i++)
        {
            var line = file.ReadLine();
            var nums = line.Split(' ', '\t');

            for (int j = 0; j < nums.Length; j++)
            {
                help[j] = Convert.ToDouble(nums[j]);
                if (i == 0)
                    p[j] = help[j];
                if (i == 1)
                    q[j] = help[j];
                if (j == i - 1)
                    a[i - 1] = help[j];
                else if (j == i)
                    b[i] = help[j];
                else if (j == i + 1 && i != n - 1)
                    c[i] = help[j];
            }
        }
        file.ReadLine();
        for (int i = 0; i < n; i++)
        {
            var tmp = file.ReadLine();
            f[i] = Convert.ToDouble(tmp);
        }
        file.Close();
        return (a, b, c, f, p, q);
    }
}