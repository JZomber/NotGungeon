namespace UI.PowerUps
{
    public interface IQueue<T>
    {
        void Acolar(T obj);
        T DesAcolar();
        T First();
    
        bool IsQueueEmpty();
    }
}
