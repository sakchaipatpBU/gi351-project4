using UnityEngine;

public class DirectMove : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 stopPos;
    public bool stopX;
    public bool stopY;
    public bool isGreater;

    public Collider2D _collider2D;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _collider2D.isTrigger = true;

    }
    private void Update()
    {
        if (stopX)
        {
            if (isGreater)
            {
                if (transform.position.x <= stopPos.x)
                {
                    rb.bodyType = RigidbodyType2D.Static;
                    _collider2D.isTrigger = false;
                    return;
                }
            }
            else
            {
                if (transform.position.x >= stopPos.x)
                {
                    rb.bodyType = RigidbodyType2D.Static;
                    _collider2D.isTrigger = false;
                    return;
                }
            }
            
        }
        if (stopY)
        {
            if (isGreater)
            {
                if (transform.position.y <= stopPos.y)
                {
                    rb.bodyType = RigidbodyType2D.Static;
                    _collider2D.isTrigger = false;

                    return;
                }
            }
            else
            {
                if (transform.position.y >= stopPos.y)
                {
                    rb.bodyType = RigidbodyType2D.Static;
                    _collider2D.isTrigger = false;

                    return;
                }
            }
            
        }
        

        rb.linearVelocity = dir * speed;
    }
}
