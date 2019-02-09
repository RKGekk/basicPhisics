using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {
    class ParticleCableConstraint : ParticleConstraint, IParticleContactGenerator {

        public float maxLength;
        public float restitution;

        public int addContact(ParticleContact[] contacts, int current, int limit) {

            float length = currentLength();

            if (length < maxLength) {
                return 0;
            }

            contacts[current].particle1 = particle;
            contacts[current].particle2 = null;

            Vec3 normal = anchor - particle.Position;
            normal.normalize();

            contacts[current].contactNormal = normal;
            contacts[current].penetration = length - maxLength;
            contacts[current].restitution = restitution;

            return 1;
        }
    }
}
