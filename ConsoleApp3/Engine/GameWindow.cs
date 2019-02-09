using ge.DataModels;
using ge.MathPrim;
using ge.Phis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ge.Engine {

    public class GameWindow : Form {

        public bool _appPaused = false;
        public bool _appClose = false;
        public bool _minimized = false;
        public bool _maximized = false;
        public bool _resizing = false;
        public bool _fullscreenState = false;

        private BufferedGraphicsContext _context;
        private BufferedGraphics _buffer;
        private Graphics _graphics;

        private string _windowCaption;
        private string _windowName;

        private void _game_form_FormClosing(object sender, FormClosingEventArgs e) {
            _appClose = true;
        }

        public string Caption {
            get {
                return _windowCaption;
            }

            set {
                this.Text = _windowName + value;
                _windowCaption = value;
            }
        }

        public GameWindow() {

        }

        public GameWindow(GameConfig cfg) {

            this._windowName = cfg.WindowConfig.MainWndCaption;

            this.Width = cfg.WindowConfig.ScreenWidth;
            this.Height = cfg.WindowConfig.ScreenHeight;

            this.Width = this.Width;
            this.Height = this.Height;
            this.DoubleBuffered = true;

            _context = BufferedGraphicsManager.Current;

            Graphics g = this.CreateGraphics();
            _buffer = _context.Allocate(g, new Rectangle(0, 0, this.Width, this.Height));

            this.FormClosing += _game_form_FormClosing;
            this.Show();
        }

        public void DrawParicle(Particle particle) {
            
            _graphics.FillEllipse(Brushes.White, new Rectangle((int)particle.Position.x, (int)particle.Position.y, (int)particle.Radius * 2, (int)particle.Radius * 2));
        }

        public void DrawLine(Vec3 p1, Vec3 p2) {
            
            //_graphics.DrawLine(Brushes.Red, new Point((int)p1.x, (int)p1.y), new Point((int)p2.x, (int)p2.y));
        }

        public void Clear() {
            this.SuspendLayout(); 
            _graphics = _buffer.Graphics;
            _graphics.Clear(Color.Black);
        }

        public void Render() {
            _buffer.Render();
            this.ResumeLayout(); 
        }
    }
}
