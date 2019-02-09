using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {
    class ParticleCable : ParticleLink, IParticleContactGenerator {

        public float maxLength;
        public float restitution;

        public int addContact(ParticleContact[] contacts, int current, int limit) {

            float length = currentLength();
            if (length < maxLength) {
                return 0;
            }

            contacts[current].particle1 = particles[0];
            contacts[current].particle2 = particles[1];

            Vec3 normal = particles[1].Position - particles[0].Position;
            normal.normalize();

            contacts[current].contactNormal = normal;
            contacts[current].penetration = (length - maxLength);
            contacts[current].restitution = restitution;

            return 1;
        }
    }
}
