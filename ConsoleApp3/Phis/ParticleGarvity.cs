using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {
    public class ParticleGravity : IParticleForceGenerator {

        private Vec3 gravity;

        public ParticleGravity() {
            gravity = new Vec3();
        }

        public ParticleGravity(Vec3 gravityG) {
            this.gravity = gravityG;
        }

        public void updateForce(Particle particle, float duration) {
            if (!particle.hasFiniteMass()) {
                return;
            }

            particle.addForce(gravity * particle.Mass);
        }
    }
}
