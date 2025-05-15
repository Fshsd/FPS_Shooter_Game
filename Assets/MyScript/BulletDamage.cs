using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage = 20; // مقدار الضرر اللي تسببه الطلقة

    private void OnCollisionEnter(Collision collision)
    {
        // تحقق إذا الجسم المصطدم فيه يحتوي على EnemyAi
        EnemyAi enemy = collision.collider.GetComponent<EnemyAi>();
        PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }else if (player != null)
        {
            player.TakeDamage(damage);
        }

        // دمر الطلقة بعد الاصطدام
        Destroy(gameObject);
    }
}
