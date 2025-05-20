using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PuzzleClone2 : MonoBehaviour
{

    [SerializeField] private Animator CubeDown;
    [SerializeField] private GameObject RIP;

    [SerializeField] private CinemachineCamera cinemachineCamera;

    public CameraShaker shaker; // اسحب المكون من الـ Inspector
    [SerializeField] private CinemachineCamera PlayerCinemachineCamera;

    [SerializeField] private Animator RipUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cinemachineCamera.gameObject.SetActive(false);
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


        StartCoroutine(Scene1());
        Invoke("PlayRipUpAnimation", 1f); // شغّل بعد ثانيتين
    }

    IEnumerator Scene1()
    {
        PlayerCinemachineCamera.gameObject.SetActive(false);
        cinemachineCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f); // يشغل الكاميرا 3 ثواني

        cinemachineCamera.gameObject.SetActive(false);
        PlayerCinemachineCamera.gameObject.SetActive(true);

    }

    void PlayRipUpAnimation()
{
    shaker.StartShake(2f, 0.2f, 70f); // مدة 1 ثانية، شدة 0.3، وسرعة 80
    RipUp.SetBool("RIPUp", true);
}



    // void Scene1()
    // {
    //     cinemachineCamera.gameObject.SetActive(true);
    // }

}
