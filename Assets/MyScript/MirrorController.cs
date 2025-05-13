using UnityEngine;
using ECM2.Examples.FirstPerson;

public class MirrorController : MonoBehaviour
{
    public FirstPersonCharacter originalPlayer;
    private FirstPersonCharacter cloneCharacter;

    void Start()
    {
        cloneCharacter = GetComponent<FirstPersonCharacter>();
    }

    void Update()
    {
        if (originalPlayer == null || cloneCharacter == null)
            return;

            

        if (originalPlayer.enabled == true)
        {
        // احصل على مدخلات اللاعب الأصلية
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // عكس الاتجاه
        Vector3 inputDirection = new Vector3(-inputX, 0f, -inputY);

        // تحويل الاتجاه نسبة لاتجاه النسخة نفسها
        Vector3 worldDirection = transform.TransformDirection(inputDirection).normalized;

        // تمرير الحركة إلى النسخة
        cloneCharacter.SetMovementDirection(worldDirection);      
        }

    }
}
