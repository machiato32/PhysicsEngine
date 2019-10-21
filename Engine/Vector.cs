using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Vector
    {
        internal const double dim = 60;
        
        internal double X { get; private set; }
        internal double Y { get; private set; }
        internal double Angle { get; private set; }
        internal double Length { get; private set; }

        public Vector(double angle, double length, bool ang)
        {
            Angle = angle;
            Length = length;
            X = Length * Math.Cos(Angle);
            Y = Length * Math.Sin(Angle);
        }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
            Length = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            if (Length == 0)
            {
                Angle = 0;
            }
            else if (y > 0)
            {
                    Angle = Math.Acos(X / Length);
            }
            else
            {
                Angle = -Math.Acos(X / Length);
            }
            
        }



        internal Vector Trans()
        {
            return new Vector(300 + dim * X, 300 - dim * Y); 
        }

        public static Vector operator +(Vector v, Vector u)
        {
            return new Vector(v.X + u.X, v.Y + u.Y);
        }
        public static Vector operator -(Vector v, Vector u)
        {
            return v + (-u);
        }
        public static Vector operator *(Vector v, double d)
        {
            return new Vector(d * v.X, d * v.Y);
        }
        public static Vector operator *(double d, Vector v)
        {
            return new Vector(d * v.X, d * v.Y);
        }
        public static Vector operator /(Vector v, double d)
        {
            return new Vector(1/d * v.X, 1/d * v.Y);
        }
        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, -v.Y);
        }
        public static double operator *(Vector v, Vector u)
        {
            return v.Length * u.Length * Math.Cos(Math.Abs(v.Angle - u.Angle));
        }
        public Vector Norm()
        {
            return this / this.Length;
        }
        public bool OnLine(Vector beginPoint, Vector endPoint)
        {
            double s = (beginPoint.Y - endPoint.Y) * (this.X - beginPoint.X) / (beginPoint.X - endPoint.X) + beginPoint.Y - this.Y;
            return this.X>=beginPoint.X && this.X<=endPoint.X && s <= 0.1 && s >= 0;
        }
        public bool NextToLine(Vector beginPoint, Vector endPoint)
        {
            if ((beginPoint - endPoint).X == 0)
            {
                return (this.X > beginPoint.X - 0.01 && this.X < beginPoint.X + 0.01);
            }
            double s = (beginPoint.Y - endPoint.Y) * (this.X - beginPoint.X) / (beginPoint.X - endPoint.X) + beginPoint.Y - this.Y;
            return (this.X > beginPoint.X && this.X < endPoint.X) && s < 0.1 && s > -0.1;
        }
        public Vector GetXComp()
        {
            return new Vector(X, 0);
        }
        public Vector GetYComp()
        {
            return new Vector(0, Y);
        }
        public Vector Mirror(Vector beginPoint, Vector endPoint)
        {
            Vector line = endPoint - beginPoint;
            if (line.Y == 0)
            {
                return new Vector(this.X, -this.Y);
            }
            if (line.X == 0)
            {
                return new Vector(-this.X, this.Y);
            }
            Vector e = line.Norm();
            double m = (endPoint.Y - beginPoint.Y) / (endPoint.X - beginPoint.X);
            double b = (endPoint.Y - beginPoint.Y) * (-beginPoint.X) / (endPoint.X - beginPoint.X) + beginPoint.Y;
            double aLength = b / Math.Sqrt(1 + Math.Pow(m, 2));
            Vector a = new Vector(e.Angle + Math.PI / 2, aLength, true);

            return 2 * e * (this * e) - this + 2 * a;
        }

        public double NormAngle(double angle)
        {
            while (angle < 0)
            {
                angle += 2 * Math.PI;
            }
            while (angle > 2 * Math.PI)
            {
                angle -= 2 * Math.PI;
            }
            return angle;
        }

    }
}
