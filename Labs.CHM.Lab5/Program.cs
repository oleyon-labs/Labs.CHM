
namespace Labs.CHM.Lab5;


class Program
{
    static void Main(string[] args)
    {

        Directory.SetCurrentDirectory("../../../");
        Console.WriteLine(Directory.GetCurrentDirectory());


        KoshiSolver.SolveRungeKutt("input3.txt", f6, "output.txt");
    }

    public static double f1(double x, double y)
    {
        return x * y;
    }
    public static double f2(double x, double y)
    {
        return x * x;
    }
    public static double f3(double x, double y)
    {
        return x+y;
    }
    public static double f4(double x, double y)
    {
        return Math.Tan(x*y);
    }
    public static double f5(double x, double y)
    {
        return x / (y * y + 1);
    }
    public static double f6(double x, double y)
    {
        return x+y;
    }
}