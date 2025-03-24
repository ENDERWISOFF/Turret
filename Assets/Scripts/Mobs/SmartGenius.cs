using UnityEngine;

public class SmartGenius : Enemy
{
    protected override void UpdateTarget()
    {
        // Если Turret не найден, пытаемся его найти
        if (turret == null)
        {
            turret = GameObject.FindGameObjectWithTag("Turret")?.transform;
        }

        // Устанавливаем цель: сначала Turret, если он есть, иначе Player
        if (turret != null)
        {
            target = turret;
        }
        else
        {
            // Если Player не найден, пытаемся найти
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player")?.transform;
            }
            target = player;
        }

        // Если оба отсутствуют, target останется null
    }

    protected override void Move()
    {
        if (target == null)
        {
            return; // Не двигаемся, если нет цели
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * data.Speed * Time.deltaTime;
    }
}