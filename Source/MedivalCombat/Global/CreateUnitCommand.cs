using System;

namespace MedivalCombat.Global
{
    [Serializable]
    public class CreateUnitCommand
    {
        public int playerNumber;
        public int unitId;
        public int positionX;
        public int positionY;
        public int frame;
    }
}