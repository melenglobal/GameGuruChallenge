namespace Case2Folders.Scripts.Interfaces
{
    public interface ICoin : ICollectableItem
    {
        void AddCoin(int amounth);
        
        void RemoveCoin(int amounth);
    }
}