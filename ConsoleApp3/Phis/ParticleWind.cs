using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {

    public class ParticleWind : IParticleForceGenerator {

        private Vec3 windOrigin;
        private float mass;
        private float G;

        public ParticleWind(Vec3 windOrigin, float mass, float G) {

            this.windOrigin = windOrigin;
            this.mass = mass;
            this.G = G;
        }

        public void updateForce(Particle particle, float duration) {
            
	        float mass1 = particle.Mass;
            float mass2 = this.mass;
            float distance = (particle.Position - windOrigin).len();

            float forceMagnitude = this.G * ((mass1 * mass2) / (distance * distance));
            Vec3 force = (particle.Position - windOrigin).normal();
            force *= -forceMagnitude;

	        particle.addForce(force);
        }
    };
}
