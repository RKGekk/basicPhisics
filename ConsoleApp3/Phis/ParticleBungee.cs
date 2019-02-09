using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {
    public class ParticleBungee : IParticleForceGenerator {

        public Particle other;
        private float springConstant;
        private float restLength;

        public ParticleBungee(Particle anchoreParticle, float stiffness, float restDistance) {

            other = anchoreParticle;
            springConstant = stiffness;
            restLength = restDistance;
        }

        public void updateForce(Particle particle, float duration) {
            
            const float zero = float.Epsilon + 0.0001f;

	        Vec3 force = new Vec3(particle.Position);
	        
	        force -= other.Position;

	        float magnitude = force.len();
            
	        if( magnitude <= zero || magnitude <= restLength) {
	        	return;
	        }

            /*
            if( magnitude <= zero) {
	        	return;
	        }*/

	        magnitude = Math.Abs(magnitude) - restLength;
	        magnitude *= springConstant;

	        force.normalize();
	        force *= -magnitude;

	        particle.addForce(force);
        }
    };
}
