using System.Collections.Generic;

namespace MedivalCombat.API
{
    public interface ISnapshot
    {
        Dictionary<string, string> Data { get; }
    }
}