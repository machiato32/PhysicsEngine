using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Spring
    {
        public Body attachedBody { get; private set; }
        public Body attachedBody2 { get; private set; }
        public Vector attachPoint { get; private set; }
        public Vector beginPoint { get; private set; }
        public Vector endPoint { get; private set; }
        public double K { get; private set; }
        public double Length0 { get; private set; }
        public double Length { get; private set; }

               

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

        public void ApplySpring()
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
        public double SpringEnergy()
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
