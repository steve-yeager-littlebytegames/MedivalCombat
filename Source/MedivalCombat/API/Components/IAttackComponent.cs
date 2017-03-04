using System.Collections.Generic;
using MedivalCombat.API;
using MedivalCombat.Commands;

namespace MedivalCombat.Implementation.Components
{
    public interface IAttackComponent : IComponent
    {
        IList<AttackCommand> Attack();
    }
}