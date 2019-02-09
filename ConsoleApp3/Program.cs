using ge.DataModels;
using ge.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp3 {
    class Program {
        static void Main(string[] args) {

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            GameConfig cfg = new GameConfig("..\\..\\gameConfig.xml");
            GameWindow wnd = new GameWindow(cfg);

            GameApplication app = new GameApplication(cfg, wnd);
            app.initialize();
            app.run();
        }
    }
}
