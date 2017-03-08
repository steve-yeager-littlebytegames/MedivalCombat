using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using MedivalCombat.General;
using MedivalCombat.Global;

namespace ConsoleView
{
    public class Program
    {
        private static bool isListening;

        private static void Main(string[] args)
        {
            //Game.Start(@"C:\Projects\Git\MedivalCombat\Source\ConsoleView\Data\Unit_Knight.json");

            while(true)
            {
                ReadInput();
            }
        }

        private static void ReadInput()
        {
            string input = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            if(input == "start")
            {
                Task.Run(() => Game.Start());
                return;
            }

            if(input == "end") 
            {
                Game.End();
                return;
            }

            if(input == "pause")
            {
                Game.Pause(true);
                return;
            }
            if (input == "resume")
            {
                Game.Pause(false);
                return;
            }
            if(input == "save")
            {
                var replay = Game.CreateReplay();
                using(FileStream fileStream = new FileStream(@"C:\Users\Steve\Desktop\Replays\" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-tt") + ".txt", FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, replay);
                }
                return;
            }
            if(input == "snapshot")
            {
                var snapshot = Game.CreateSnapshot();
                File.WriteAllText(@"C:\Users\Steve\Desktop\Snapshots\" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-tt") + ".txt", snapshot);
                return;
            }

            if(input.StartsWith("replay"))
            {
                string path = input.Split(' ')[1];
                Replay replay = Replay.Load(path);
                Game.Replay(replay);
                return;
            }

            if(input.StartsWith("load"))
            {
                string path = input.Split(' ')[1];
                string data = File.ReadAllText(path);
                Game.PlaySnapshot(data);
                return;
            }

            if (input == "l")
            {
                isListening = true;
                Game.UpdateEvent += OnUpdate;
                return;
            }

            if (input == "p")
            {
                isListening = false;
                Game.UpdateEvent += OnUpdate;
                return;
            }

            if(input == "u1")
            {
                Game.SpawnUnit(0, 0, 0, 0);
                return;
            }
            if(input == "u2")
            {
                Game.SpawnUnit(0, 1, 100, 57);
                return;
            }

            try
            {
                string[] args = input.Split(' ');
                int unitId = int.Parse(args[0]);
                int playerNumber = int.Parse(args[1]);
                int positionX = int.Parse(args[2]);
                int positionY = int.Parse(args[3]);
                Game.SpawnUnit(unitId, playerNumber, positionX, positionY);
            }
            catch(Exception)
            {
                // ignored
            }
        }

        private static void OnUpdate()
        {
            if (!isListening)
            {
                Game.UpdateEvent -= OnUpdate; 
            }
            Console.WriteLine($"Frame: {Game.FrameCount}");
            foreach(var entity in Game.entities)
            {
                Console.WriteLine(entity);
            }
        }
    }
}