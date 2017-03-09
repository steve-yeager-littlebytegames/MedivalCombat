using System;
using System.Collections.Generic;
using System.Linq;
using MedivalCombat.Implementation;

namespace MedivalCombat.API
{
    public abstract class Object : IObject
    {
        public event Action<IObject> DestroyEvent;

        public const uint NoId = 0;

        public static readonly List<Object> allObjects = new List<Object>();
        private static uint objectCount;

        public uint Id { get; private set; }

        protected Object()
        {
            Id = objectCount;
            ++objectCount;
            allObjects.Add(this);
        }

        public abstract void Save(ISnapshot snapshot);

        public ISnapshot Save()
        {
            Snapshot snapshot = new Snapshot();
            snapshot.ObjectId = Id;
            Save(snapshot);
            return snapshot;
        }

        public virtual void Load(ISnapshot save)
        {
            Id = save.ObjectId;
        }

        public static void Reset()
        {
            objectCount = 1;
        }

        internal static Object GetById(uint id)
        {
            if (id == NoId)
            {
                return null;
            }

            return allObjects.FirstOrDefault(o => o.Id == id);
        }

        public virtual void Destroy()
        {
            DestroyEvent?.Invoke(this);
        }
    }
}