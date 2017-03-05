using System;
using MedivalCombat.API;
using MedivalCombat.API.Components;

namespace MedivalCombat.Implementation.Components
{
    public class MoveComponent : Component, IMoveComponent
    {
        public MoveComponent(IEntity owner)
            : base(owner)
        {
        }

        public int Speed { get; set; }

        public void MoveBy(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void MoveTowards(int x, int y)
        {
            int xDistance = Math.Abs(x - Owner.PositionX);
            xDistance = Math.Min(xDistance, Speed);

            int xDirection = Math.Sign(x - Owner.PositionX);
            Owner.PositionX += xDistance * xDirection;

            int yDistance = Math.Abs(y - Owner.PositionY);
            yDistance = Math.Min(yDistance, Speed);

            int yDirection = Math.Sign(y - Owner.PositionY);
            Owner.PositionY += yDistance * yDirection;
        }

        public override ISnapshot Save()
        {
            throw new NotImplementedException();
        }

        public override void Load(ISnapshot save)
        {
            throw new NotImplementedException();
        }
    }
}