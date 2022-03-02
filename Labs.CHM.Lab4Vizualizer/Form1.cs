namespace Labs.CHM.Lab4Vizualizer
{
    public partial class Form1 : Form
    {
        //Mollifier mollifier = new Mollifier();
        //Point[] points = new Point[20];
        double[] yPos = new double[20];
        int activePoint = 0;
        bool resulted = false;
        int H;
        public Form1()
        {
            InitializeComponent();
            H = graph.Bounds.Width / yPos.Length;
            this.graph.MouseClick += OnPictureBoxClicked;
        }

        private void graph_Click(object sender, EventArgs e)
        {
            Graphics graphics = graph.CreateGraphics();
            Pen pen = new Pen(Color.Black, 6f);

            graphics.Clear(Color.White);
            //for (int i = 0; i < activePoint; i++)
            //{
            //    graphics.DrawEllipse(pen, new Rectangle(points[i].X - 2, points[i].Y - 2, 4, 4));
            //}
            DrawGraph(graphics, pen, ArrayToPoints(yPos));

        }

        private void calculate_Click(object sender, EventArgs e)
        {
            errorLabel.Text = " ";
            Graphics graphics = graph.CreateGraphics();
            Pen pen = new Pen(Color.Black, 6f);
            Pen pen1 = new Pen(Color.Red, 6f);

            graphics.Clear(Color.White);
            //for (int i = 0; i < activePoint; i++)
            //{
            //    graphics.DrawEllipse(pen, new Rectangle(points[i].X - 2, points[i].Y - 2, 4, 4));
            //}
            DrawGraph(graphics, pen, ArrayToPoints(yPos));

            int iterations = 0;
            string data = iterationsInputTextBox.Text;
            int.TryParse(iterationsInputTextBox.Text, out iterations);

            int errorStatus = 0;

            for (int i = 0; i < iterations; i++)
            {
                var result = Mollifier.Mollify(yPos, activePoint);
                if ((errorStatus=result.IER) == 2)
                {
                    errorLabel.Text = "Ошибка!! Количество точек должно быть больше 4";
                }
                else
                {
                    yPos = result.smoothedPoints;
                    //DrawGraph(graphics, pen1, ArrayToPoints(yPos));
                    resulted = true;
                }
            }
            if (iterations == 0)
            {
                errorLabel.Text = "Правильно введите число итераций!";
            }
            else if (errorStatus != 2)
            {
                DrawGraph(graphics, pen1, ArrayToPoints(yPos));
            }

            
            //activePoint = 0;
        }
        double[] PointsToArray(Point[] points)
        {
            double[] result = new double[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                result[i] = points[i].Y;
            }
            return result;
        }
        Point[] ArrayToPoints(double[] arr)
        {
            Point[] result = new Point[arr.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                result[i] = new Point(i * H+10, (int)arr[i]);
            }
            return result;
        }
        void OnPictureBoxClicked(object sender, MouseEventArgs args)
        {
            if(resulted)
            {
                activePoint = 0;
                resulted = false;
            }

            if(activePoint < yPos.Length)
            {
                var location = args.Location;
                yPos[activePoint] = location.Y;
                activePoint++;
            }
            graph_Click(sender, args);
            
        }

        void DrawGraph(Graphics graphics, Pen pen, Point[] points)
        {
            for (int i = 0; i < activePoint; i++)
            {
                graphics.DrawEllipse(pen, new Rectangle(points[i].X - 2, points[i].Y - 2, 4, 4));
            }
        }


    }
}