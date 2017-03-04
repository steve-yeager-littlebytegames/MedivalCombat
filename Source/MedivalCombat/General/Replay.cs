using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MedivalCombat.Global;

namespace MedivalCombat.General
{
    [Serializable]
    public class Replay
    {
        public Queue<CreateUnitCommand> frames;

        public static Replay Load(string path)
        {
            using(FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var replay = formatter.Deserialize(fileStream);
                fileStream.Dispose();
                return (Replay)replay;
            }
        }
    }
}