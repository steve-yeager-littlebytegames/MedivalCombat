namespace MedivalCombat.API
{
    public abstract class Component : Object, IComponent
    {
        public IEntity Owner { get; }

        protected Component(IEntity owner)
        {
            Owner = owner;
        }
    }
}