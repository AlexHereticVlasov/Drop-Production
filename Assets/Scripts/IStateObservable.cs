using UnityEngine.Events;

public interface IStateObservable
{
    public event UnityAction<DropStates> StateChanged;
    public event UnityAction Hited;
}
