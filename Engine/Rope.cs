using System;
using System.Collections.Generic;
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

            }
            //if (attachedBody2 != null) attachedBody2.forces.Add(-K * (Length0 - Length) * (beginPoint - endPoint).Norm());
        }
    }
}
