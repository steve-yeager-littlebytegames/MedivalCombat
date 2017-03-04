using System;
using MedivalCombat.API;
using MedivalCombat.Implementation;
using MedivalCombat.Implementation.Components;

namespace MedivalCombat.Global
{
    [Flags]
    public enum UnitTypes
    {
        Ground,
        Flying,
        Building,
        Spell,
    }

    public static class UnitFactory
    {
        //public static string UnitData;

        public static IEntity Create(int unitId, int playerNumber)
        {
            switch(unitId)
            {
                case 0:
                    return CreateKnight(playerNumber);
                default:
                    throw new NotSupportedException();
            }
        }

        private static IEntity CreateKnight(int playerNumber)
        {
            IEntity entity = new Entity("Knight", UnitTypes.Ground, playerNumber);

            TargetDetector targetDetector = new TargetDetector(entity)
            {
                TargetTypes = UnitTypes.Ground | UnitTypes.Building,
                Range = 2
            };
            entity.AddComponent(targetDetector);

            MoveComponent mover = new MoveComponent(entity) {Speed = 2};
            entity.AddComponent(mover);

            return entity;
        }
    }
}

public interface IHealth : IComponent
{
    int Health { get; }

    void DealDamage(int amount);
}