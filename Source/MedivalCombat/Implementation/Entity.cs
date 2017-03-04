using MedivalCombat.API;

namespace MedivalCombat.Implementation
{
    public class Entity : IEntity
    {
        public string Name { get; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public Entity(string name)
        {
            Name = name;
        }

        public void LogicUpdate()
        {
        }

        public void PhysicsUpdate()
        {
        }

        public void CombatUpdate()
        {
        }
    }
}