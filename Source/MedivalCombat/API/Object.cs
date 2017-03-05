using System.Collections.Generic;
using System.Linq;

namespace MedivalCombat.API
{
    public abstract class Object : IObject
    {
        public const uint NoId = 0;

        public static readonly List<Object> allObjects = new List<Object>();
        private static uint objectCount;
        public readonly uint id;

        public uint Id { get { return id; } }

        protected Object()
        {
            id = objectCount;
            ++objectCount;
            allObjects.Add(this);
        }

        public abstract ISnapshot Save();
        public abstract void Load(ISnapshot save);

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

            return allObjects.FirstOrDefault(o => o.id == id);
        }
    }
}