using System;

namespace MedivalCombat.API
{
    public interface IObject
    {
        event Action<IObject> DestroyEvent;

        uint Id { get; }

        void Destroy();
    }
}