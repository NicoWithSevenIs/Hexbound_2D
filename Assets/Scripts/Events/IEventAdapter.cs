
using System;

public interface IEventAdapterListener
{
    public void Trigger();
}

public interface IEventAdapter
{
    public void Register(IEventAdapterListener listener, Type[] events);
    public void Unregister(IEventAdapterListener listener);
}
