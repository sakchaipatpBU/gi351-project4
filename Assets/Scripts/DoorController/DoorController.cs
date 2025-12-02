using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DoorController : MonoBehaviour
{
    [Header("Door Identity")]
    public string doorID;

    [Header("Door Components")]
    [Tooltip("The child object that represents the closed door (with a SpriteRenderer and Collider)")]
    public GameObject closedStateObject;

    [Header("Event Channel")]
    public DoorEventChannel doorChannel;

    [Header("Linked Doors")]
    public List<string> linkedDoorIDs;
    
    [Header("Triggers")]
    public Collider2D triggerA;
    public Collider2D triggerB;

    // --- State & Trigger logic ---
    private bool isPermanentlyShut = false;
    private bool playerIsInTriggerA = false;
    private bool playerIsInTriggerB = false;
    private Collider2D entryTrigger = null;
    
    // --- NEW VARIABLES FOR LOCKING LOGIC ---
    [Header("Locking Mechanism")]
    public bool startsLocked = true;
    public int buttonsRequiredToOpen = 1;
    public ButtonEventChannel buttonChannel; // Channel to listen for button presses
    public List<string> listensForButtonIDs; // A list of button IDs this door cares about
    private int buttonsPressedCount = 0;
    private bool isUnlocked = false;

    public AudioSource audioSource;
    public AudioClip audioClipOpen;
    public AudioClip audioClipClose;

    void Start()
    {
        
        if (startsLocked)
        {
            isUnlocked = false;
            if (closedStateObject != null) closedStateObject.SetActive(true);
        }
        else
        {
            isUnlocked = true;
            if (closedStateObject != null) closedStateObject.SetActive(false);
        }
        
    }

    private void OnEnable()
    {
        if (doorChannel != null) doorChannel.OnDoorCloseRequest.AddListener(HandleCloseRequest);
        if (buttonChannel != null) buttonChannel.OnButtonPressed.AddListener(HandleButtonPress);
    }

    private void OnDisable()
    {
        if (doorChannel != null) doorChannel.OnDoorCloseRequest.RemoveListener(HandleCloseRequest);
        if (buttonChannel != null) buttonChannel.OnButtonPressed.RemoveListener(HandleButtonPress);
    }

    // --- NEW FUNCTION TO HANDLE BUTTON PRESSES ---
    private void HandleButtonPress(string pressedButtonID)
    {
        // If the door is already unlocked, ignore button presses
        if (isUnlocked) return;

        // Check if this is a button we care about and we haven't counted it yet
        if (listensForButtonIDs.Contains(pressedButtonID))
        {
            buttonsPressedCount++;
            Debug.Log($"Door '{doorID}' heard button '{pressedButtonID}'. Total presses: {buttonsPressedCount}/{buttonsRequiredToOpen}");

            // Once we have enough presses, unlock the door
            if (buttonsPressedCount >= buttonsRequiredToOpen)
            {
                UnlockAndOpenDoor();
            }
        }
    }
    
    private void UnlockAndOpenDoor()
    {
        Debug.Log($"Door '{doorID}' has been unlocked!");
        isUnlocked = true;
        if (closedStateObject != null)
        {
            closedStateObject.SetActive(false);

            audioSource.clip = audioClipOpen;
            audioSource.Play(); // sound

            //SoundManager.Instance.PlaySFX("Door_Open", 0.02f, 1); // Sound
        }
    }
    // --- END NEW FUNCTION ---

    private void HandleCloseRequest(string idToClose)
    {
        if (idToClose == this.doorID && !isPermanentlyShut)
        {
            ShutDoorPermanently();
        }
    }

    public void OnPlayerEnterTrigger(Collider2D enteredTrigger)
    {
        if (!isUnlocked || isPermanentlyShut) return;
        if (enteredTrigger == triggerA) playerIsInTriggerA = true;
        if (enteredTrigger == triggerB) playerIsInTriggerB = true;
        if (entryTrigger == null) entryTrigger = enteredTrigger;
    }

    public void OnPlayerExitTrigger(Collider2D exitedTrigger)
    {
        if (!isUnlocked || isPermanentlyShut) return;
        if (exitedTrigger == triggerA) playerIsInTriggerA = false;
        if (exitedTrigger == triggerB) playerIsInTriggerB = false;

        if (!playerIsInTriggerA && !playerIsInTriggerB && entryTrigger != null)
        {
            if (exitedTrigger != entryTrigger)
            {
                doorChannel.RaiseEvent(this.doorID);
                foreach (string linkedID in linkedDoorIDs)
                {
                    doorChannel.RaiseEvent(linkedID);
                }
            }
            entryTrigger = null;
        }
    }
    
    private void ShutDoorPermanently()
    {
        isPermanentlyShut = true;
        Debug.Log("Closing door: " + doorID);
        
        if (closedStateObject != null)
        {
            closedStateObject.SetActive(true);

            audioSource.clip = audioClipClose;
            audioSource.Play(); // sound
            //SoundManager.Instance.PlaySFX("Door_Close", 0.02f, 1); // Sound
        }
    }
}