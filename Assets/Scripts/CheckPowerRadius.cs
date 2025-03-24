using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CheckPowerRadius : MonoBehaviour
{
    [Header("Visualization")]
    [SerializeField] private bool showRadius = true;
    [SerializeField] private Color radiusColor = Color.red;
    [SerializeField] private int segments = 50; // Чем больше, тем плавнее круг

    private Turret turret;
    private CircleCollider2D col;
    private LineRenderer lineRenderer;

    void Start()
    {
        turret = GetComponentInParent<Turret>();
        col = GetComponent<CircleCollider2D>();

        if (turret == null) Debug.LogError("Turret не найдена в родителях!");
        if (col == null) Debug.LogError("CircleCollider2D не найден!");

        CreateRadiusVisual();
    }

    void CreateRadiusVisual()
    {
        if (!showRadius || col == null) return;

        // Создаем и настраиваем LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"))
        {
            color = radiusColor
        };

        // Рассчитываем позиции точек
        float radius = col.radius;
        float x, y;
        float angle = 0f;

        lineRenderer.positionCount = segments + 1;

        for (int i = 0; i < segments + 1; i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += (360f / segments);
        }
    }

    // Остальной код без изменений
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Игрок вошёл в радиус турели");
            turret.isCanShoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Игрок вышел из радиуса турели");
            turret.isCanShoot = false;
        }
    }
}