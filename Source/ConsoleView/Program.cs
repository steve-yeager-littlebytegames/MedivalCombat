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
            //Console.WriteLine("Frame: " + Game.FrameCount);
        }
    }
}