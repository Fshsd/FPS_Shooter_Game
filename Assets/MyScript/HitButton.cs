using UnityEngine;

public class HitButton : MonoBehaviour
{

    [SerializeField] private Animator OpenDoor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        SoundManager.Instance.PlaySound("hitTarget");
        OpenDoor.SetBool("OpenDoor" , true);
        SoundManager.Instance.PlaySound("opendoor");
    }
}
