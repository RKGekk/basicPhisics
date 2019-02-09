using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {

    public class ParticleFakeSpring : IParticleForceGenerator {

        public Particle other;
        private float springConstant;
        private float damping;

        public ParticleFakeSpring(Particle anchoreParticle, float stiffness, float dampSpeed) {

            other = anchoreParticle;
            springConstant = stiffness;
            damping = dampSpeed;
        }

        public void updateForce(Particle particle, float duration) {
            
            const float zero = float.Epsilon + 0.0001f;

            Vec3 position =  particle.Position;
	        position -= other.Position;

	        float gamma = 0.5f * (float)(Math.Sqrt(4.0f * springConstant - damping * damping));
	        if(Math.Abs(gamma) <= zero ) {
	        	return;
	        }

	        Vec3 c = position * (damping / (2.0f * gamma)) + particle.Velocity * (1.0f / gamma);
	        Vec3 target = position * ((float)(Math.Cos(gamma * duration))) + c * ((float)(Math.Sin(gamma * duration)));
	        target *= (float)Math.Exp(-0.5f * duration * damping);
	        Vec3 accel = (target - position) * (1.0f / (duration * duration)) - particle.Velocity * (1.0f / duration);

	        particle.addForce(accel * particle.Mass);
        }
    };
}
