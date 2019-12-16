using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Rope //TODO: nincs kesz :O
    {
        internal Body attachedBody { get; private set; }
        internal Body attachedBody2 { get; private set; }
        internal Vector attachPoint { get; private set; }
        internal Vector beginPoint { get; private set; }
        internal Vector endPoint { get; private set; }
        internal double Length0 { get; private set; }
        internal double Length { get; private set; }

               

        public Rope(Body attachedBody, Body attachedBody2, double lenght0)
        {
            this.attachedBody = attachedBody;
            this.attachedBody2 = attachedBody2;
            beginPoint = attachedBody.Pos;
            endPoint = attachedBody2.Pos;
            //K = k;
            Length0 = lenght0;
        }

        public Rope(Body attachedBody, Vector attachPoint, double lenght0)
        {
            this.attachedBody = attachedBody;
            this.attachPoint = attachPoint;
            beginPoint = attachedBody.Pos;
            endPoint = attachPoint;
            Length0 = lenght0;
        }

        internal void DrawRope(Graphics gr)
        {
            gr.DrawLine(Pens.Red, (float)this.beginPoint.Trans().X, (float)this.beginPoint.Trans().Y, (float)this.endPoint.Trans().X, (float)this.endPoint.Trans().Y);
        }

        internal void ApplyRope()
        {
            beginPoint = attachedBody.Pos;
            if (attachedBody2 != null)
            {
                endPoint = attachedBody2.Pos;
            }
            Length = (beginPoint - endPoint).Length;
            if (Length >= Length0)
            {
                if (attachedBody2 != null)
                {

                }
                else
                {
                    Vector allFroce = new Vector(0, 0);
                    foreach (Vector force in attachedBody.forces)
                    {
                        allFroce += force;
                    }

                    Vector velNextIter = attachedBody.Vel + allFroce / attachedBody.Mass * PhysicsTable.dt;

                    Vector toMirror = new Vector((beginPoint - endPoint).Angle + Math.PI / 2, 2, true);
                    attachedBody.Vel = velNextIter.MirrorVel(toMirror);
                }
            }
            //if (attachedBody2 != null) attachedBody2.forces.Add(-K * (Length0 - Length) * (beginPoint - endPoint).Norm());
        }
    }
}
