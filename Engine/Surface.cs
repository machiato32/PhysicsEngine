using System;
using System.Collections.Generic;
using System.Drawing;

namespace Engine
{
    public class Surface
    {
        internal static List<bool> stillColliding = new List<bool>();
        static int counter = 0;
        List<Body> bodies = new List<Body>();
        internal Vector BeginPoint { get; private set; }
        internal Vector EndPoint { get; private set; }
        internal double Length { get; private set; }
        internal double Angle { get; private set; }
        private int Counter { get; set; }

        public Surface(Vector beginPoint, Vector endPoint, List<Body> bodies)
        {
            Counter = counter;
            counter++;
            stillColliding.Add(false);

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
            Vector surfaceMiddle = (BeginPoint + EndPoint) / 2;
            Vector surfaceVect = EndPoint - BeginPoint;
            foreach (Body body in bodies)
            {
                Vector allFroce = new Vector(0, 0);
                foreach (Vector force in body.forces)
                {
                    allFroce += force;
                }

                Vector velNextIter = body.Vel + allFroce / body.Mass * PhysicsTable.dt;
                Vector posNextIter = body.Pos + velNextIter * PhysicsTable.dt;

                Vector posTransf = body.Pos - surfaceMiddle;
                Vector posTransfNextIter = posNextIter - surfaceMiddle;

                Vector a = 1.0 / 2 * surfaceVect - posTransf;
                Vector b = - 1.0 / 2 * surfaceVect - posTransf;
                Vector c = posTransfNextIter - posTransf;


                if ( false && body.Pos.OnLine(BeginPoint, EndPoint))
                {
                    Vector normVec = surfaceVect/surfaceVect.Length;
                    Matrix projector = Matrix.Identity(2) - Vector.Diadic(normVec, normVec);

                    Vector surfaceForce = projector * allFroce;
                    //if (Math.Sign((EndPoint - BeginPoint).X) == Math.Sign(surfaceForce.X))
                    //{
                        body.forces.Add(-surfaceForce);
                        body.Vel = new Vector(body.Vel.X, 0);
                    //}
                }
                else
                //if (body.Pos.NextToLine(BeginPoint, EndPoint))
                if (Math.Sign(Vector.VectorMultAbs(c, a)) != Math.Sign(Vector.VectorMultAbs(c,b)) && Math.Sign(Vector.VectorMultAbs(surfaceVect, posTransf)) != Math.Sign(Vector.VectorMultAbs(surfaceVect, posTransfNextIter)))
                {
                    body.Vel = velNextIter.MirrorVel(BeginPoint, EndPoint);
                    stillColliding[Counter] = true;
                }
            }
        }
    }

}
