using UnityEngine;
using Unity.Cinemachine;


public class BulletControl : MonoBehaviour
{
    public float initialSpeed = 20f;
    public float minSpeed = 5f;
    public float decelerationRate = 5f;
    public float moveSpeed = 5f;
    public GameObject explosionEffect;
    private float currentSpeed;

    private Rigidbody rb;

    public CinemachineCamera playerCam;
    public CinemachineCamera bulletCam;

    public BulletShooter bulletShooter;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
         rb = GetComponent<Rigidbody>();
        bulletCam = GameObject.Find("BulletCam").GetComponent<CinemachineCamera>();
        playerCam = GameObject.Find("PlayerCamera").GetComponent<CinemachineCamera>();
        bulletShooter = GameObject.Find("First Person Character").GetComponent<BulletShooter>();

    }

    void Start()
    {
       
    currentSpeed = initialSpeed;
    rb.linearVelocity = transform.forward * currentSpeed;


    }
    

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, y, 0f);
        rb.linearVelocity += (transform.right * move.x + transform.up * move.y) * moveSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (currentSpeed > minSpeed)
        {
            currentSpeed -= decelerationRate * Time.fixedDeltaTime;
            if (currentSpeed < minSpeed)
                currentSpeed = minSpeed;
        }

        Vector3 forwardVelocity = transform.forward * currentSpeed;
        Vector3 lateralVelocity = Vector3.ProjectOnPlane(rb.linearVelocity, transform.forward);
        rb.linearVelocity = forwardVelocity + lateralVelocity;
    }

void OnCollisionEnter(Collision collision)
{
    if (explosionEffect)
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

    // إعادة تفعيل كاميرا اللاعب
    if (playerCam) playerCam.gameObject.SetActive(true);

    // تعطيل كاميرا الطلقة
    if (bulletCam) bulletCam.gameObject.SetActive(false);

    // إعادة تمكين الـ BulletShooter
    bulletShooter.ResetShoot();

    // تدمير الطلقة
    Destroy(gameObject);
}


public void ResetSpeed()
{
    currentSpeed = initialSpeed;  // تعيين السرعة إلى السرعة الأولية
    rb.linearVelocity += transform.forward * currentSpeed; // إعادة تعيين السرعة إلى الاتجاه الأمامي
}

    
}
