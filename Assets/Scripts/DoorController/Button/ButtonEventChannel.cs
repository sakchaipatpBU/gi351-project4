using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Button Event Channel")]
public class ButtonEventChannel : ScriptableObject
{
    // This event will carry the unique ID of the button that was pressed
    public UnityEvent<string> OnButtonPressed;

    public void RaiseEvent(string buttonID)
    {
        OnButtonPressed?.Invoke(buttonID);
    }
}