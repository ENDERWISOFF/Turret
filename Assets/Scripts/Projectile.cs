// Projectile.cs
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speedProjectile = 5f;
    [SerializeField] private float damage = 3f;
    private Enemy targetEnemy;

    public void Initialize(Enemy enemy)
    {
        targetEnemy = enemy;
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            Vector3 direction = (targetEnemy.transform.position - transform.position).normalized;
            transform.Translate(direction * speedProjectile * Time.deltaTime, Space.World);
        }
        else
        {
            Destroy(gameObject); // Уничтожаем снаряд, если цель исчезла
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}