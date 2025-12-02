using UnityEngine;

public class SpinLaser : MonoBehaviour
{
    public float SpinSpeed = -1;
    void FixedUpdate()
    {
        transform.Rotate(0, 0, SpinSpeed);
    }
}
