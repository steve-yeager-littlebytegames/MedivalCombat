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

        public static IEntity Create(int unitId)
        {
            switch(unitId)
            {
                case 0:
                    return CreateKnight();
                default:
                    throw new NotSupportedException();
            }
        }

        private static IEntity CreateKnight()
        {
            IEntity entity = new Entity("Knight", UnitTypes.Ground);
            TargetDetector targetDetector = new TargetDetector(entity)
            {
                TargetTypes = UnitTypes.Ground | UnitTypes.Building,
                Range = 2
            };
            entity.AddComponent(targetDetector);

            return entity;
        }
    }
}

public interface IHealth : IComponent
{
    int Health { get; }

    void DealDamage(int amount);
}

public interface IMovement : IComponent
{
    void MoveBy(int x, int y);
    void MoveTowards(int x, int y);
}