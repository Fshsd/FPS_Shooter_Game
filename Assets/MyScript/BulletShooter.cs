using UnityEngine;
using Unity.Cinemachine;
using ECM2.Examples.FirstPerson;
using System.Collections;
using TMPro;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject cloneBulletPrefab;

    public FirstPersonCharacter firstPersonCharacter;
    public Transform firePoint;

    public Transform camTarget;

    public BulletControl bulletControl;

    public CinemachineCamera playerCam;
    public CinemachineCamera bulletCam;

    private bool canShoot = true;
    private bool canSpawnClone = true;

    private bool whatIsWeapon = false;

    [SerializeField] private GameObject Clone;
    [SerializeField] private TextMeshProUGUI CoolDownClone;
    [SerializeField] private RectTransform CoolDownClone1;

    [SerializeField] private GameObject BulletControl;

    public float cloneCooldownTime = 15f;





    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (whatIsWeapon)
            {
                whatIsWeapon = false;
                Clone.SetActive(false);
                BulletControl.SetActive(true);
            }
            else
            {
                whatIsWeapon = true;
                Clone.SetActive(true);
                BulletControl.SetActive(false);
            }
        }

        if (canSpawnClone && Input.GetMouseButtonDown(1) && whatIsWeapon)
        {
            canSpawnClone = false;
            GameObject bullet = Instantiate(cloneBulletPrefab, firePoint.position, firePoint.rotation);
            SoundManager.Instance.PlaySound("clone");
            StartCoroutine(CloneCooldown());

            // لو بغيت تتحكم فيها بكاميرا مثلاً أضف نفس خطوات الكاميرا
        }

        if (canShoot && Input.GetMouseButtonDown(1) && whatIsWeapon == false)
        {
            firstPersonCharacter.enabled = false;
            // Disable shooting until the next shot
            canShoot = false;
            // Instantiate the bullet
            SoundManager.Instance.PlaySoundLoop("bullet");

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);


            // bulletCam.transform.SetParent(null);

            // camTarget = bullet.transform.Find("BulletCamTarget");

            // camTarget.position = playerCam.transform.position;
            // camTarget.rotation = playerCam.transform.rotation;


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

IEnumerator CloneCooldown()
{
    float cooldown = cloneCooldownTime;

        while (cooldown > 0)
        {
            CoolDownClone.SetText(Mathf.Ceil(cooldown).ToString() + "s");
            yield return new WaitForSeconds(1f);
            cooldown -= 1f;
            if (cooldown == 0)
            {
                CoolDownClone.gameObject.SetActive(false);
                CoolDownClone1.gameObject.SetActive(false);
            }
            else
            {
                CoolDownClone.gameObject.SetActive(true);
                CoolDownClone1.gameObject.SetActive(true);
            }
    }

    CoolDownClone.SetText(""); // إخفاء العداد بعد الانتهاء
    canSpawnClone = true;
}

    public void ResetCloneShoot()
    {
        canSpawnClone = true;
    }

    public void ResetShoot()
    {
        firstPersonCharacter.enabled = true;
        canShoot = true;
        SoundManager.Instance.StopSoundLoop();
    }
}
