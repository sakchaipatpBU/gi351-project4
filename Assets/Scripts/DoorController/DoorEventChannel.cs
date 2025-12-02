using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Door Event Channel")]
public class DoorEventChannel : ScriptableObject
{
    public UnityEvent<string> OnDoorCloseRequest;

    public void RaiseEvent(string doorID)
    {
        OnDoorCloseRequest?.Invoke(doorID);
    }
}