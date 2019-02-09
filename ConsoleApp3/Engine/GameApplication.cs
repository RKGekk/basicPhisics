using ge.DataModels;
using ge.MathPrim;
using ge.Phis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ge.Engine {
    public class GameApplication {

        private struct li {
            public Vec3 p1;
            public Vec3 p2;
        }

        private GameWindow _gameWindow;

        private int _frameCnt = 0;
        private float _timeElapsed = 0.0f;

        private Particle[] particleArray;
        private ParticleGravity[] particleGravityArray;
        private ParticleGravity[] particleGravityArray2;
        private ParticleWorld world1;
        private ParticleRespawn respawn;
        private ParticleWind radialWind;
        private ParticleWind radialWind2;

        private List<li> lines;

        private GroundContact groundContactGenerator;
        private SphereContact sphereContactGenerator;

        private ParticleRod[] particleRods;

        private int particleCount = 16;
        private int cablesCount = 24 + 18;
        private int freeParticles = 20;

        private GameTimer _timer;

        private void loadTextures() { }
        private void loadModels() { }
        private void buildGeometry() { }
        private void buildCamera() { }

        public int Width { get; set; }
        public int Height { get; set; }

        public GameConfig Config { get; set; }

        public GameApplication(GameConfig game_cfg, GameWindow gameWindow) {

            Config = game_cfg;

            _gameWindow = gameWindow;

            Width = Config.WindowConfig.ScreenWidth;
            Height = Config.WindowConfig.ScreenHeight;

            _timer = new GameTimer();

            world1 = new ParticleWorld(300, 16);

            respawn = new ParticleRespawn(1, 3);
            //radialWind = new ParticleWind(new ge.Vec3(100.0f, 100.0f, 0.0f), 10000.0f, 10.0f);
            //radialWind2 = new ParticleWind(new ge.Vec3(500.0f, 300.0f, 0.0f), 6000.0f, 15.0f);

            particleArray = new Particle[particleCount + freeParticles];
            for (int i = 0; i < (particleCount + freeParticles); ++i) {
                particleArray[i] = new Particle();
            }

            particleGravityArray = new ParticleGravity[(particleCount + freeParticles)];
            //particleGravityArray2 = new ParticleGravity[(particleCount + freeParticles)];

            for (int i = 0; i < (particleCount + freeParticles); i++) {
                world1.getParticles().Add(particleArray[i]);
            }

            groundContactGenerator = new GroundContact();
            groundContactGenerator.init(world1.getParticles());

            particleRods = new ParticleRod[cablesCount];

            sphereContactGenerator = new SphereContact();
            sphereContactGenerator.init(world1.getParticles());

            world1.getContactGenerators().Add(groundContactGenerator);
            world1.getContactGenerators().Add(sphereContactGenerator);
        }

        public void run() {
            _timer.reset();
            while (!_gameWindow._appClose) {
                _timer.tick();
                if (!_gameWindow._appPaused) {
                    update(_timer);
                    draw(_timer);
                    CalculateFrameStats();
                }
                else {
                    Thread.Sleep(100);
                }
                Application.DoEvents();
            }
        }

        public void initialize() {

            Random rnd = new Random();
            int numParticles = particleArray.Length;

            //lines

            float startX = 400.0f;
            float startY = 300.0f;
            float distanceX = 20.0f;
            float distanceY = 20.0f;
            float radius = 8.0f;
            int stride = 4;


            int cableCounter = 0;
            for (int i = 0; i < (numParticles - freeParticles); ++i) {

                int row = i / stride;
                int column = i - (stride * row);

                float x = startX + distanceX * ((float)(column));
                float y = startY + distanceY * ((float)(row));

                particleArray[i].setPosition(x, y, 0.0f);
                particleArray[i].Mass = 1.0f;
                particleArray[i].Radius = radius;
                //particleArray[i].setVelocity(20.0f, 90.0f + (float)(i * 10 * rnd.Next() % 20), 0.0f);
                particleArray[i].setVelocity(0.0f, 0.0f, 0.0f);
                particleArray[i].setAcceleration(0.0f, 0.0f, 0.0f);
                particleArray[i].Damping = 0.99f;
                particleArray[i].clearAccumulator();


                if (row != 0 && column != 0) {

                    // --
                    particleRods[cableCounter] = new ParticleRod();
                    particleRods[cableCounter].length = distanceX;
                    particleRods[cableCounter].particles[0] = particleArray[i];
                    particleRods[cableCounter].particles[1] = particleArray[i - 1];

                    world1.getContactGenerators().Add(particleRods[cableCounter]);

                    ++cableCounter;

                    // \
                    particleRods[cableCounter] = new ParticleRod();
                    particleRods[cableCounter].length = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
                    particleRods[cableCounter].particles[0] = particleArray[i];
                    particleRods[cableCounter].particles[1] = particleArray[i - stride - 1];

                    world1.getContactGenerators().Add(particleRods[cableCounter]);

                    ++cableCounter;

                    // /
                    particleRods[cableCounter] = new ParticleRod();
                    particleRods[cableCounter].length = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
                    particleRods[cableCounter].particles[0] = particleArray[i - 1];
                    particleRods[cableCounter].particles[1] = particleArray[i - stride];

                    world1.getContactGenerators().Add(particleRods[cableCounter]);

                    ++cableCounter;
                }
                else {
                    if (row != 0 && column == 0) {

                        // --
                        particleRods[cableCounter] = new ParticleRod();
                        particleRods[cableCounter].length = distanceX;
                        particleRods[cableCounter].particles[0] = particleArray[i];
                        particleRods[cableCounter].particles[1] = particleArray[i + 1];

                        world1.getContactGenerators().Add(particleRods[cableCounter]);

                        ++cableCounter;
                    }
                }

                if (i >= stride) {
                    if (row != 0) {

                        // |
                        particleRods[cableCounter] = new ParticleRod();
                        particleRods[cableCounter].length = distanceY;
                        particleRods[cableCounter].particles[0] = particleArray[i - stride];
                        particleRods[cableCounter].particles[1] = particleArray[i];

                        world1.getContactGenerators().Add(particleRods[cableCounter]);

                        ++cableCounter;
                    }
                }

                particleGravityArray[i] = new ParticleGravity(new Vec3(0.0f, 9.8f * 10.0f, 0.0f));
                world1.getForceRegistry().add(particleArray[i], particleGravityArray[i]);
            }


            for (int i = particleCount; i < (particleCount + freeParticles); ++i) {

                int randX = Math.Abs(rnd.Next() % 36);
                float x = 15.0f + (float)((i - freeParticles) * 40);
                float y = 0.0f + (i - freeParticles);
                particleArray[i].setPosition(x, y, 0.0f);
                particleArray[i].Mass = 10.0f;
                particleArray[i].Radius = 20.0f + 3.0f * (float)(rnd.Next() % 3);
                particleArray[i].setVelocity(20.0f * (i % 2 == 0 ? -1.0f : 1.0f), 90.0f + (float)(i * 10 * rnd.Next() % 20), 0.0f);
                particleArray[i].setAcceleration(0.0f, 0.0f, 0.0f);
                particleArray[i].Damping = 0.99f;
                particleArray[i].clearAccumulator();

                particleGravityArray[i] = new ParticleGravity(new Vec3(0.0f, 9.8f * 40.0f, 0.0f));

                world1.getForceRegistry().add(particleArray[i], particleGravityArray[i]);
            }

            loadTextures();
            loadModels();
            buildGeometry();
            buildCamera();
        }

        public void update(GameTimer timer) {

            world1.startFrame();
            float duration = timer.deltaTime();
            world1.runPhysics(duration);
        }

        public void draw(GameTimer timer) {

            _gameWindow.Clear();

            foreach (var obj in particleArray) {
                _gameWindow.DrawParicle(obj);
            }

            /*
            foreach (var obj in lines) {
                _gameWindow.DrawLine(obj.p1, obj.p2);
            }*/


            _gameWindow.Render();
        }

        public void CalculateFrameStats() {
            _frameCnt++;

            if ((_timer.gameTime() - _timeElapsed) >= 1.0f) {
                float fps = (float)_frameCnt;
                float mspf = 1000.0f / fps;

                _gameWindow.Caption = "    " + "FPS: " + fps + "    " + "Frame Time: " + mspf + " (ms)";

                _frameCnt = 0;
                _timeElapsed += 1.0f;
            }
        }
    }
}
