using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Positioning")]
    public Vector3 offset;
    public float smoothSpeed = 0.125f; // Renamed from smoothDuration for clarity

    [Header("Map Boundaries")]
    public float mapMinX;
    public float mapMaxX;
    public float mapMinY;
    public float mapMaxY;

    [Header("Shake")]
    public AnimationCurve curve;
    public float ShakeDuration = 1.0f;
    [SerializeField]
    private bool isShaking = false;
    private float startShakingTime;
    private Coroutine cameraShakingCoroutine;


    // --- Private Variables ---
    private Camera cam;
    private float camHeight;
    private float camWidth;

    void Start()
    {
        // Get the camera component and calculate its size in world units
        cam = GetComponent<Camera>();
        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    void LateUpdate()
    {
        // 1. Calculate the camera's desired position
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 2. Calculate the actual boundary limits for the camera's center point
        //    This subtracts half the camera's size from the map edge.
        float clampedX = Mathf.Clamp(smoothedPosition.x, mapMinX + camWidth, mapMaxX - camWidth);
        float clampedY = Mathf.Clamp(smoothedPosition.y, mapMinY + camHeight, mapMaxY - camHeight);

        // 3. Apply the final, clamped position
        if (isShaking)
        {
            Vector3 shake = Shaking();
            clampedX += shake.x;
            clampedY += shake.y;
        }
        transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
    }
    #region Shake
    public void StartShaking(float d)
    {
        ShakeDuration = d;
        StopCameraShakingCoroutine();
        cameraShakingCoroutine = StartCoroutine(ShakeCoroutine());
    }
    public void StopCameraShakingCoroutine()
    {
        if (cameraShakingCoroutine != null)
        {
            StopCoroutine(cameraShakingCoroutine);
            isShaking = false;

        }
    }

    IEnumerator ShakeCoroutine()
    {
        isShaking = true;
        startShakingTime = Time.time;
        yield return new WaitForSeconds(ShakeDuration);
        isShaking = false;
    }
    private Vector3 Shaking()
    {
        float strength = curve.Evaluate(Time.time - startShakingTime / ShakeDuration);
        Vector3 s = Random.insideUnitSphere * strength;
        return s;
    }

    #endregion
}