namespace MedivalCombat.API
{
    public interface ISnapshot
    {
        uint ObjectId { get; }

        void Add(string key, object value);
        T Get<T>(string key);
    }
}