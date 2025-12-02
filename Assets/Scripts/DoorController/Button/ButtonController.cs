using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [Header("Button Identity")]
    public string buttonID;

    [Header("Event Channel")]
    public ButtonEventChannel buttonChannel;

    [Header("Visuals")]
    public SpriteRenderer spriteRenderer;
    public Sprite spriteWhenOn;
    public Sprite spriteWhenOff;

    private bool hasBeenPressed = false;

    public Color color1;
    public Color color2;

    void Start()
    {
        // Start in the "off" state
        if (spriteRenderer != null && spriteWhenOff != null)
        {
            spriteRenderer.sprite = spriteWhenOff;
            spriteRenderer.color = color1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only allow the button to be pressed once, and only by the player
        if (!hasBeenPressed && other.CompareTag("Player"))
        {
            PressButton();
        }
    }

    private void PressButton()
    {
        hasBeenPressed = true;
        Debug.Log("Button pressed: " + buttonID);

        // Change to the "on" sprite
        if (spriteRenderer != null && spriteWhenOn != null)
        {
            spriteRenderer.sprite = spriteWhenOn;
            spriteRenderer.color = color2;
        }
        
        // Broadcast the event that this button was pressed
        if (buttonChannel != null)
        {
            buttonChannel.RaiseEvent(buttonID);
        }
    }
}