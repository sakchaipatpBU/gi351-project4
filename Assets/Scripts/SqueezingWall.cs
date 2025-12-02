using UnityEngine;
using UnityEngine.UIElements;

public class SqueezingWall : MonoBehaviour
{

    public Transform topWall;
    public Transform lowerWall;

    public Collider2D UpperCollider2D;
    public Collider2D LowerCollider2D;
    public float MiddlePoint;
    public float SqueezingSpeed;

    public bool isSqueezing = false;

    [SerializeField] private CameraController cameraController;
    public float shakingTime = 5;
    public bool isShaking = false;

    private void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isShaking)
            {
                cameraController.StartShaking(shakingTime);
                isShaking = true;
            }
            float range = (topWall.position.y - lowerWall.position.y) / 2;
            MiddlePoint = topWall.position.y - range;
            isSqueezing = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraController.StopCameraShakingCoroutine();
        }
    }
    void FixedUpdate()
    {
        if (isSqueezing)
        {
            Squeezing();
            
        }
    }

    private void Squeezing()
    {

        topWall.Translate(Vector3.down * SqueezingSpeed * Time.deltaTime);
        lowerWall.Translate(Vector3.up * SqueezingSpeed * Time.deltaTime);

        // if in middle stop squeeze
        if (topWall.position.y <= MiddlePoint && lowerWall.position.y >= MiddlePoint)
        {
            isSqueezing = false;
        }
    }
    
}
