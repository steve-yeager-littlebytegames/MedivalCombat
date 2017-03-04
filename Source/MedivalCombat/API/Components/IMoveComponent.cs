namespace MedivalCombat.API.Components
{
    public interface IMoveComponent : IComponent
    {
        void MoveBy(int x, int y);
        void MoveTowards(int x, int y);
    }
}