using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {

    public class SphereContact : IParticleContactGenerator {

        private List<Particle> particles;

        public SphereContact() {
            particles = new List<Particle>();
        }

        public void init(List<Particle> particles) {
            this.particles = particles;
        }

        public int addContact(ParticleContact[] contacts, int current, int limit) {

            int count = 0;
            ParticleContact contact = contacts[current];
            foreach (Particle p1 in particles) {
                foreach (Particle p2 in particles) {
                    if (p2 != p1) {
                        Vec3 particle1Pos = p1.Position;
                        Vec3 particle2Pos = p2.Position;
                        float rad1 = p1.Radius;
                        float rad2 = p2.Radius;

                        Vec3 contactTrace = particle1Pos - particle2Pos;
                        float distance = contactTrace.len();

                        if (distance < (rad1 + rad2)) {
                            contact.contactNormal = contactTrace.normal();
                            contact.particle1 = p1;
                            contact.particle2 = p2;
                            contact.penetration = (rad1 + rad2) - distance;
                            contact.restitution = 1.0f;
                            current++;
                            contact = contacts[current];
                            count++;
                        }

                        if (count >= limit) return count;
                    }
                }
            }
            return count;
        }
    }
}
