using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {
    public interface IParticleForceGenerator {
        void updateForce(Particle particle, float duration);
    }
}
