namespace MedivalCombat.API
{
    public interface IEntity
    {
        int PlayerNumber { get; }
        string Name { get; }
        int PositionX { get; set; }
        int PositionY { get; set; }

        void LogicUpdate();
        void PhysicsUpdate();
        void CombatUpdate();

        T AddComponent<T>() where T : class, IComponent, new();
        T GetComponent<T>() where T : class, IComponent;
    }
}