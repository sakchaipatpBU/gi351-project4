using UnityEngine;
using UnityEngine.UIElements;

public class RollingStone : MonoBehaviour
{
    public GameObject stone;
    public float rollingTime;
    public Vector3 rollSpeed;

    
    private void Update()
    {
        if(rollingTime > 0)
        {
            rollingTime -= Time.deltaTime;
            stone.transform.Rotate(rollSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Trigger RollingStone");

            GameManager.GetInstance().ShowLosePanel();
        }
    }
}
