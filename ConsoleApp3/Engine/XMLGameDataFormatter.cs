using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ge.Engine {

    public class XMLGameDataFormatter<T> : IGameDataFormatter<T> {

        private Type tType;
        private Encoding eEncoding;

        public T Deserialize(Stream serializationStream) {
            T result;
            
            DataContractSerializer serializer = new DataContractSerializer(tType);
            result = (T)serializer.ReadObject(serializationStream);
            
            return result;
        }

        public T Deserialize(string stringToDeserialize) {
            T result;
            using (MemoryStream memStm = new MemoryStream(eEncoding.GetBytes(stringToDeserialize))) {
                result = this.Deserialize(memStm);
            }
            return result;
        }

        public void Serialize(Stream serializationStream, T objectToSerialize) {
            
            DataContractSerializer serializer = new DataContractSerializer(tType);
            serializer.WriteObject(serializationStream, objectToSerialize);
        }

        public string Serialize(T objectToSerialize) {
            string result;
            using (MemoryStream memStm = new MemoryStream()) {
                this.Serialize(memStm, objectToSerialize);

                memStm.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memStm)) {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }

        public XMLGameDataFormatter() {
            tType = typeof(T);
            eEncoding = Encoding.UTF8;
        }
    }
}
