using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;

namespace GUI
{
    public partial class Form1 : Form
    {
        //public Vector dimension = new Vector(10, 10); //dim in meters


        Body body = new Body(new Vector(0, 1), new Vector(0, 0), new Vector(0, 0), 10);
        Body body2 = new Body(new Vector(1, 1), new Vector(3, 0), new Vector(0, 0), 10);
        Body body3 = new Body(new Vector(2, 1), new Vector(0, 0), new Vector(0, 0), 10);
        Body body4 = new Body(new Vector(1, 0), new Vector(0, 0), new Vector(0, 0), 10);
        Body body5 = new Body(new Vector(1, 1), new Vector(0, 0), new Vector(0, 0), 10);
        Body body6 = new Body(new Vector(1, 2), new Vector(0, 0), new Vector(0, 0), 10);
        Body body7 = new Body(new Vector(2, 0), new Vector(0, 0), new Vector(0, 0), 10);
        Body body8 = new Body(new Vector(2, 1), new Vector(0, 0), new Vector(0, 0), 10);
        Body body9 = new Body(new Vector(1, 2), new Vector(0, 0), new Vector(0, 0), 10);
        Spring spring, spring2, spring3, spring4, spring5, spring6, spring7, spring8, spring9, spring10, spring11, spring12;
        Surface surface, surface2, surface3, surface4;

        double[,] sajt = new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };


        List<Body> bodies = new List<Body>();

        List<Spring> springs = new List<Spring>();

        List<Surface> surfaces = new List<Surface>();

        Bitmap bitmap = new Bitmap(600, 600);
        public delegate void PosHandler(double dt);
        public event PosHandler OnPosChange;
        public delegate void VelHandler(double dt);
        public event VelHandler OnVelChange;
        public delegate void ForceHandler();
        public event ForceHandler OnForceChange;
        public delegate void GravityHandler(bool isGravity);
        public event GravityHandler OnGravity;
        public delegate void SpringHandler();
        public event SpringHandler OnSpring;
        public delegate void DeleteForceHandler();
        public event DeleteForceHandler OnDelete;
        public delegate void SurfaceHandler();
        public event SurfaceHandler OnSurface;

        PhysicsTable table = new PhysicsTable();

        const double dt = 0.01;
        public Form1()
        {
            Matrix matrix = new Matrix(sajt);
            table.AddBody(body);
            table.AddBody(body2);
            table.AddBody(body3);
            //bodies.Add(body4);
            //bodies.Add(body5);
            //bodies.Add(body6);
            //bodies.Add(body7);
            //bodies.Add(body8);
            //bodies.Add(body9);

            //spring = new Spring(body, new Vector(300, 100), 10, 150);

            spring = new Spring(body, body2, 100, 1);
            spring2 = new Spring(body2, body3, 400, 1);
            spring3 = new Spring(body3, new Vector(3, 1), 100, 1);
            spring4 = new Spring(body2, body5, 100, 1.5);
            spring5 = new Spring(body6, new Vector(0, 2), 100, 1.5);
            spring6 = new Spring(body4, body5, 100, 1.5);
            spring7 = new Spring(body4, body7, 100, 1.5);
            spring8 = new Spring(body5, body6, 100, 1.5);
            spring9 = new Spring(body5, body8, 100, 1.5);
            spring10 = new Spring(body6, new Vector(2, 2), 100, 1.5);
            spring11 = new Spring(body7, body8, 100, 1.5);
            spring12 = new Spring(body8, new Vector(2, 2), 100, 1.5);

            table.AddSpring(spring);
            table.AddSpring(spring2);
            //springs.Add(spring3);
            //springs.Add(spring4);
            //springs.Add(spring5);
            //springs.Add(spring6);
            //springs.Add(spring7);
            //springs.Add(spring8);
            //springs.Add(spring9);
            //springs.Add(spring10);
            //springs.Add(spring11);
            //springs.Add(spring12);

            surface = new Surface(new Vector(0, 0), new Vector(2, 2), bodies);
            //surface2 = new Surface(new Vector(2, -3), new Vector(2, 2), bodies);
            //surface3 = new Surface(new Vector(2, 2), new Vector(-3, 2), bodies);
            //surface4 = new Surface(new Vector(-3, 2), new Vector(-3, -3), bodies);


            //surfaces.Add(surface);
            //surfaces.Add(surface2);
            //surfaces.Add(surface3);
            //surfaces.Add(surface4);

            InitializeComponent();
            Timer timer = new Timer()
            {
                Interval = 10,
                Enabled = true
            };
            timer.Tick += Timer_Tick;
            pictureBox1.Image = bitmap;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {

            bitmap = new Bitmap(600, 600);
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                table.OnTick(gr);
            }
            pictureBox1.Image = bitmap;
        }
    }
}
