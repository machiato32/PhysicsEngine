using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Body
    {
        public Vector Pos { get; private set; }
        public Vector Vel { get; internal set; }
        public Vector Acc { get; private set; }
        public double Mass { get; private set; }

        internal List<Vector> forces = new List<Vector>();

        public Body(Vector pos, Vector vel, Vector acc, double mass)
        {
            Pos = pos;
            Vel = vel;
            Acc = acc;
            Mass = mass;
        }
        public void DeleteForces()
        {
            forces = new List<Vector>();
        }
        public void Gravity(bool isGravity)
        {
            if(isGravity)
                forces.Add(new Vector(0, -10));
        }
        public void ApplyForces()
        {
            Vector allForce = new Vector(0, 0);
            foreach(Vector force in forces)
            {
                allForce += force;
            }
            Acc = allForce / Mass;
        }
        public void ApplyAcc(double dt)
        {
            Vel += Acc * dt;
        }

        public void ApplyVel(double dt)
        {
            Pos += Vel * dt;
        }
        public double KineticEnergy()
        {
            return 0.5 * Mass * Math.Pow(Vel.Length, 2);
        }
    }
}
