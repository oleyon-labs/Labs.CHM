using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.CHM.Lab5
{
    internal class KoshiSolver
    {
        public static void SolveRungeKutt(string data, Func<double,double,double> f, string res)
        {
            int icod = -1;
            //Парсинг входных значений из файла A,B,y0,H,eps
            var fileData = File.ReadAllLines(data);
            string[] line = fileData[0].Split(' ');
            double A = double.Parse(line[0]);
            double B = double.Parse(line[1]);
            double y0 = double.Parse(line[2]);
            line = fileData[1].Split(' ');
            double H = double.Parse(line[0]);
            double eps = double.Parse(line[1]);


            double diapason = Math.Abs(B - A);
            double Hmin = CountMachEpsilon() * diapason;

            double[] y = new double[2];
            double[] x = new double[2];
            double epsr = 0;

            int iterationCount;
            double H1=H;

            double epsrPrev = Double.MaxValue;

            while (icod==-1)
            {

                iterationCount = (int)Math.Ceiling(diapason / H1);
                H1 = diapason / iterationCount;


                for (int j = 0; j < 2; j++)
                {
                    y[j] = y0;
                    x[j] = A;
                    double newH = H1 / (j + 1);
                    iterationCount *= (j + 1);
                    for (double i = 0; i < iterationCount; i++)
                    {
                        double K1 = newH * f(x[j], y[j]);
                        double K2 = newH * f(x[j] + newH / 4, y[j] + K1 / 4);
                        double K3 = newH * f(x[j] + newH / 2, y[j] + K2 / 2);
                        double K4 = newH * f(x[j] + newH, y[j] + K1 - 2 * K2 + K3);
                        y[j] = y[j] + 1.0 / 6 * (K1 + 4 * K3 + K4);
                        x[j] = x[j] + newH;
                    }
                }

                epsr = Math.Abs((y[1] - y[0]) / 15);

                if (epsr > eps)
                {
                    if (epsr < epsrPrev)
                    {
                        double tH = H1 / 2 * Math.Pow(eps / epsr, 1.0 / 4);
                        if (H1 <= Hmin)
                            icod = 2;
                        else
                            H1 = tH;
                        epsrPrev = epsr;
                    }
                    else
                        icod = 1;
                }
                else
                {
                    icod = 0;
                }
            }







            var outputFile = new StreamWriter(res);
            //outputFile.WriteLine($"{epsr} {H1/2} {icod}");
            //outputFile.WriteLine($"{x[1]} {y[1]}");
            outputFile.WriteLine($"epsr={epsr} h={H1 / 2} icod={icod}");
            outputFile.WriteLine($"x={x[1]} y={y[1]}");
            outputFile.Close();
        }
        static double CountMachEpsilon()
        {
            double eps = 1;
            while ((1 + eps) > 1)
            {
                eps /= 2;
            }
            eps *= 2;
            return eps;
        }
    }
}
