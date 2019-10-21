using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Spring
    {
        internal Body attachedBody { get; private set; }
        internal Body attachedBody2 { get; private set; }
        internal Vector attachPoint { get; private set; }
        internal Vector beginPoint { get; private set; }
        internal Vector endPoint { get; private set; }
        internal double K { get; private set; }
        internal double Length0 { get; private set; }
        internal double Length { get; private set; }

               

        public Spring(Body attachedBody, Body attachedBody2, double k, double lenght0)
        {
            this.attachedBody = attachedBody;
            this.attachedBody2 = attachedBody2;
            beginPoint = attachedBody.Pos;
            endPoint = attachedBody2.Pos;
            K = k;
            Length0 = lenght0;
        }

        public Spring(Body attachedBody, Vector attachPoint, double k, double lenght0)
        {
            this.attachedBody = attachedBody;
            this.attachPoint = attachPoint;
            beginPoint = attachedBody.Pos;
            endPoint = attachPoint;
            K = k;
            Length0 = lenght0;
        }
        internal void DrawSpring(Graphics gr)
        {
            gr.DrawLine(Pens.Black, (float)this.beginPoint.Trans().X, (float)this.beginPoint.Trans().Y, (float)this.endPoint.Trans().X, (float)this.endPoint.Trans().Y);
        }

        internal void ApplySpring()
        {
            beginPoint = attachedBody.Pos;
            if (attachedBody2 != null)
            {
                endPoint = attachedBody2.Pos;
            }
            Length = (beginPoint - endPoint).Length;
            attachedBody.forces.Add(K * (Length0 - Length)*(beginPoint-endPoint).Norm());
            if(attachedBody2!=null) attachedBody2.forces.Add(-K * (Length0 - Length) * (beginPoint - endPoint).Norm());
        }
        internal double SpringEnergy()
        {
            beginPoint = attachedBody.Pos;
            if (attachedBody2 != null)
            {
                endPoint = attachedBody2.Pos;
            }
            Length = (beginPoint - endPoint).Length;
            return K * Math.Pow((Length - Length0), 2);
        }
    }
}
