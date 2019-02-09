using ge.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ge.DataModels {

    [DataContract(Name = "WindowConfig", IsReference = false, Namespace = "https://geekbrains.ru/CSL3/2018/05/02/1300/GameConfig/Entities")]
    public class GameConfig {

        private WindowConfig wcWindowConfig;

        private IGameDataFormatter<GameConfig> cfgFormatter;

        [DataMember(Name = "windowConfig", Order = 0, IsRequired = true, EmitDefaultValue = false)]
        public WindowConfig WindowConfig {
            get {
                return wcWindowConfig;
            }
            set {
                wcWindowConfig = value;
            }
        }

        public GameConfig() {
            this.cfgFormatter = new XMLGameDataFormatter<GameConfig>();
        }

        public GameConfig(WindowConfig wcWindowConfig) : this() {
            this.wcWindowConfig = wcWindowConfig;
        }

        public GameConfig(string path) : this() {
            LoadFromFile(path);
        }

        public void LoadFromFile(string path) {
            GameConfig cfg;
            using (FileStream stm = new FileStream(path, FileMode.Open)) {
                cfg = cfgFormatter.Deserialize(stm);
            }
            this.wcWindowConfig = cfg.WindowConfig;
        }

        public void SaveToFile(string path) {
            using (FileStream stm = new FileStream(path, FileMode.Create)) {
                cfgFormatter.Serialize(stm, this);
            }
        }

        public override string ToString() {
            return wcWindowConfig.ToString() + "\n";
        }
    }
}
