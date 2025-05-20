using UnityEngine;

public class PuzzleClone : MonoBehaviour
{

    [SerializeField] private Animator CubeDown;
    [SerializeField] private GameObject Glass;
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

        Destroy(Glass);
        SoundManager.Instance.PlaySound("glass");
        Destroy(gameObject);

    
}

}
