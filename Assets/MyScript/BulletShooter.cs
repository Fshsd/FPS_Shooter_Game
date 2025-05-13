using UnityEngine;
using Unity.Cinemachine;
using ECM2.Examples.FirstPerson;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;

    public FirstPersonCharacter firstPersonCharacter;
    public Transform firePoint;

    public BulletControl bulletControl;

    public CinemachineCamera playerCam;
    public CinemachineCamera bulletCam;

    private bool canShoot = true;

void Update()
{
    if (canShoot && Input.GetMouseButtonDown(0))
    {
        firstPersonCharacter.enabled = false;
        // Disable shooting until the next shot
        canShoot = false;
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

        
    }
}


    public void ResetShoot()
    {
        firstPersonCharacter.enabled = true;
        canShoot = true;
    }
}
