using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] protected EnemyData data; // ScriptableObject с параметрами

    protected Transform player;
    protected Transform turret;
    protected Transform target;
    protected float currentHealth;

    protected virtual void Start()
    {
        currentHealth = data.MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        turret = GameObject.FindGameObjectWithTag("Turret").transform;
    }

    protected virtual void Update()
    {
        UpdateTarget();
        Move();
    }

    protected abstract void UpdateTarget();
    protected abstract void Move();

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Destroy(gameObject);
    }
}