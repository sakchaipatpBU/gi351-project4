using UnityEngine;

 public class Projectile2D : MonoBehaviour
 {
     public float Speed;
     public float Timer;
     private Rigidbody2D rb;

     void Start()
     {
         rb = GetComponent<Rigidbody2D>();
         rb.linearVelocity = transform.up * Speed;
         Destroy(gameObject, Timer);
     }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("turret") || collision.CompareTag("Projectile") || collision.CompareTag("Laser") || collision.CompareTag("Button") || collision.CompareTag("Timer"))
        {
            return;
        }
        else
        {
            //SoundManager.Instance.PlaySFX("Projectile_Hit", 0.2f, 1); // Sound

            Destroy(gameObject);
        }
        
     }
 }