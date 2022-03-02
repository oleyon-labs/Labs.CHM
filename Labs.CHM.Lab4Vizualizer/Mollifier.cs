namespace Labs.CHM.Lab4Vizualizer
{
    internal class Mollifier
    {
        public static (double[] smoothedPoints, int IER) Mollify(double[] points, int count)
        {
            int N = count;
            double[] smoothedPoints = new double[points.Length];
            if (N < 5)
                return (points, 2);
            for (int i = 2; i < N - 2; i++)
            {
                double newY = (-3) * points[i - 2] + (12) * points[i - 1] + (17) * points[i] + (12) * points[i + 1] + (-3) * points[i + 2];
                smoothedPoints[i] = newY / 35.0;
            }
            smoothedPoints[0] = points[0];
            smoothedPoints[N - 1] = points[N - 1];
            smoothedPoints[N - 2] = ((2) * points[N - 5] + (-8) * points[N - 4] + (12) * points[N - 3] + (27) * points[N - 2] + (2) * points[N - 1]) / 35.0;
            smoothedPoints[1] = ((2) * points[0] + (27) * points[1] + (12) * points[2] + (-8) * points[3] + (2) * points[4]) / 35.0;
            return (smoothedPoints, 0);
        }
    }
}