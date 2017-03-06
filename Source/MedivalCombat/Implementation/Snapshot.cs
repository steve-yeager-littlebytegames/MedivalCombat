using System.Collections.Generic;
using MedivalCombat.API;

namespace MedivalCombat.Implementation
{
    class Snapshot : ISnapshot
    {
        public Dictionary<string, string> Data { get; } = new Dictionary<string, string>();
    }
}