using System;
using System.Threading.Tasks;
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

            if (input == "l")
            {
                isListening = true;
                Game.UpdateEvent += OnUpdate;
            }

            if (input == "p")
            {
                isListening = false;
                Game.UpdateEvent += OnUpdate;
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