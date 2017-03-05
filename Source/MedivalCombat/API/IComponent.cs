namespace MedivalCombat.API
{
    public interface IComponent : IObject
    {
        IEntity Owner { get; }
    }
}