using UnityEngine;
using Unity.Cinemachine;
using Unity.Mathematics;


public class BulletControl : MonoBehaviour
{
    public float initialSpeed = 20f;
    public float minSpeed = 5f;
    public float decelerationRate = 5f;
    public float moveSpeed = 5f;

    public float autoExplodeTime = 5f;
    public GameObject explosionEffect;

    public GameObject player;
    private float currentSpeed;

    private Rigidbody rb;

    public CinemachineCamera playerCam;
    public CinemachineCamera bulletCam;

    public BulletShooter bulletShooter;

    public Transform camTarget;

    public float rotationSpeed = 6f;  // درجة/ثانية
    public float smoothTime = 5f;      // كل ما زاد صار أبطأ

    private Quaternion targetRotation;

    public Transform bulletCamTarget;  // عيّنه من الـ Inspector


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bulletCam = GameObject.Find("BulletCam").GetComponent<CinemachineCamera>();
        // player = GameObject.Find("First Person Character");
        playerCam = GameObject.Find("PlayerCamera").GetComponent<CinemachineCamera>();
        bulletShooter = GameObject.Find("First Person Character").GetComponent<BulletShooter>();
        Invoke(nameof(Explode), autoExplodeTime);
        bulletCamTarget = GameObject.Find("BulletCam").GetComponent<Transform>();
        targetRotation = bulletCamTarget.rotation;
    }

    void Start()
    {

        currentSpeed = initialSpeed;
        rb.linearVelocity = transform.forward * currentSpeed;

        // bulletCam.transform.SetParent(null);

    }


    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");  // A/D أو الأسهم

        if (input != 0 && bulletCamTarget != null)
        {
            // نضيف الدوران حول محور Y بناءً على الاتجاه وسرعة الدوران
            targetRotation *= Quaternion.Euler(0f, input * rotationSpeed * Time.deltaTime, 0f);
        }

        // سموث لفة للكاميرا
        if (bulletCamTarget != null)
        {
            bulletCamTarget.rotation = Quaternion.Lerp(bulletCamTarget.rotation, targetRotation, Time.deltaTime * smoothTime);
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, y, 0f);
        rb.linearVelocity += (transform.right * move.x + transform.up * move.y) * moveSpeed * Time.deltaTime;

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
        CancelInvoke(nameof(Explode));
        Explode();
    }

    void Explode()
    {
        if (explosionEffect)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        // إعادة تفعيل كاميرا اللاعب
        if (playerCam) playerCam.gameObject.SetActive(true);

        // تعطيل كاميرا الطلقة
        if (bulletCam) bulletCam.gameObject.SetActive(false);

        // إعادة تمكين الـ BulletShooter
        // تدمير الطلقة
        Destroy(gameObject);
        bulletShooter.ResetShoot();
    }

    public void ResetSpeed()
    {
        currentSpeed = initialSpeed;  // تعيين السرعة إلى السرعة الأولية
        rb.linearVelocity += transform.forward * currentSpeed; // إعادة تعيين السرعة إلى الاتجاه الأمامي
    }


}
