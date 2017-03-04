using System;
using MedivalCombat.API;
using MedivalCombat.Implementation;

namespace MedivalCombat.Global
{
    public static class UnitFactory
    {
        //public static string UnitData;

        public static IEntity Create(int unitId)
        {
            switch(unitId)
            {
                case 0:
                    IEntity entity = new Entity("Knight");
                    return entity;
                default:
                    throw new NotSupportedException();
            }
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