using System;
using System.Collections.Generic;
using MedivalCombat.API;
using MedivalCombat.Commands;

namespace MedivalCombat.Implementation.Components
{
    class SingleAttackComponent : Component, IAttackComponent
    {
        public SingleAttackComponent(IEntity owner)
            : base(owner)
        {
        }

        public IList<AttackCommand> Attack()
        {
            throw new NotImplementedException();
        }

        public override void Save(ISnapshot snapshot)
        {
        }
    }
}