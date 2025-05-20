using UnityEngine;
using TMPro;
using ECM2.Examples.FirstPerson;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;
using System.Collections;



public class RIP : MonoBehaviour
{
    [SerializeField] private float rangeRadius = 2;
    [SerializeField] private Transform rangeCenter;

    // [SerializeField] private FirstPersonCharacter 
    [SerializeField] private GameObject interactUI;

    [SerializeField] private Transform player;

        [SerializeField] private GameObject playerObj;


    [SerializeField] private CinemachineCamera playerCam;

    [SerializeField] private CinemachineCamera RIPCam;

    private bool isInRange = false;

    [SerializeField] private Animator closeEye;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, rangeCenter.position);
        isInRange = distance <= rangeRadius;

        interactUI.SetActive(isInRange);

            if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartRIP();
        }

    }

        void StartRIP()
    {
        // interactUI.SetActive(false);

        //change cam
        playerObj.SetActive(false);
        playerCam.gameObject.SetActive(false);

        RIPCam.gameObject.SetActive(true);

        closeEye.SetBool("CloseEye" , true);
    StartCoroutine(LoadSceneAfterAnimation(3.5f)); // ← حدد المدة بدقة
}

IEnumerator LoadSceneAfterAnimation(float delay)
{
    yield return new WaitForSeconds(delay);
    SceneManager.LoadScene("LastScene");
}



    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rangeCenter.position, rangeRadius);
    }
}
