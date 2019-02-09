using ge.MathPrim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ge.Phis {

    public class ParticleRespawn : IParticleForceGenerator {

        private Random rnd;
        private float[] sizes;
        private float[] posX;
        private float[] posY;
        private Vec3[] velocities;
        private const int count = 1000;
        private int current;
        private float ttl = 1.0f;
        private float currentTime;

        public ParticleRespawn(int minRadius, int maxRadius) {
            rnd = new Random();
            sizes = new float[count];
            velocities = new Vec3[count];
            posX = new float[count];
            posY = new float[count];

            for (int i = 0; i < count; ++i) {

                float randX = (float)Math.Abs(rnd.Next() % 800);
                float randY = -(float)Math.Abs(rnd.Next() % 170);
                sizes[i] = minRadius + ((float)(rnd.Next() % maxRadius));;
                velocities[i] = new Vec3(30.0f * (i % 2 == 0 ? -1.0f : 1.0f), 90.0f + (float)(i * 10 * rnd.Next() % 20), 0.0f);
                posX[i] = randX;
                posY[i] = randY;
            }

            current = 0;
        }

        public void updateForce(Particle particle, float duration) {

            if(current < (count - 1)) {
                ++current;
            }
            else {
                current = 0;
            }

            if(currentTime > ttl) {
                currentTime = 0.0f;
                //particle.Position.y = -particle.Radius * 2.0f;
                particle.Position.x = posX[current];
                particle.Position.y = posY[current];
                particle.Velocity = velocities[current];
                particle.Acceleration.x = 0.0f;
                particle.Acceleration.y = 0.0f;
                particle.Radius = sizes[current];
                return;
            }
            else {
                currentTime += duration;
            }
            
	        if((particle.Position.y > 550.0f) || (particle.Position.x > 810.0f) || (particle.Position.x < -10.0f)) {
                //particle.Position.y = -particle.Radius * 2.0f;
                particle.Position.x = posX[current];
                particle.Position.y = posY[current];
                particle.Velocity = velocities[current];
                particle.Acceleration.x = 0.0f;
                particle.Acceleration.y = 0.0f;
                particle.Radius = sizes[current];
            }
        }
    };
}
