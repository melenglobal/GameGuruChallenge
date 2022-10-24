namespace Case2Folders.Scripts.Interfaces
{
    public interface IPool<T>
    {
        T Pull();
        
        void Push(T item);
    }
}