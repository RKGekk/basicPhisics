using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {
    class GroundContact : IParticleContactGenerator {

        private List<Particle> particles;

        public GroundContact() {
            particles = new List<Particle>();
        }

        public void init(List<Particle> particles) {
            this.particles = particles;
        }

        public int addContact(ParticleContact[] contacts, int current, int limit) {

            int count = 0;
            ParticleContact contact = contacts[current];
            foreach (Particle p in particles) {

                float y = p.Position.y;
                float x = p.Position.x;
                if (y > 500.0f) {
                    contact.contactNormal = new Vec3(0.0f, -1.0f, 0.0f);
                    contact.particle1 = p;
                    contact.particle2 = null;
                    contact.penetration = y - 500.0f;
                    contact.restitution = 0.4f;
                    current++;
                    contact = contacts[current];
                    count++;
                }

                if (y < 0.0f) {
                    contact.contactNormal = new Vec3(0.0f, 1.0f, 0.0f);
                    contact.particle1 = p;
                    contact.particle2 = null;
                    contact.penetration = -y;
                    contact.restitution = 0.4f;
                    current++;
                    contact = contacts[current];
                    count++;
                }

                if (x > 700.0f) {
                    contact.contactNormal = new Vec3(-1.0f, 0.0f, 0.0f);
                    contact.particle1 = p;
                    contact.particle2 = null;
                    contact.penetration = x - 700.0f;
                    contact.restitution = 0.7f;
                    current++;
                    contact = contacts[current];
                    count++;
                }

                if (x < 0.0f) {
                    contact.contactNormal = new Vec3(1.0f, 0.0f, 0.0f);
                    contact.particle1 = p;
                    contact.particle2 = null;
                    contact.penetration = -x;
                    contact.restitution = 0.4f;
                    current++;
                    contact = contacts[current];
                    count++;
                }

                if (count >= limit) return count;
            }
            return count;
        }
    }
}
