using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Body
    {
        internal Vector Pos { get; set; }
        internal Vector Vel { get; set; }
        internal Vector Acc { get; private set; }
        internal double Mass { get; private set; }

        internal List<Vector> forces = new List<Vector>();

        public Body(Vector pos, Vector vel, Vector acc, double mass)
        {
            Pos = pos;
            Vel = vel;
            Acc = acc;
            Mass = mass;
        }
        internal void DrawBody(Graphics gr)
        {
            gr.FillEllipse(Brushes.Black, (float)this.Pos.Trans().X - 5, (float)this.Pos.Trans().Y - 5, 10, 10);
        }
        internal void DeleteForces()
        {
            forces = new List<Vector>();
        }
        internal void Gravity(bool isGravity)
        {
            if (isGravity)
                forces.Add(new Vector(0, -20) * Mass);
        }
        internal void ApplyForces()
        {
            Vector allForce = new Vector(0, 0);
            foreach(Vector force in forces)
            {
                allForce += force;
            }
            Acc = allForce / Mass;
        }
        internal void ApplyAcc(double dt)
        {
            Vel += Acc * dt;
        }

        internal void ApplyVel(double dt)
        {
            Pos += Vel * dt;
        }
        internal double KineticEnergy()
        {
            return 0.5 * Mass * Math.Pow(Vel.Length, 2);
        }
    }
}
