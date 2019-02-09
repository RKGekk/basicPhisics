using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {

    public class ParticleConstraint {

        public Particle particle;
        public Vec3 anchor;

        protected float currentLength() {
            Vec3 relativePos = particle.Position - anchor;
            return relativePos.len();
        }
    };
}
