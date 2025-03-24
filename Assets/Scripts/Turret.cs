using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string turretName;
    [SerializeField] private float fireRate = 1.0f;
    [SerializeField] private float health = 100;
    [SerializeField] private float rotateSpeed = 360f; // Увеличена скорость поворота
    [SerializeField] private float aimingThreshold = 3f; // Порог точности наведения

    [Header("References")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    private Coroutine shootingCoroutine;
    private Enemy targetEnemy;
    public bool isCanShoot = false;

    private void Start()
    {
        shootingCoroutine = StartCoroutine(ShootRoutine());
    }

    private void Update()
    {
        RotateToTarget(); // Обновляем поворот каждый кадр
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            SelectTarget();
            TryShoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void SelectTarget()
    {
        targetEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemyObj in enemies)
        {
            float distance = Vector3.Distance(firePoint.position, enemyObj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetEnemy = enemyObj.GetComponent<Enemy>();
            }
        }
    }

    private void RotateToTarget()
    {
        if (targetEnemy == null) return;

        Vector3 direction = targetEnemy.transform.position - firePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Корректировка угла
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }

    private void TryShoot()
    {
        if (!isCanShoot || targetEnemy == null) return;
        if (!IsAimed()) return;

        if (projectilePrefab && firePoint)
        {
            GameObject projectileObj = Instantiate(
                projectilePrefab,
                firePoint.position,
                firePoint.rotation
            );

            if (projectileObj.TryGetComponent(out Projectile projectile))
            {
                projectile.Initialize(targetEnemy);
            }
            else
            {
                Debug.LogError("Projectile component missing!");
                Destroy(projectileObj);
            }
        }
    }

    private bool IsAimed()
    {
        if (targetEnemy == null) return false;

        Vector3 directionToTarget = (targetEnemy.transform.position - firePoint.position).normalized;
        float angle = Vector3.Angle(firePoint.up, directionToTarget);
        return angle < aimingThreshold;
    }

    private void OnDestroy()
    {
        if (shootingCoroutine != null)
            StopCoroutine(shootingCoroutine);
    }

    public void SetFireRate(float newRate)
    {
        fireRate = newRate;
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = StartCoroutine(ShootRoutine());
        }
    }
}