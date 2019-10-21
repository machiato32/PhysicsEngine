using System;
using System.Collections.Generic;
using System.Drawing;

namespace Engine
{
    public class Surface
    {
        List<Body> bodies = new List<Body>();
        internal Vector BeginPoint { get; private set; }
        internal Vector EndPoint { get; private set; }
        internal double Length { get; private set; }
        internal double Angle { get; private set; }

        public Surface(Vector beginPoint, Vector endPoint, List<Body> bodies)
        {
            if (beginPoint.X <= endPoint.X)
            { 
                BeginPoint = beginPoint;
                EndPoint = endPoint;
            }
            else
            {
                BeginPoint = endPoint;
                EndPoint = beginPoint;
            }
            Angle = (endPoint - beginPoint).Angle;
            Length = (EndPoint - BeginPoint).Length;
            this.bodies = bodies;
        }
        public Surface(Vector beginPoint, double length, float angle, List<Body> bodies)
        {

        }

        internal void DrawSurface(Graphics gr)
        {
            gr.DrawLine(Pens.Blue, (float)this.BeginPoint.Trans().X, (float)this.BeginPoint.Trans().Y, (float)this.EndPoint.Trans().X, (float)this.EndPoint.Trans().Y);
        }

        internal void CheckBodies()
        {
            foreach (Body body in bodies)
            {
                if (body.Pos.OnLine(BeginPoint, EndPoint))
                {
                    Vector allFroce = new Vector(0, 0);
                    foreach (Vector force in body.forces)
                    {
                        allFroce += force;
                    }
                    
                    Vector xComp = allFroce.GetXComp();
                    Vector yComp = allFroce.GetYComp();

                    Vector normVec = Matrix.Rotate(Vector.NormCosAngle(Angle), new Vector(0, 1));
                    Matrix projector = Vector.Diadic(normVec, normVec);

                    Vector surfaceForce = projector * allFroce;

                    //Vector surfaceX = new Vector(xComp.Angle - Angle, xComp.Length * Math.Sin(Angle), true);
                    //Vector surfaceY = new Vector(yComp.Angle + Angle, yComp.Length * Math.Cos(Angle), true);

                    //Vector surfaceForce = surfaceX + surfaceY;
                    if (Math.Sign((EndPoint - BeginPoint).X) == Math.Sign(surfaceForce.X))
                    {
                        body.forces.Add(-surfaceForce);
                    }
                }
                if (body.Pos.NextToLine(BeginPoint, EndPoint))
                {
                    body.Vel = body.Vel.Mirror(BeginPoint, EndPoint);
                }
            }
        }
    }

}
