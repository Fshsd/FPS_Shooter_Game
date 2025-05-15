using UnityEngine;
using TMPro;

public class ScrollCredits : MonoBehaviour
{
    public float scrollSpeed = 50f;

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}
