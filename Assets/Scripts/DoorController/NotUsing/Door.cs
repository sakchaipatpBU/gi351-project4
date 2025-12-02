using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Components")]
    public GameObject doorSpriteGameObj;
    public Collider2D doorCollider;

    [Header("Triggers")]
    public Collider2D triggerA;
    public Collider2D triggerB;

    // --- STATE TRACKING ---
    private bool isPermanentlyShut = false;
    private bool playerIsInTriggerA = false;
    private bool playerIsInTriggerB = false;

    // THE KEY VARIABLE: Remembers the first trigger the player entered
    private Collider2D entryTrigger = null;

    void Start()
{
    // Get the SpriteRenderer component from the doorSpriteGameObj object
    SpriteRenderer doorSpriteRenderer = doorSpriteGameObj.GetComponent<SpriteRenderer>();

    // Start with the door visually and physically open
    if (doorSpriteRenderer != null)
    {
        doorSpriteRenderer.enabled = false; // Disable the component
    }
    doorCollider.enabled = false;
}

    public void OnPlayerEnterTrigger(Collider2D enteredTrigger)
    {
        if (isPermanentlyShut) return;

        // Update our state flags
        if (enteredTrigger == triggerA) playerIsInTriggerA = true;
        if (enteredTrigger == triggerB) playerIsInTriggerB = true;

        // If this is the first trigger the player has entered for this "crossing",
        // record it as our point of entry.
        if (entryTrigger == null)
        {
            entryTrigger = enteredTrigger;
            Debug.Log("Player initiated crossing from: " + entryTrigger.name);
        }
    }

    public void OnPlayerExitTrigger(Collider2D exitedTrigger)
    {
        if (isPermanentlyShut) return;

        // Update our state flags
        if (exitedTrigger == triggerA) playerIsInTriggerA = false;
        if (exitedTrigger == triggerB) playerIsInTriggerB = false;

        // Check for the final decision only when the player is in NEITHER trigger
        if (!playerIsInTriggerA && !playerIsInTriggerB)
        {
            // If the system was waiting for a crossing to complete...
            if (entryTrigger != null)
            {
                // ...and the player exited from the OPPOSITE side they entered from...
                if (exitedTrigger != entryTrigger)
                {
                    // ...then they successfully passed through.
                    ShutDoorPermanently();
                }
                else
                {
                    // ...otherwise, they exited from the SAME side, meaning they backed out.
                    Debug.Log("Player backed out through " + exitedTrigger.name + ". Resetting.");
                }

                // The crossing attempt is over, so reset for the next time.
                entryTrigger = null;
            }
        }
    }

    private void ShutDoorPermanently()
{
    isPermanentlyShut = true;

    // Get the SpriteRenderer component from the doorVisual object
    SpriteRenderer doorSprite = doorSpriteGameObj.GetComponent<SpriteRenderer>();

    if (doorSprite != null)
    {
        doorSprite.enabled = true; // Enable the component
    }
    doorCollider.enabled = true;
    Debug.Log("Door is now permanently shut.");
}
}