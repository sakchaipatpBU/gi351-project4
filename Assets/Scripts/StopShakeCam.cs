using UnityEngine;

public class StopShakeCam : MonoBehaviour
{
    public CameraController cameraController;

    private void Start()
    {
        if(cameraController == null)
        {
            cameraController = Camera.main.GetComponent<CameraController>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cameraController.StopCameraShakingCoroutine();
    }
}
