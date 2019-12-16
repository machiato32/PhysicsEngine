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
        internal Vector baseX, baseY;
        internal static readonly Vector defBaseX = new Vector(1, 0);
        internal static readonly Vector defBaseY = new Vector(0, 1);

        public Vector(double angle, double length, bool ang, Vector baseX = null, Vector baseY = null)
        {
            Angle = angle;
            Length = length;
            X = Length * Math.Cos(Angle);
            Y = Length * Math.Sin(Angle);
            if (baseX == null)
            {
                this.baseX = defBaseX;
            }
            else
            {
                this.baseX = baseX;
            }
            if (baseY == null)
            {
                this.baseY = defBaseY;
            }
            else
            {
                this.baseY = baseY;
            }
        }

        public Vector(double x, double y, Vector baseX = null, Vector baseY = null)
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
            if (baseX == null)
            {
                this.baseX = defBaseX;
            }
            else
            {
                this.baseX = baseX;
            }
            if (baseY == null)
            {
                this.baseY = defBaseY;
            }
            else
            {
                this.baseY = defBaseY;
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

        public static double VectorMultAbs(Vector v, Vector u)
        {
            return v.X * u.Y - u.X * v.Y;
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
        public bool NextToLine(Vector beginPoint, Vector endPoint) //TODO: ha a kovetkezo frameben tulmenne
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
        public Vector MirrorVel(Vector beginPoint, Vector endPoint)
        {
            Vector line = endPoint - beginPoint;
            Vector e = line.Norm();
            Matrix projector = Vector.Diadic(e, e);
            Matrix mirror = 2 * projector - Matrix.Identity(2);
            return (mirror * this);
        }
        public Vector MirrorVel(Vector toMirror)
        {
            Vector line = toMirror;
            Vector e = line.Norm();
            Matrix projector = Vector.Diadic(e, e);
            Matrix mirror = 2 * projector - Matrix.Identity(2);
            return (mirror * this);
        }

        public Vector MirrorPos(Vector beginPoint, Vector endPoint)
        {
            Vector line = endPoint - beginPoint;
            Vector e = line.Norm();
            Matrix projector = Vector.Diadic(e, e);
            Matrix mirror = 2 * projector - Matrix.Identity(2);
            return (mirror * this) - (mirror - Matrix.Identity(2)) * beginPoint;
        }

        public static Matrix Diadic(Vector v, Vector u)
        {
            return new Matrix(new double[,] { { v.X }, {v.Y } }) * new Matrix(new double[,] { { u.X, u.Y } });
        }

        public static double NormCosAngle(double angle)
        {
            while (angle < -Math.PI / 2) 
            {
                angle +=  Math.PI;
            }
            while (angle > Math.PI / 2) 
            {
                angle -= Math.PI;
            }
            return angle;
        }
        public static double NormAngle(double angle)
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
