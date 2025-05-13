using UnityEngine;
using ECM2.Examples.FirstPerson;


public class CloneBulletControl : MonoBehaviour
{
    public GameObject clonePrefab;

    private Rigidbody rb;
    public float initialSpeed = 50f;


    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.Find("First Person Character");

            rb = GetComponent<Rigidbody>();
    rb.linearVelocity = transform.forward * initialSpeed;
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 spawnPos = transform.position;

        GameObject clone = Instantiate(clonePrefab, spawnPos, player.transform.rotation);

        // أضف سكربت MirrorController للنسخة واربطه باللاعب الأصلي
        MirrorController mirror = clone.AddComponent<MirrorController>();
        mirror.originalPlayer = player.GetComponent<FirstPersonCharacter>();

        Destroy(gameObject);

}

}
