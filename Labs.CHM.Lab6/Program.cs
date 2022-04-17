namespace Labs.CHM.Lab6;


class Program
{
    static void Main(string[] args)
    {
        Directory.SetCurrentDirectory("../../../");
        Console.WriteLine(Directory.GetCurrentDirectory());

        ShootingSolver.Solve("input1.txt", f2, "output.txt");
        Console.WriteLine("");
        ShootingSolver.Solve("input1.txt", f2,f2y,f2y1, "output.txt");
    }
    public static double f1(double x, double y, double y1)
    {
        return 6*x;
    }
    public static double f2(double x, double y, double y1)
    {
        return (y1)/(x+1);
    }
    public static double f2y(double x, double y, double y1)
    {
        return 1 / (x + 1);
    }
    public static double f2y1(double x, double y, double y1)
    {
        return 0;
    }
}