using System;
using MedivalCombat.API;
using MedivalCombat.Global;

namespace MedivalCombat.Implementation.Components
{
    public class TargetDetector : Component, ITargetDetector
    {
        public TargetDetector(IEntity owner)
            : base(owner)
        {
        }

        public IEntity Target { get; private set; }
        public UnitTypes TargetTypes { get; set; }
        public int Range { get; set; }

        public void GetTarget()
        {
            Target = null;

            int smallestDistance = int.MaxValue;
            for(int i = 0; i < Game.entities.Count; i++)
            {
                if((TargetTypes & Game.entities[i].UnitType) != Game.entities[i].UnitType)
                {
                    continue;
                }

                int x = Math.Abs(Game.entities[i].PositionX - Owner.PositionX);
                int y = Math.Abs(Game.entities[i].PositionY - Owner.PositionY);
                int distance = x + y;
                if(distance < smallestDistance)
                {
                    smallestDistance = distance;
                    Target = Game.entities[i];
                }
            }
        }

        public bool IsInRange()
        {
            if(Target == null)
            {
                return false;
            }

            int x = Math.Abs(Target.PositionX - Owner.PositionX);
            int y = Math.Abs(Target.PositionY - Owner.PositionY);
            int distance = x + y;
            return distance <= Range;
        }
    }
}