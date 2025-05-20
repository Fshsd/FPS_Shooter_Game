using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Transform cameraTransform;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;
    public float shakeSpeed = 50f;

    private Vector3 originalPos;
    private float timer = 0f;
    private bool isShaking = false;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        originalPos = cameraTransform.localPosition;
    }

    void Update()
    {
        if (isShaking)
        {
            timer += Time.deltaTime * shakeSpeed;

            float offsetX = Mathf.Sin(timer) * shakeMagnitude;
            cameraTransform.localPosition = originalPos + new Vector3(offsetX, 0f, 0f);

            shakeDuration -= Time.deltaTime;

            if (shakeDuration <= 0f)
            {
                isShaking = false;
                cameraTransform.localPosition = originalPos;
            }
        }
    }

    public void StartShake(float duration, float magnitude, float speed)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeSpeed = speed;
        timer = 0f;
        isShaking = true;
    }
}
