using System;

namespace Labs.CHM.Lab2;
class Program
{
    static void Main(string[] args)
    {
        int N, L;
        Console.WriteLine("Введите N");
        N = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Введите L");
        L = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Введите K");
        int K = Convert.ToInt32(Console.ReadLine());
        double totalPrecision = 0;
        int testCount = 100;

        for (int i = 0; i < testCount; i++)
        {

            double[,] matrix = new double[,] { {1,4,2 },
                                                {5,1,13 },
                                                {4,-2,-4 },
                                                {7,2,-1 },
                                                {8,4,0 },
                                                {10,0,0 }};
            //double[,] matrix = new double[,]
            //{
            //    {16,-4,-8,0 },
            //    {-4,17,-2,8 },
            //    {-8,-2,14,1 },
            //    {0,8,1,6 }
            //};
            double[,] matrix2 = new double[,]
            {
                {16,-4 },
                {17,-2 },
                {14,1 },
                {6,0 }
            };
            double[] f = new double[] { 7, -4, 12, -6, 3, -5 };
            double[] f2 = new double[] { 2, 5, -1, 3 };
            //double[] x = SolveSymmetric(N, L, matrix, CalculateRightSide(matrix)/*f*/);
            //double[,] matrixGen = GenerateBadMatrix(N, L, 10, K);
            double[,] matrixGen = GenerateMatrix(N, L, 10);
            double[] x = SolveSymmetric(N, L, matrixGen, CalculateRightSide(matrixGen)/*f*/);
            //for (int i = 0; i < x.Length; i++)
            //{
            //    Console.WriteLine($"x{i + 1} = {x[i]}");
            //}
            totalPrecision += CalculatePrecision(x);
        }
        Console.WriteLine("precision = " + totalPrecision / testCount);
    }
    //static double[] SolveSymmetric2(int N, int L, double[,] a, double[] f)
    //{
    //    double[] x = new double[N];

    //    double[,] b = new double[N, L];
    //    for(int i=0;i<N;i++)
    //    {
    //        for (int j = i; j < RowSize(i, L, N); j++)
    //        {
    //            b[i, j] = a[i, j];
    //            double sum = 0;
    //            for (int k = 0; k < j - 1; k++)
    //            {
    //                sum += b[i, k] * b[j, k] / b[k, k];
    //            }
    //            b[i, j] -= sum;
    //        }
    //    }
    //    double[] y = new double[N];

    //    for(int i=0;i<N;i++)
    //    {
    //        y[i] = f[i];
    //        for(int j=1;j < RowSize(Bi(i, N), L, N); j++)
    //        {
    //            y[i] -= b[Bi(i, N), Bj(j, L)] * y[i - j];
    //        }
    //        y[i] /= b[Bi(i, N), Bj(0, L)];
    //    }

    //    for (int i = N-1; i >= 0; i--)
    //    {
    //        x[i] = y[i];
    //        for (int j = 1; j < RowSize(i, L, N); j++)
    //        {
    //            x[i] -= b[i, j] / b[i,0] * x[i + j];
    //        }
    //        x[i] /= b[i, 0];
    //    }

    //    return x;
    //}
    static double[] SolveSymmetric(int N, int L, double[,] a, double[] f)
    {
        double[] x = new double[N];
        double[] y = new double[N];
        double[,] b = new double[N, L];

        for (int i = 0; i < N; i++)
        {
            for (int j = LeftBorder(i, L); j < L; j++)
            {
                b[i, j] = a[i + j + 1 - L, L - 1 - j];
                for (int k = LeftBorder(i, L); k < j; k++)
                {
                    b[i, j] -= b[i, k] * b[i + j + 1 - L, k + L - 1 - j] / b[i - L + k + 1, L - 1];
                }
            }
        }
        for (int i = 0; i < N; i++)
        {
            y[i] = f[i];
            for (int j = 1; j < L - LeftBorder(i, L); j++)
            {
                y[i] -= y[i - j] * b[i, L - 1 - j];
            }
            y[i] /= b[i, L - 1];
        }
        for (int i = N - 1; i >= 0; i--)
        {
            x[i] = 0;
            for (int j = 1; j < L - LeftBorder(N - 1 - i, L); j++)
            {
                x[i] -= x[i + j] * b[i + j, L - 1 - j];
            }
            x[i] /= b[i, L - 1];
            x[i] += y[i];
        }
        return x;
    }
    static int LeftBorder(int i, int L)
    {
        return Math.Max(0, L - i - 1);
    }
    static int RightBorder(int i, int L, int N)
    {
        return Math.Min(L - 1, N - i - 1);
    }
    static int K0(int i, int L)
    {
        return Math.Max(0, i - L + 1);
    }
    static int KN(int i, int L, int N)
    {
        return Math.Min(i + L - 1, N - 1);
    }
    static int RowSize(int i, int L, int N)
    {
        return Math.Min(L, N - i);
    }
    static double[] CalculateRightSide(double[,] matrix/*, double[] x*/)
    {
        int N = matrix.GetLength(0);
        int L = matrix.GetLength(1);
        double[] f = new double[N];
        for (int i = 0; i < N; i++)
        {
            f[i] = 0;
            for (int j = 0; j <= RightBorder(i, L, N); j++)
            {
                f[i] += matrix[i, j];
            }
            for (int j = 1; i - j >= 0 && j < L; j++)
            {
                f[i] += matrix[i - j, j];
            }
        }
        return f;
    }
    static double CalculatePrecision(double[] x)
    {
        double precision = 0;
        foreach (double val in x)
        {
            if (precision < Math.Abs(val - 1))
            {
                precision = Math.Abs(val - 1);
            }
        }
        return precision;
    }
    static double[,] GenerateMatrix(int N, int L, int diapason)
    {
        double[,] matrix = new double[N, L];
        Random gen = new Random();
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= RightBorder(i, L, N); j++)
            {
                double sign = gen.Next(0, 2) == 0 ? -1 : 1;

                matrix[i, j] = sign * gen.NextDouble() * diapason;
            }
        }
        return matrix;
    }
    static double[,] GenerateBadMatrix(int N, int L, int diapason, int koeff)
    {
        double[,] matrix = new double[N, L];
        Random gen = new Random();
        for (int i = 0; i < N; i++)
        {

            for (int j = 1; j <= RightBorder(i, L, N); j++)
            {
                double sign = gen.Next(0, 2) == 0 ? -1 : 1;

                matrix[i, j] = sign * gen.NextDouble() * diapason;
            }
            matrix[i, 0] = gen.NextDouble() * diapason / koeff;
        }

        return MultiplyMatrixOnItself(matrix, N, L);
    }
    static double[,] MultiplyMatrixOnItself(double[,] matrix, int N, int L)
    {
        double[,] newMatrix = new double[N, L];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= RightBorder(i, L, N); j++)
            {
                newMatrix[i, j] = 0;
                for (int k = 0; k < L - j && i - k >= 0; k++)
                {
                    newMatrix[i, j] += matrix[i - k, k] * matrix[i - k, j + k];
                }
            }
        }
        return newMatrix;
    }

    //static (int, int) SymmetricTranslationA(int i, int j)
    //{

    //}
}