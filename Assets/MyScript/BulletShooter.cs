using UnityEngine;
using Unity.Cinemachine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public BulletControl bulletControl;

    public CinemachineCamera playerCam;
    public CinemachineCamera bulletCam;

    private bool canShoot = true;

void Update()
{
    if (canShoot && Input.GetMouseButtonDown(0))
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Activate bullet camera and deactivate player camera
        bulletCam.gameObject.SetActive(true);
        playerCam.gameObject.SetActive(false);

        // Set bullet camera to follow the bullet
        bulletCam.Follow = bullet.transform;
        bulletCam.LookAt = bullet.transform;

        // Reset bullet speed
        BulletControl bulletControlScript = bullet.GetComponent<BulletControl>();
        if (bulletControlScript != null)
        {
            bulletControlScript.ResetSpeed();
        }

        // Disable shooting until the next shot
        canShoot = false;
    }
}


    public void ResetShoot()
    {
        canShoot = true;
    }
}
