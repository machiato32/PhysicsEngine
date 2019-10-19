using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Surface
    {
        List<Body> bodies = new List<Body>();
        public Vector BeginPoint { get; private set; }
        public Vector EndPoint { get; private set; }
        public double Length { get; private set; }
        public double Angle { get; private set; }

        public Surface(Vector beginPoint, Vector endPoint, List<Body> bodies)
        {
            BeginPoint = beginPoint;
            EndPoint = endPoint;
            Angle = (endPoint - beginPoint).Angle;
            Length = (EndPoint - BeginPoint).Length;
            this.bodies = bodies;
        }
        public Surface(Vector beginPoint, double length, float angle, List<Body> bodies)
        {

        }

        public void CheckBodies()
        {
            foreach(Body body in bodies)
            {
                if (body.Pos.OnLine(BeginPoint, EndPoint))
                {
                    Vector allFroce = new Vector(0, 0);
                    foreach (Vector force in body.forces)
                    {
                        allFroce += force;
                    }
                    //Vector planeForce = new Vector(allFroce.)
                    Vector xComp = allFroce.GetXComp();
                    Vector yComp = allFroce.GetYComp();

                    Vector surfaceX = new Vector(xComp.Angle - Angle, xComp.Length * Math.Sin(Angle), true);
                    Vector surfaceY = new Vector(yComp.Angle + Angle, yComp.Length * Math.Cos(Angle), true);

                    Vector surfaceForce  = surfaceX + surfaceY;
                    if (Math.Sign((EndPoint - BeginPoint).X) == Math.Sign(surfaceForce.X))
                    {
                        body.forces.Add(-surfaceForce);
                    }
                }
                if(body.Pos.NextToLine(BeginPoint, EndPoint))
                {
                    body.Vel = body.Vel.Mirror(BeginPoint, EndPoint);
                }
            }
        }
    }

}
