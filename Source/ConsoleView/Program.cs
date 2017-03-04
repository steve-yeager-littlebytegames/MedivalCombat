using System;
using MedivalCombat.Global;

namespace ConsoleView
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Game.UpdateEvent += OnUpdate;
            Game.Start(@"C:\Projects\Git\MedivalCombat\Source\ConsoleView\Data\Unit_Knight.json");
        }

        private static void OnUpdate()
        {
            Console.WriteLine($"Frame: {Game.FrameCount}");
            foreach(var entity in Game.entities)
            {
                Console.WriteLine(entity);
            }


            //string input = Console.ReadLine();
            //if(string.IsNullOrWhiteSpace(input))
            //{
            //    return;
            //}

            //try
            //{
            //    string[] args = input.Split(' ');
            //    int unitId = int.Parse(args[0]);
            //    int playerNumber = int.Parse(args[1]);
            //    int positionX = int.Parse(args[2]);
            //    int positionY = int.Parse(args[3]);
            //    Game.SpawnUnit(unitId, playerNumber, positionX, positionY);
            //}
            //catch(Exception)
            //{
            //    // ignored
            //}
        }
    }
}