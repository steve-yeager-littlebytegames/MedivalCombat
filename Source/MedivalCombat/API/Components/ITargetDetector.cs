namespace MedivalCombat.API
{
    interface ITargetDetector : IComponent
    {
        uint Target { get; }

        void GetTarget();
        bool IsInRange();
    }
}