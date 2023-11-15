using System;

public interface IActivatable
{
    public event Action OnActivate;

    public abstract void Activate();
}
