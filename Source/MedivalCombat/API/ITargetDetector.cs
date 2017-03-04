namespace MedivalCombat.API
{
    interface ITargetDetector : IComponent
    {
        IEntity Target { get; }

        void GetTarget();
        bool IsInRange();
    }
}