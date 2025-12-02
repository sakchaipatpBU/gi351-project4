using UnityEngine;

public class FireBallShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float fireRate;
    private float nextFireTime = 0f;

    public AudioSource audioSource;

    void Update()
    {
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
        //audioSource.Play(); // sound

        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
