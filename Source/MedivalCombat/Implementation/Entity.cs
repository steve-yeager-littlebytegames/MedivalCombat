using System.Collections.Generic;
using MedivalCombat.API;
using MedivalCombat.Global;

namespace MedivalCombat.Implementation
{
    public class Entity : Object, IEntity
    {
        private readonly List<IComponent> components = new List<IComponent>();

        public string Name { get; private set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PlayerNumber { get; private set; }
        public UnitTypes UnitType { get; private set; }

        public Entity(string name, UnitTypes unitType, int playerNumber)
        {
            Name = name;
            UnitType = unitType;
            PlayerNumber = playerNumber;
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
            return $"ID: {Id}, Name: {Name}, Team: {PlayerNumber}, Position: {PositionX}, {PositionY}";
        }

        public override void Save(ISnapshot snapshot)
        {
            snapshot.Add("Name", Name);
            snapshot.Add("PositionX", PositionX);
            snapshot.Add("PositionY", PositionY);
            snapshot.Add("PlayerNumber", PlayerNumber);
            snapshot.Add("UnitType", UnitType);
        }

        public override void Load(ISnapshot save)
        {
            base.Load(save);
            Name = save.Get<string>("Name");
            PositionX = save.Get<int>("PositionX");
            PositionY = save.Get<int>("PositionY");
            PlayerNumber = save.Get<int>("PlayerNumber");
            UnitType = save.Get<UnitTypes>("UnitType");
        }
    }
}