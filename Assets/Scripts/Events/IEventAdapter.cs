
using System;

public interface IEventAdapterListener
{
    public void Trigger();
}

public interface IEventAdapter
{
    public bool TryRegister(IEventAdapterListener listener, Type[] events);
    public bool TryUnregister(IEventAdapterListener listener);
}
