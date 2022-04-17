using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet;

namespace Labs.CHM.Lab6
{
    internal static class ShootingSolver
    {
        public static void Solve(string data, Func<double, double, double, double> f, string res)
        {
            var Derivative = MathNet.Numerics.Differentiate.FirstPartialDerivative;
            int icod = -1;
            //Парсинг входных значений из файла A,B,y0,H,eps
            var fileData = File.ReadAllLines(data);
            string[] line = fileData[0].Split(' ');
            double a = double.Parse(line[0]); //left x
            double b = double.Parse(line[1]); //right x
            double A = double.Parse(line[2]); //first condition
            double B = double.Parse(line[3]); //second condition
            int N = int.Parse(line[4]); //number of intervals
            double eps = double.Parse(line[5]); //tolerance
            double M = double.Parse(line[6]); //maximum number of iterations

            double TK = double.Parse(line[7]); //initial value of parameter


            double[,] w = new double[2, N+1];
            double[,] k = new double[4, 2];
            double[,] k1 = new double[4, 2];
            double u1, u2;

            Func<double[], double> func = (p) => f(p[0], p[1], p[2]);

            //step 1
            double h = (b - a) / (N);
            int kc = 1;
            TK = TK;
            //step 2
            while (kc <= M) //steps 3-10
            {
                //step 3
                w[0, 0] = A;
                w[1, 0] = TK;
                u1= 0;
                u2 = 1;
                //step 4
                for (int i = 1; i <= N; i++) //steps 5-6
                {
                    //step 5
                    double x = a + (i - 1) * h;
                    //step 6
                    k[0, 0] = h * w[1, i - 1];
                    k[0, 1] = h * f(x, w[0, i - 1], w[1, i - 1]);
                    k[1, 0] = h * (w[1, i - 1] + 1.0 / 2.0 * k[0, 1]);
                    k[1, 1] = h * f(x + h / 2, w[0, i - 1] + 1.0 / 2 * k[0, 0], w[1, i - 1] + 1.0 / 2 * k[0, 1]);
                    k[2, 0] = h * (w[1, i - 1] + 1.0 / 2 * k[1, 1]);
                    k[2, 1] = h * f(x + h / 2, w[0, i - 1] + 1.0 / 2 * k[1, 0], w[1, i - 1] + 1.0 / 2 * k[1, 1]);
                    k[3, 0] = h * (w[1, i - 1] + k[2, 1]);
                    k[3, 1] = h * f(x + h, w[0, i - 1] + k[2, 0], w[1, i - 1] + k[2, 1]);
                    w[0, i] = w[0, i - 1] + (k[0, 0] + 2 * k[1, 0] + 2 * k[2, 0] + k[3, 0]) / 6.0;
                    w[1, i] = w[1, i - 1] + (k[0, 1] + 2 * k[1, 1] + 2 * k[2, 1] + k[3, 1]) / 6.0;
                    k1[0, 0] = h * u2;
                    k1[0, 1] = h * (Derivative(func, new double[] { x, w[0, i - 1], w[1, i - 1] }, 1) * u1 
                        + Derivative(func, new double[] { x, w[0, i - 1], w[1, i - 1] }, 2) * u2);
                    k1[1, 0] = h * (u2 + 1.0 / 2 * k1[0, 1]);
                    k1[1,1]= h * (Derivative(func, new double[] { x+h/2, w[0, i - 1], w[1, i - 1] }, 1) * (u1+1.0/2*k1[0,0])
                        + Derivative(func, new double[] { x+h/2, w[0, i - 1], w[1, i - 1] }, 2) * (u2+1.0/2*k1[0,1]));
                    k1[2, 0] = h * (u2 + 1.0 / 2 * k[1, 1]);
                    k1[2,1]= h * (Derivative(func, new double[] { x + h / 2, w[0, i - 1], w[1, i - 1] }, 1) * (u1 + 1.0 / 2 * k1[1, 0])
                        + Derivative(func, new double[] { x + h / 2, w[0, i - 1], w[1, i - 1] }, 2) * (u2 + 1.0 / 2 * k1[1, 1]));
                    k[3, 0] = h * (u2 + k1[2, 1]);
                    k[3,1]= h * (Derivative(func, new double[] { x + h, w[0, i - 1], w[1, i - 1] }, 1) * (u1 + k1[2, 0])
                        + Derivative(func, new double[] { x + h, w[0, i - 1], w[1, i - 1] }, 2) * (u2 + k1[2, 1]));
                    u1 = u1 + (k1[0, 0] + 2 * k1[1, 0] + 2 * k1[2, 0] + k1[3, 0]) / 6;
                    u2 = u2 + (k1[0, 1] + 2 * k1[1, 1] + 2 * k1[2, 1] + k1[3, 1]) / 6;
                }
                //step 7
                if(Math.Abs(w[0,N]-B)<=eps)//steps 8-9
                {
                    //step 8
                    for (int j = 0; j <= N; j++)
                    {
                        double x=a+j*h;
                        Console.WriteLine($"{x}; {w[0,j]}; {w[1,j]}");
                    }
                    //step 9
                    return;
                }
                //step 10
                TK = TK - (w[0, N] - B) / u1;
                kc++;
            }
            //step 11
            Console.WriteLine("Maximum number of iterations exceeded");
            return;
        }
        public static void Solve(string data, Func<double, double, double, double> f, Func<double, double, double, double> fy, Func<double, double, double, double> fy1, string res)
        {
            var Derivative = MathNet.Numerics.Differentiate.FirstPartialDerivative;
            int icod = -1;
            //Парсинг входных значений из файла A,B,y0,H,eps
            var fileData = File.ReadAllLines(data);
            string[] line = fileData[0].Split(' ');
            double a = double.Parse(line[0]); //left x
            double b = double.Parse(line[1]); //right x
            double A = double.Parse(line[2]); //first condition
            double B = double.Parse(line[3]); //second condition
            int N = int.Parse(line[4]); //number of intervals
            double eps = double.Parse(line[5]); //tolerance
            double M = double.Parse(line[6]); //maximum number of iterations

            double TK = double.Parse(line[7]); //initial value of parameter


            double[,] w = new double[2, N + 1];
            double[,] k = new double[4, 2];
            double[,] k1 = new double[4, 2];
            double u1, u2;

            Func<double[], double> func = (p) => f(p[0], p[1], p[2]);

            //step 1
            double h = (b - a) / (N);
            int kc = 1;
            TK = TK;
            //step 2
            while (kc <= M) //steps 3-10
            {
                //step 3
                w[0, 0] = A;
                w[1, 0] = TK;
                u1 = 0;
                u2 = 1;
                //step 4
                for (int i = 1; i <= N; i++) //steps 5-6
                {
                    //step 5
                    double x = a + (i - 1) * h;
                    //step 6
                    k[0, 0] = h * w[1, i - 1];
                    k[0, 1] = h * f(x, w[0, i - 1], w[1, i - 1]);
                    k[1, 0] = h * (w[1, i - 1] + 1.0 / 2.0 * k[0, 1]);
                    k[1, 1] = h * f(x + h / 2, w[0, i - 1] + 1.0 / 2 * k[0, 0], w[1, i - 1] + 1.0 / 2 * k[0, 1]);
                    k[2, 0] = h * (w[1, i - 1] + 1.0 / 2 * k[1, 1]);
                    k[2, 1] = h * f(x + h / 2, w[0, i - 1] + 1.0 / 2 * k[1, 0], w[1, i - 1] + 1.0 / 2 * k[1, 1]);
                    k[3, 0] = h * (w[1, i - 1] + k[2, 1]);
                    k[3, 1] = h * f(x + h, w[0, i - 1] + k[2, 0], w[1, i - 1] + k[2, 1]);
                    w[0, i] = w[0, i - 1] + (k[0, 0] + 2 * k[1, 0] + 2 * k[2, 0] + k[3, 0]) / 6.0;
                    w[1, i] = w[1, i - 1] + (k[0, 1] + 2 * k[1, 1] + 2 * k[2, 1] + k[3, 1]) / 6.0;
                    k1[0, 0] = h * u2;
                    k1[0, 1] = h * (fy(x, w[0, i - 1], w[1, i - 1]) * u1
                        + fy1(x, w[0, i - 1], w[1, i - 1]) * u2);
                    k1[1, 0] = h * (u2 + 1.0 / 2 * k1[0, 1]);
                    k1[1, 1] = h * (fy(x + h / 2, w[0, i - 1], w[1, i - 1]) * (u1 + 1.0 / 2 * k1[0, 0])
                        + fy1(x + h / 2, w[0, i - 1], w[1, i - 1]) * (u2 + 1.0 / 2 * k1[0, 1]));
                    k1[2, 0] = h * (u2 + 1.0 / 2 * k[1, 1]);
                    k1[2, 1] = h * (fy(x + h / 2, w[0, i - 1], w[1, i - 1]) * (u1 + 1.0 / 2 * k1[1, 0])
                        + fy1(x + h / 2, w[0, i - 1], w[1, i - 1]) * (u2 + 1.0 / 2 * k1[1, 1]));
                    k[3, 0] = h * (u2 + k1[2, 1]);
                    k[3, 1] = h * (fy(x + h, w[0, i - 1], w[1, i - 1]) * (u1 + k1[2, 0])
                        + fy1(x + h, w[0, i - 1], w[1, i - 1]) * (u2 + k1[2, 1]));
                    u1 = u1 + (k1[0, 0] + 2 * k1[1, 0] + 2 * k1[2, 0] + k1[3, 0]) / 6;
                    u2 = u2 + (k1[0, 1] + 2 * k1[1, 1] + 2 * k1[2, 1] + k1[3, 1]) / 6;
                }
                //step 7
                if (Math.Abs(w[0, N] - B) <= eps)//steps 8-9
                {
                    //step 8
                    for (int j = 0; j <= N; j++)
                    {
                        double x = a + j * h;
                        Console.WriteLine($"{x}; {w[0, j]}; {w[1, j]}");
                    }
                    //step 9
                    return;
                }
                //step 10
                TK = TK - (w[0, N] - B) / u1;
                kc++;
            }
            //step 11
            Console.WriteLine("Maximum number of iterations exceeded");
            return;
        }
        /*
         public static void Solve(string data, Func<double, double, double, double> f, string res)
        {
            var Derivative = MathNet.Numerics.Differentiate.FirstPartialDerivative;
            int icod = -1;
            //Парсинг входных значений из файла A,B,y0,H,eps
            var fileData = File.ReadAllLines(data);
            string[] line = fileData[0].Split(' ');
            double a = double.Parse(line[0]); //left x
            double b = double.Parse(line[1]); //right x
            double A = double.Parse(line[2]); //first condition
            double B = double.Parse(line[3]); //second condition
            int N = int.Parse(line[4]); //number of intervals
            double eps = double.Parse(line[5]); //tolerance
            double M = double.Parse(line[6]); //maximum number of iterations

            double TK = double.Parse(line[7]); //initial value of parameter


            double[,] w = new double[2, N];
            double[,] k = new double[4, 2];
            double[,] k1 = new double[4, 2];
            double u1, u2;

            Func<double[], double> func = (p) => f(p[0], p[1], p[2]);

            //step 1
            double h = (b - a) / N;
            int kc = 1;
            TK = TK;
            //step 2
            while (kc <= M) //steps 3-10
            {
                //step 3
                w[1, 0] = A;
                w[2, 0] = TK;
                u1= 0;
                u2 = 1;
                //step 4
                for (int i = 1; i < N; i++) //steps 5-6
                {
                    //step 5
                    double x = a + (i - 1) * h;
                    //step 6
                    k[1, 1] = h * w[2, i - 1];
                    k[1, 2] = h * f(x, w[1, i - 1], w[2, i - 1]);
                    k[2, 1] = h * (w[2, i - 1] + 1.0 / 2.0 * k[1, 2]);
                    k[2, 2] = h * f(x + h / 2, w[1, i - 1] + 1.0 / 2 * k[1, 1], w[2, i - 1] + 1.0 / 2 * k[1, 2]);
                    k[3, 1] = h * (w[2, i - 1] + 1.0 / 2 * k[2, 2]);
                    k[3, 2] = h * f(x + h / 2, w[1, i - 1] + 1.0 / 2 * k[2, 1], w[2, i - 1] + 1.0 / 2 * k[2, 2]);
                    k[4, 1] = h * (w[2, i - 1] + k[3, 2]);
                    k[4, 2] = h * f(x + h, w[1, i - 1] + k[3, 1], w[2, i - 1] + k[3, 2]);
                    w[1, i] = w[1, i - 1] + (k[1, 1] + 2 * k[2, 1] + 2 * k[3, 1] + k[4, 1]) / 6.0;
                    w[2, i] = w[2, i - 1] + (k[1, 2] + 2 * k[2, 2] + 2 * k[3, 2] + k[4, 2]) / 6.0;
                    k1[1, 1] = h * u2;
                    k1[1, 2] = h * (Derivative(func, new double[] { x, w[1, i - 1], w[2, i - 1] }, 1) * u1 
                        + Derivative(func, new double[] { x, w[1, i - 1], w[2, i - 1] }, 2) * u2);
                    k1[2, 1] = h * (u2 + 1.0 / 2 * k1[1, 2]);
                    k1[2,2]= h * (Derivative(func, new double[] { x+h/2, w[1, i - 1], w[2, i - 1] }, 1) * (u1+1.0/2*k1[1,1])
                        + Derivative(func, new double[] { x+h/2, w[1, i - 1], w[2, i - 1] }, 2) * (u2+1.0/2*k1[1,2]));
                    k1[3, 1] = h * (u2 + 1.0 / 2 * k[2, 2]);
                    k1[3,2]= h * (Derivative(func, new double[] { x + h / 2, w[1, i - 1], w[2, i - 1] }, 1) * (u1 + 1.0 / 2 * k1[2, 1])
                        + Derivative(func, new double[] { x + h / 2, w[1, i - 1], w[2, i - 1] }, 2) * (u2 + 1.0 / 2 * k1[2, 2]));
                    k[4, 1] = h * (u2 + k1[3, 2]);
                    k[4,2]= h * (Derivative(func, new double[] { x + h, w[1, i - 1], w[2, i - 1] }, 1) * (u1 + k1[3, 1])
                        + Derivative(func, new double[] { x + h, w[1, i - 1], w[2, i - 1] }, 2) * (u2 + k1[3, 2]));
                    u1 = u1 + (k1[1, 1] + 2 * k1[2, 1] + 2 * k1[3, 1] + k1[4, 1]) / 6;
                    u2 = u2 + (k1[1, 2] + 2 * k1[2, 2] + 2 * k1[3, 2] + k1[4, 2]) / 6;
                }
                //step 7
                if(Math.Abs(w[1,N]-B)<=eps)//steps 8-9
                {
                    //step 8
                    for (int j = 0; j < N; j++)
                    {
                        double x=a+j*h;
                        Console.WriteLine($"{x}, {w[1,j]}, {w[2,j]}");
                    }
                    //step 9
                    return;
                }
                //step 10
                TK = TK - (w[1, N] - B) / u1;
                kc++;
            }
            //step 11
            Console.WriteLine("Maximum number of iterations exceeded");
            return;
        }
        */
    }
}
