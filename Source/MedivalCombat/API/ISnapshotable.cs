namespace MedivalCombat.API
{
    public interface ISnapshotable
    {
        ISnapshot Save();
        void Load(ISnapshot snapshot);
    }
}