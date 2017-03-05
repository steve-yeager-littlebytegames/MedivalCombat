using MedivalCombat.Global;

namespace MedivalCombat.API
{
    public interface IEntity : IObject
    {
        int PlayerNumber { get; }
        string Name { get; }
        UnitTypes UnitType { get; }
        int PositionX { get; set; }
        int PositionY { get; set; }

        void LogicUpdate();
        void PhysicsUpdate();
        void CombatUpdate();

        void AddComponent(IComponent component);
        T GetComponent<T>() where T : class, IComponent;
    }
}