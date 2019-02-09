using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ge.DataModels {

    [DataContract(Name = "WindowConfig", IsReference = false, Namespace = "https://geekbrains.ru/CSL3/2018/05/02/1300/GameConfig/WindowConfig/Entities")]
    public class WindowConfig {

        private int iScreenWidth;

        private int iScreenHeight;

        private string sMainWndCaption;

        [DataMember(Name = "screenWidth", Order = 0, IsRequired = true, EmitDefaultValue = false)]
        public int ScreenWidth {
            get {
                return iScreenWidth;
            }
            set {
                iScreenWidth = value;
            }
        }

        [DataMember(Name = "screenHeight", Order = 1, IsRequired = true, EmitDefaultValue = false)]
        public int ScreenHeight {
            get {
                return iScreenHeight;
            }
            set {
                iScreenHeight = value;
            }
        }

        [DataMember(Name = "mainWndCaption", Order = 2, IsRequired = true, EmitDefaultValue = false)]
        public string MainWndCaption {
            get {
                return sMainWndCaption;
            }
            set {
                sMainWndCaption = value;
            }
        }

        public WindowConfig() {
            this.iScreenWidth = 800;
            this.iScreenHeight = 600;
            this.sMainWndCaption = "";
        }

        public WindowConfig(int iScreenWidth, int iScreenHeight) {
            this.iScreenWidth = iScreenWidth;
            this.iScreenHeight = iScreenHeight;
            this.sMainWndCaption = "";
        }

        public WindowConfig(int iScreenWidth, int iScreenHeight, string sMainWndCaption) {
            this.iScreenWidth = iScreenWidth;
            this.iScreenHeight = iScreenHeight;
            this.sMainWndCaption = sMainWndCaption;
        }

        public override string ToString() {
            return "ScreenWidth: " + iScreenWidth.ToString() + "; ScreenHeight: " + iScreenHeight + ";";
        }
    }
}
