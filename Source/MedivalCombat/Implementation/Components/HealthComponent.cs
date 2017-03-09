using MedivalCombat.API;

namespace MedivalCombat.Implementation.Components
{
    class HealthComponent : Component, IHealth 
    {
        public HealthComponent(IEntity owner, int maxHealth)
            : base(owner)
        {
            Health = maxHealth;
        }

        public override void Save(ISnapshot snapshot)
        {
            snapshot.Add("Health", Health);
        }

        public override void Load(ISnapshot save)
        {
            base.Load(save);
            Health = save.GetInt("Health");
        }

        public int Health { get; private set; }

        public void DealDamage(int amount)
        {
            Health -= amount;
        }
    }
}
