using UnityEngine;

public class PuzzleClone2 : MonoBehaviour
{

    [SerializeField] private Animator CubeDown;
    [SerializeField] private GameObject RIP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

void OnTriggerEnter(Collider other)
{
    CubeDown.SetBool("CubeDown", true);
    SoundManager.Instance.PlaySound("cloneTarget");

        RIP.SetActive(true);
    SoundManager.Instance.PlaySound("earthquake");

    
}

}
