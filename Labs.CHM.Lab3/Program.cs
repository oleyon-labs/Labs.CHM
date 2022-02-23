using System.Diagnostics;

namespace Labs.CHM.Lab3;
static class Program
{
    static void Main()
    {
        //Console.WriteLine("hello world!");
        int N = 100;
        double accuracy = 1e-7;
        int tests = 50;
        Console.WriteLine(Stopwatch.Frequency);
        Console.WriteLine(Stopwatch.IsHighResolution);
        double acc = 0;
        double acc2 = 0;
        int counter = 0;
        int counter2 = 0;

        double ticks1 = 0;
        double ticks2 = 0;
        for (int i = 0; i < tests; i++)
        {
            var res = GenerateMatrix(N);
            int count;
            Stopwatch sw = Stopwatch.StartNew();
            acc += CalculateAccuracy(Solve(res.matrix, accuracy, out count), res.eigenvalues);
            sw.Stop();

            int count2;
            Stopwatch sw2 = Stopwatch.StartNew();
            acc2 += CalculateAccuracy(Solvesymm(res.matrix, accuracy, out count2), res.eigenvalues);
            sw2.Stop();
            counter += count;
            counter2 += count2;
            ticks1 += sw.ElapsedTicks / 10000.0;
            ticks2 += sw2.ElapsedTicks / 10000.0;
            //CalculateAccuracy(Solvesymm(res.matrix, accuracy, out count), res.eigenvalues);
        }
        counter /= tests;
        acc /= tests;
        counter2 /= tests;
        acc2 /= tests;
        ticks1 /= tests;
        ticks2 /= tests;
        //Console.WriteLine($"accuracy = {acc}\navg count = {counter}");
        Console.WriteLine($"accuracy2 = {acc2}\navg count2 = {counter2}");
        //Console.WriteLine($"elapsed time 1 = {ticks1}");
        Console.WriteLine($"elapsed time2 = {ticks2}");
    }

    static double[,] GenerateRandomMatrix(int N, int M)
    {
        double[,] matrix = new double[N, M];
        Random random = new Random();
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                int sign = 1;
                if (random.Next(2) == 0)
                    sign = -1;
                matrix[i, j] = random.NextDouble() * 10 * sign;
            }
        }
        return matrix;
    }

    static double[,] NormalizeVector(double[,] vector)
    {
        int N = vector.GetLength(0);
        double[,] normalizedVector = new double[N, 1];
        double length = 0;
        for (int i = 0; i < N; i++)
        {
            length += vector[i, 0] * vector[i, 0];

        }
        length = Math.Sqrt(length);
        for (int i = 0; i < N; i++)
        {
            normalizedVector[i, 0] = vector[i, 0] / length;
        }
        return normalizedVector;
    }

    static double[,] Transpose(double[,] matrix)
    {
        int N = matrix.GetLength(0);
        int M = matrix.GetLength(1);

        double[,] transposedMatrix = new double[M, N];
        for (int i = 0; i < M; i++)
        {
            for (int j = 0; j < N; j++)
            {
                transposedMatrix[i, j] = matrix[j, i];
            }
        }
        return transposedMatrix;
    }

    static (double[,] matrix, double[,] eigenvalues, double[,] eigenvectors) GenerateMatrix(int N)//to do
    {

        double[,] matrix = new double[N, N];
        Random r = new Random();
        //генерируем Л
        bool good = false;
        while (!good)
        {
            for (int i = 0; i < N; i++)
            {
                int sign = 1;
                if (r.Next(0, 2) == 0)
                    sign = -1;
                matrix[i, i] = r.NextDouble() * 50 * sign;
            }
            good = true;
            for (int i = 0; i < N; i++)
            {
                for (int k = i + 1; k < N; k++)
                {
                    if (matrix[i, i] == matrix[k, k])
                        good = false;
                }
            }
        }
        double[,] eigenvalues = new double[N, 1];
        for (int i = 0; i < N; i++)
        {
            eigenvalues[i, 0] = matrix[i, i];
        }

        //генерируем w
        double[,] w = NormalizeVector(GenerateRandomMatrix(N, 1));
        double[,] H = Substract(GenerateIdentityMatrix(N), Multiply(Multiply(w, Transpose(w)), 2));

        matrix = Multiply(Multiply(H, matrix), H);

        return (matrix, eigenvalues, H);
    }
    static double[,] GenerateIdentityMatrix(int N)
    {
        double[,] matrix = new double[N, N];
        for (int i = 0; i < N; i++)
        {
            matrix[i, i] = 1;
        }
        return matrix;
    }
    static double[,] Substract(double[,] matrix1, double[,] matrix2)
    {
        int N = matrix1.GetLength(0);
        int M = matrix1.GetLength(1);
        double[,] result = new double[N, M];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                result[i, j] = matrix1[i, j] - matrix2[i, j];
            }
        }
        return result;
    }
    static double[,] Multiply(double[,] A, double[,] B)
    {
        if (A.GetLength(1) != B.GetLength(0))
            throw new Exception("Bad matrix dimensions");
        double[,] result = new double[A.GetLength(0), B.GetLength(1)];
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                result[i, j] = 0;
                for (int t = 0; t < A.GetLength(1); t++)
                {
                    result[i, j] += A[i, t] * B[t, j];
                }
            }
        }
        return result;
    }
    static double[,] Multiply(double[,] matrix, double number)
    {
        int N = matrix.GetLength(0);
        int M = matrix.GetLength(1);
        double[,] result = new double[N, M];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                result[i, j] = matrix[i, j] * number;
            }
        }
        return result;
    }
    static void PrintMatrix(double[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(Math.Round(matrix[i, j], 3) + "\t");
            }
            Console.WriteLine();
        }
    }
    static double CalculateAccuracy(double[,] vector, double[,] accurateVector)
    {
        double accuracy = 0;
        double[] vector1 = new double[vector.GetLength(0)];
        double[] accurateVector1 = new double[vector.GetLength(0)];
        for (int i = 0; i < vector.GetLength(0); i++)
        {
            vector1[i] = vector[i, 0];
            accurateVector1[i] = accurateVector[i, 0];
        }
        Array.Sort(vector1);
        Array.Sort(accurateVector1);
        for (int i = 0; i < vector.GetLength(0); i++)
        {
            double currentAcc = Math.Abs(vector1[i] - accurateVector1[i]);
            if (currentAcc > accuracy)
                accuracy = currentAcc;
        }
        return accuracy;
    }
    static double CalculateAccuracy(double[] vector, double[,] accurateVector)
    {
        double accuracy = 0;
        double[] vector1 = new double[vector.GetLength(0)];
        double[] accurateVector1 = new double[vector.GetLength(0)];
        for (int i = 0; i < vector.GetLength(0); i++)
        {
            vector1[i] = vector[i];
            accurateVector1[i] = accurateVector[i, 0];
        }
        Array.Sort(vector1);
        Array.Sort(accurateVector1);
        for (int i = 0; i < vector.GetLength(0); i++)
        {
            double currentAcc = Math.Abs(vector1[i] - accurateVector1[i]);
            if (currentAcc > accuracy)
                accuracy = currentAcc;
        }
        return accuracy;
    }
    static double[] Solve(double[,] A, double accuracy, out int counter)
    {
        int N = A.GetLength(0);
        double[,] newA = (double[,])A.Clone();


        bool solved = false;
        int count = 0;
        while (!solved && count < 100000)
        {
            count++;
            //Console.WriteLine(count);
            //ищем недиагональный максимальный по модулю элемент
            int imax = 1, jmax = 0;
            for (int i = 1; i < N; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (Math.Abs(newA[i, j]) >= Math.Abs(newA[imax, jmax]))
                    {
                        imax = i;
                        jmax = j;
                    }
                }
            }
            if (Math.Abs(newA[imax, jmax]) < accuracy)
            {
                solved = true;
            }
            else
            {
                //Console.WriteLine(newA[imax, jmax]);
                double p = 2 * newA[imax, jmax];
                double q = newA[imax, imax] - newA[jmax, jmax];
                double d = Math.Sqrt(p * p + q * q);
                double c, s;
                if (q == 0)
                    c = s = Math.Sqrt(2) / 2;
                else
                {
                    double r = Math.Abs(q) / (2 * d);
                    c = Math.Sqrt(0.5 + r);
                    s = Math.Sqrt(0.5 - r) * Math.Sign(p * q);
                }
                for (int i = 0; i < N; i++)
                {
                    double aimax = newA[imax, i];
                    double ajmax = newA[jmax, i];
                    newA[imax, i] = c * aimax + s * ajmax;
                    newA[jmax, i] = -s * aimax + c * ajmax;
                }
                for (int i = 0; i < N; i++)
                {
                    double aimax = newA[i, imax];
                    double ajmax = newA[i, jmax];
                    newA[i, imax] = c * aimax + s * ajmax;
                    newA[i, jmax] = -s * aimax + c * ajmax;
                }
                newA[imax, jmax] = 0;
                newA[jmax, imax] = 0;

                //for (int i = 0; i < N; i++)
                //{
                //    int mini = Math.Min(i, imax);
                //    int maxi = Math.Max(i, imax);
                //    int minj = Math.Min(i, jmax);
                //    int maxj = Math.Max(i, jmax);
                //    double aimax = newA[maxi, mini];
                //    double ajmax = newA[maxj, minj];
                //    if (i != imax&&i!=jmax)
                //    {
                //        newA[maxi, mini] = c * aimax + s * ajmax;
                //        newA[maxj, minj] = -s * aimax + c * ajmax;
                //    }

                //    //newA[mini, maxi] = c * aimax + s * ajmax;
                //    //newA[minj, maxj] = -s * aimax + c * ajmax;
                //}

                //double aimaxn = newA[imax, imax];
                //double ajmaxn = newA[jmax, jmax];
                //double aij = newA[imax, jmax];//красный
                //newA[imax, imax] = c * aimaxn + s * aij;
                //newA[jmax, jmax] = -s * aij + c * ajmaxn;
                //aij = c * aij + s * ajmaxn;
                //newA[imax, imax] = c * aimaxn + s * aij;
                //newA[jmax, jmax] = -s * aij + c * ajmaxn;
                //newA[imax, jmax] = 0;
            }
        }
        //double[,] newEi = new double[N, 1];
        //for (int j = 0; j < N; j++)
        //{
        //    newEi[j, 0] = newA[j, j];
        //}
        double[] newEi = new double[N];
        for (int j = 0; j < N; j++)
        {
            newEi[j] = newA[j, j];
        }
        //Console.WriteLine($"count = {count}");
        counter = count;
        return newEi;
    }
    static double[,] Solve1(double[,] A, double accuracy)
    {
        int N = A.GetLength(0);
        double[,] newA = (double[,])A.Clone();


        bool solved = false;
        int count = 0;
        while (!solved && count < 10000000)
        {
            count++;
            //Console.WriteLine(count);
            //ищем недиагональный максимальный по модулю элемент
            int imax = 1, jmax = 0;
            solved = true;
            for (int k = 1; k < N; k++)
            {
                for (int l = 0; l < k; l++)
                {
                    imax = k;
                    jmax = l;
                    if (Math.Abs(newA[imax, jmax]) > accuracy)
                    {
                        solved = false;
                    }
                    else
                    {
                        //Console.WriteLine(newA[imax, jmax]);
                        double p = 2 * newA[imax, jmax];
                        double q = newA[imax, imax] - newA[jmax, jmax];
                        double d = Math.Sqrt(p * p + q * q);
                        double c, s;
                        if (q == 0)
                            c = s = Math.Sqrt(2) / 2;
                        else
                        {
                            double r = Math.Abs(q) / (2 * d);
                            c = Math.Sqrt(0.5 + r);
                            s = Math.Sqrt(0.5 - r) * Math.Sign(p * q);
                            //if (Math.Sign(p * q) == 0 || c == 0 || s == 0)
                            //    throw new Exception("gf");

                        }
                        for (int i = 0; i < N; i++)
                        {
                            double aimax = newA[imax, i];
                            double ajmax = newA[jmax, i];
                            newA[imax, i] = c * aimax + s * ajmax;
                            newA[jmax, i] = -s * aimax + c * ajmax;
                        }
                        for (int i = 0; i < N; i++)
                        {
                            double aimax = newA[i, imax];
                            double ajmax = newA[i, jmax];
                            newA[i, imax] = c * aimax + s * ajmax;
                            newA[i, jmax] = -s * aimax + c * ajmax;
                        }
                    }

                }
            }

        }
        double[,] newEi = new double[N, 1];
        for (int j = 0; j < N; j++)
        {
            newEi[j, 0] = newA[j, j];
        }
        Console.WriteLine($"count = {count}");
        return newEi;
    }

    static double[,] Solve3(double[,] A, double accuracy)
    {
        int N = A.GetLength(0);
        double[,] newA = (double[,])A.Clone();


        bool solved = false;
        int count = 0;
        while (!solved && count < 10000000)
        {
            count++;
            //Console.WriteLine(count);
            //ищем недиагональный максимальный по модулю элемент
            int imax = 1, jmax = 0;
            for (int i = 1; i < N; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (Math.Abs(newA[i, j]) > Math.Abs(newA[imax, jmax]))
                    {
                        imax = i;
                        jmax = j;
                    }
                }
            }
            if (Math.Abs(newA[imax, jmax]) < accuracy)
            {
                solved = true;
            }
            else
            {
                //Console.WriteLine(newA[imax, jmax]);
                //double p = 2 * newA[imax, jmax];
                //double q = newA[imax, imax] - newA[jmax, jmax];
                //double d = Math.Sqrt(p * p + q * q);
                double tan2a;
                tan2a = 2 * newA[imax, jmax] / (newA[jmax, jmax] - newA[imax, imax]);
                double c, s;
                c = Math.Cos(Math.Atan(tan2a) / 2);
                s = Math.Sin(Math.Atan(tan2a) / 2);

                //if (q == 0)
                //    c = s = Math.Sqrt(2) / 2;
                //else
                //{
                //    double r = Math.Abs(q) / (2 * d);
                //    c = Math.Sqrt(0.5 + r);
                //    s = Math.Sqrt(0.5 - r) * Math.Sign(p * q);
                //}
                for (int i = 0; i < N; i++)
                {
                    double aimax = newA[imax, i];
                    double ajmax = newA[jmax, i];
                    newA[imax, i] = c * aimax + s * ajmax;
                    newA[jmax, i] = -s * aimax + c * ajmax;
                }
                for (int i = 0; i < N; i++)
                {
                    double aimax = newA[i, imax];
                    double ajmax = newA[i, jmax];
                    newA[i, imax] = c * aimax + s * ajmax;
                    newA[i, jmax] = -s * aimax + c * ajmax;
                }
            }
        }
        double[,] newEi = new double[N, 1];
        for (int j = 0; j < N; j++)
        {
            newEi[j, 0] = newA[j, j];
        }
        Console.WriteLine($"count = {count}");
        return newEi;
    }

    static double[] Solvesymm(double[,] A, double accuracy, out int counter)
    {
        int N = A.GetLength(0);
        double[,] newA = (double[,])A.Clone();

        bool solved = false;
        int count = 0;
        double[] newEi = new double[N];
        for (int j = 0; j < N; j++)
        {
            newEi[j] = newA[j, j];
        }
        while (!solved && count < 100000)
        {
            count++;
            //ищем недиагональный максимальный по модулю элемент
            int imax = 1, jmax = 0;
            for (int i = 1; i < N; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (Math.Abs(newA[i, j]) >= Math.Abs(newA[imax, jmax]))
                    {
                        imax = i;
                        jmax = j;
                    }
                }
            }
            if (Math.Abs(newA[imax, jmax]) < accuracy)
            {
                solved = true;
            }
            else
            {
                double p = 2 * newA[imax, jmax];
                double q = newEi[imax] - newEi[jmax];
                double d = Math.Sqrt(p * p + q * q);
                double c, s;
                if (q == 0)
                    c = s = Math.Sqrt(2) / 2;
                else
                {
                    double r = Math.Abs(q) / (2 * d);
                    c = Math.Sqrt(0.5 + r);
                    s = Math.Sqrt(0.5 - r) * Math.Sign(p * q);
                }

                for (int i = 0; i < N; i++)
                {
                    int mini = Math.Min(i, imax);
                    int maxi = Math.Max(i, imax);
                    int minj = Math.Min(i, jmax);
                    int maxj = Math.Max(i, jmax);
                    double aimax = newA[maxi, mini];
                    double ajmax = newA[maxj, minj];
                    if (i != imax && i != jmax)
                    {
                        newA[maxi, mini] = c * aimax + s * ajmax;
                        newA[maxj, minj] = -s * aimax + c * ajmax;
                    }
                }

                double aimaxn = newEi[imax];
                double ajmaxn = newEi[jmax];
                double aij = newA[imax, jmax];
                newEi[jmax] = -s * aij + c * ajmaxn;
                newEi[imax] = c * aimaxn + s * aij;
                double aij2 = -s * aimaxn + c * aij;
                double aij1 = c * aij + s * ajmaxn;
                aimaxn = newEi[imax];
                ajmaxn = newEi[jmax];

                newEi[imax] = c * aimaxn + s * aij1;
                newEi[jmax] = -s * aij2 + c * ajmaxn;
                newA[imax, jmax] = 0;
            }
        }
        counter = count;
        return newEi;
    }

}