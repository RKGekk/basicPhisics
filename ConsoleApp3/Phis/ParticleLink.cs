using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {
    public class ParticleLink {

        public Particle[] particles;

        public ParticleLink() {

            particles = new Particle[2];
        }

        protected float currentLength() {
            Vec3 relativePos = particles[0].Position - particles[1].Position;
	        return relativePos.len();
        }
    }
}
