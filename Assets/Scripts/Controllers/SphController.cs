using UnityEngine;
using Main;
using Models;

namespace Controllers
{
    public class SphController : Controller
    {

        private float H, H2, EPS, REST_DENS, GAS_CONST, MASS, VISC, DT, POLY6, SPIKY_GRAD, VISC_LAP, BOUND_DAMPING;
        private Vector2 G;

        public SphController(Game game) : base(game)
        {
            this.H = this.game.getParticleSize();
            this.H2 = H*H;
            this.EPS = H;
            this.G = new Vector2(0.0f, -10.0f);
            this.REST_DENS = 300.0f;
            this.GAS_CONST = 2000.0f;
            this.MASS = 2.5f;
            this.VISC = 200.0f;
            this.DT = 0.0007f;
            this.POLY6 = 4.0f / (Mathf.PI * Mathf.Pow(H, 8.0f));
            this.SPIKY_GRAD = -10.0f / (Mathf.PI  * Mathf.Pow(H, 5.0f));
            this.VISC_LAP = 40.0f / (Mathf.PI * Mathf.Pow(H, 5.0f));
            this.BOUND_DAMPING = -0.5f;
        }

        private void computeDensityPressure()
        {
            foreach(Particle pi in this.game.getParticles())
            {
                pi.density = 0f;
                foreach(Particle pj in this.game.getParticles())
                {
                    float l2 = (pj.getPosition() - pi.getPosition()).SqrMagnitude();
                    if(l2 < H2)
                        pi.density += MASS * POLY6 * Mathf.Pow(H2 - l2, 3);
                }
                pi.pressure = GAS_CONST * (pi.density - REST_DENS);
            }
        }

        private void computeForces()
        {
            foreach(Particle pi in this.game.getParticles())
            {
                Vector2 pressureForce = new Vector2(0, 0);
                Vector2 viscocityForce = new Vector2(0, 0);
                foreach(Particle pj in this.game.getParticles())
                {
                    if(pi != pj)
                    {
                        Vector2 ij = (pj.getPosition() - pi.getPosition());
                        float l = ij.magnitude;
                        if(l < H)
                        {
                            pressureForce += -ij.normalized * MASS * (pi.pressure + pj.pressure) / (2 * pj.density) * SPIKY_GRAD * Mathf.Pow(H - l, 3);
                            viscocityForce += VISC * MASS * (pj.velocity - pi.velocity) / pj.density * VISC_LAP * (H - l);
                        }
                    }
                }
                Vector2 gravityForce = G * MASS / pi.density;
                pi.force = pressureForce + viscocityForce + gravityForce;
            }
        }

        private void integrate()
        {
            foreach(Particle p in this.game.getParticles())
            {
                p.velocity += DT * p.force / p.density;
                p.setPosition(p.getPosition() + (DT * p.velocity));
                float x = p.getPosition()[0];
                float y = p.getPosition()[1];

                if(this.game.isOutsideBox(new Vector2(x - EPS, y)))
                {
                    p.velocity *= BOUND_DAMPING;
                    p.setPosition(new Vector2(EPS - (this.game.getBoxSize()[0] / 2), y));
                }
                if(this.game.isOutsideBox(new Vector2(x + EPS, y)))
                {
                    p.velocity *= BOUND_DAMPING;
                    p.setPosition(new Vector2((this.game.getBoxSize()[0] / 2) - EPS, y));
                }

                if(this.game.isOutsideBox(new Vector2(x, y - EPS)))
                {
                    p.velocity *= BOUND_DAMPING;
                    p.setPosition(new Vector2(x, EPS - (this.game.getBoxSize()[1] / 2)));
                }
                if(this.game.isOutsideBox(new Vector2(x, y + EPS)))
                {
                    p.velocity *= BOUND_DAMPING;
                    p.setPosition(new Vector2(x, (this.game.getBoxSize()[1] / 2) - EPS));
                }
            }
        } 

        override
        public void start()
        {
            return;
        }

        override
        public void update()
        {
            this.computeDensityPressure();
            this.computeForces();
            this.integrate();
        }

    }
}