using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed;

    [Header("Shooting")]
    public GameObject projectilePrefab; // The projectile prefab to be fired.
    public Transform[] firePoints;      // An array of points from where projectiles are shot.
    public float fireRate;         // How many times to shoot per second.

    private float nextFireTime = 0f;

    public AudioSource audioSource;
    public AudioClip audioClip;

    void Update()
    {
        // Rotate the object around the Z-axis
        // Use Time.deltaTime to make the rotation frame-rate independent.
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Check if enough time has passed to fire again.
        if (Time.time >= nextFireTime)
        {
            Shoot();
            // Set the time for the next shot.
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        // Ensure we have a projectile prefab and fire points assigned.
        if (projectilePrefab != null && firePoints.Length > 0)
        {


            // Loop through each fire point.
            foreach (Transform point in firePoints)
            {
                //AudioSource.PlayClipAtPoint(audioClip, transform.position);

                // Create an instance of the projectile at the fire point's position and rotation.
                Instantiate(projectilePrefab, point.position, point.rotation);
            }
        }
    }
}