namespace PantheonAddonFramework.Events;

public class AddonEvent<T>
{
    private event Action<T> Event = delegate { }; // Prevents null reference exceptions

    public void Subscribe(Action<T> handler) => Event += handler;
    public void Unsubscribe(Action<T> handler) => Event -= handler;
    public void Raise(T arg) => Event(arg);
}