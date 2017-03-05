namespace MedivalCombat.API
{
    interface ISnapshotable
    {
        ISnapshot Save();
        void Load(ISnapshot snapshot);
    }
}