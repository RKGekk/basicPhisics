using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {

    class ParticleRod : IParticleContactGenerator {

        public float length;
        public Particle[] particles;

        public ParticleRod() {

            particles = new Particle[2];
        }

        public float currentLength() {
            Vec3 relativePos = particles[0].Position - particles[1].Position;
	        return relativePos.len();
        }

        
        public int addContact(ParticleContact[] contacts, int current, int limit) {

            float currentLen = currentLength();
            if (currentLen == this.length) {
                return 0;
            }

            contacts[current].particle1 = particles[0];
            contacts[current].particle2 = particles[1];

            Vec3 normal = (particles[1].Position - particles[0].Position).normal();

            if (currentLen > this.length) {

                contacts[current].contactNormal = normal;
                contacts[current].penetration = currentLen - this.length;
            }
            else {

                contacts[current].contactNormal = normal * -1.0f;
                contacts[current].penetration = this.length - currentLen;
            }

            contacts[current].restitution = 0.0f;

            return 1;
        }
    }
}
