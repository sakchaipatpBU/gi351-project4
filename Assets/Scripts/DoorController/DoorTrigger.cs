using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    // The main controller script on the parent object
    private DoorController doorController;
    private Collider2D triggerCollider;

    void Awake()
    {
        // Get the controller from the parent object
        doorController = GetComponentInParent<DoorController>();
        triggerCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Tell the main controller THAT I was entered
            doorController.OnPlayerEnterTrigger(triggerCollider);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Tell the main controller THAT I was exited
            doorController.OnPlayerExitTrigger(triggerCollider);
        }
    }
}