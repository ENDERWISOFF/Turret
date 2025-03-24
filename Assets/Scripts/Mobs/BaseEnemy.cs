using UnityEngine;

public class BaseEnemy : Enemy
{
    protected override void UpdateTarget()
    {
        target = player; // Всегда атакует игрока
    }

    protected override void Move()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * data.Speed * Time.deltaTime;
    }
}
