using UnityEngine;

public class EnterRoomTimer : MonoBehaviour
{
    public GameManager gameManager;
    public float timer = 30f;

    private void Start()
    {
        gameManager = GameManager.GetInstance();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.StartTimer(timer);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.StopTimerInRoomCoroutine();
        }
        
    }
}
