using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class PhysicsTable
    {
        List<Body> bodies = new List<Body>();
        List<Surface> surfaces = new List<Surface>();
        List<Spring> springs = new List<Spring>();

        internal delegate void PosHandler(double dt);
        internal event PosHandler OnPosChange;
        internal delegate void VelHandler(double dt);
        internal event VelHandler OnVelChange;
        internal delegate void ForceHandler();
        internal event ForceHandler OnForceChange;
        internal delegate void GravityHandler(bool isGravity);
        internal event GravityHandler OnGravity;
        internal delegate void SpringHandler();
        internal event SpringHandler OnSpring;
        internal delegate void DeleteForceHandler();
        internal event DeleteForceHandler OnDelete;
        internal delegate void SurfaceHandler();
        internal event SurfaceHandler OnSurface;

        const double dt = 0.01;

        public void AddBody(Body body)
        {
            bodies.Add(body);
            OnPosChange += body.ApplyVel;
            OnVelChange += body.ApplyAcc;
            OnForceChange += body.ApplyForces;
            OnGravity += body.Gravity;
            OnDelete += body.DeleteForces;
        }
        public void AddSpring(Spring spring)
        {
            springs.Add(spring);
            OnSpring += spring.ApplySpring;
        }
        public void AddSurface(Vector beginPoint, Vector endPoint)
        {
            Surface surface = new Surface(beginPoint, endPoint, bodies);
            surfaces.Add(surface);
            OnSurface += surface.CheckBodies;
        }

        public void OnTick(Graphics gr)
        {
            OnDelete?.Invoke();
            OnGravity?.Invoke(true);
            OnSpring?.Invoke();
            OnSurface?.Invoke();
            OnForceChange?.Invoke();
            OnVelChange?.Invoke(dt);
            OnPosChange?.Invoke(dt); for (int i = -10; i < 10; i++)
            {
                gr.DrawLine(Pens.LightGray, i * 60, 0, i * 60, 600);
                gr.DrawLine(Pens.LightGray, 0, i * 60, 600, i * 60);
                if (i == 5)
                {
                    gr.DrawLine(Pens.DarkGray, i * 60, 0, i * 60, 600);
                    gr.DrawLine(Pens.DarkGray, 0, i * 60, 600, i * 60);
                }
            }

            foreach (Body body in bodies)
            {
                body.DrawBody(gr);
            }
            foreach (Spring spring in springs)
            {
                spring.DrawSpring(gr);
            }
            foreach (Surface surface in surfaces)
            {
                surface.DrawSurface(gr);
            }
        }
        
    }
}
