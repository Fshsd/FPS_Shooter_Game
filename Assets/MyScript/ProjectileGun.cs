using UnityEngine;
using TMPro;

public class ProjectileGun : MonoBehaviour
{
    public GameObject bullet;

    public float shootForce , upwardForce;

    //Gun Stats
    public float timeBetweenShooting , spread , reloadTime , timeBetweenShots;
    public int magazineSize , bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft , bulletsShot;
    bool shooting , readyToShoot , reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;

    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    //bug fixing 
    public bool allowInvoke = true;


    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        //Set ammo display , if it exists
        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " +magazineSize / bulletsPerTap);
        }
        if (reloading == true)
        {
            ammunitionDisplay.SetText(" Reloading..");

        }
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);


        //Reloading
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }


        //Reload automatically when trying to shoot without ammo
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            Reload();
        }


        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
       readyToShoot = false;

       //Find the exact hit position using a raycast
       Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f , 0.5f , 0)); 
       RaycastHit hit;

       //check if ray hits sometiong
       Vector3 targetPoint;
       if (Physics.Raycast(ray , out hit))
       {
        targetPoint = hit.point; // Shoot hit something
       }
       else
       {
        targetPoint = ray.GetPoint(75); // Shoot go to Air
       }

       //Calculate direction from attackPoint to targetPoing
       Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

       //Calculate spread 
       float x = Random.Range(-spread , spread);
       float y = Random.Range(-spread , spread);

       //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x,y,0);

        //Instantoate bullet/projectile
        GameObject currentBullet = Instantiate(bullet , attackPoint.position , Quaternion.identity);
        SoundManager.Instance.PlaySound("shoot");

        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce , ForceMode.Impulse);

        //Instantiate muzzle flash , if you have one
        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash , attackPoint.position , Quaternion.identity);
        }

       bulletsLeft--;
       bulletsShot++;
    //Invoke resetShot method (if not already invoked)
    if(allowInvoke)
    {
        Invoke("ResetShot" , timeBetweenShooting);
        allowInvoke = false;
    }

    //if more than one bulletsPerTap make sure to repeat shoot function
    if(bulletsShot < bulletsPerTap && bulletsLeft > 0)
    {
        Invoke("Shoot" , timeBetweenShots);
    }

    }


    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        SoundManager.Instance.PlaySound("reload");
        Invoke("ReloadFinished" , reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
