using System.Collections.Generic;
using MedivalCombat.API;
using MedivalCombat.Global;

namespace MedivalCombat.Implementation
{
    public class Entity : IEntity
    {
        public const uint NoId = 0;
        private static uint IdCount;
        private static readonly Dictionary<uint, IEntity> entityIds = new Dictionary<uint, IEntity>();
        private readonly List<IComponent> components = new List<IComponent>();

        public uint Id { get; }
        public string Name { get; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PlayerNumber { get; }
        public UnitTypes UnitType { get; }

        public Entity(string name, UnitTypes unitType, int playerNumber)
        {
            Id = IdCount;
            ++IdCount;
            entityIds.Add(Id, this);

            Name = name;
            UnitType = unitType;
            PlayerNumber = playerNumber;
        }

        internal static void ResetCount()
        {
            IdCount = 1;
        }

        internal static IEntity GetById(uint id)
        {
            if(id == NoId)
            {
                return null;
            }

            IEntity entity;
            if(entityIds.TryGetValue(id, out entity))
            {
                return entity;
            }

            return null;
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

        public void AddComponent(IComponent component)
        {
            components.Add(component);
        }

        public T GetComponent<T>() where T : class, IComponent
        {
            for(int i = 0; i < components.Count; i++)
            {
                T found = components[i] as T;
                if(found != null)
                {
                    return found;
                }
            }

            return null;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Team: {PlayerNumber}, Position: {PositionX}, {PositionY}";
        }
    }
}