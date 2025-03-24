using UnityEngine;
using System.Collections;
public class SetTurret : MonoBehaviour
{
    public GameObject turretPrefab;

    private bool isSpawned = false;
    private bool canPlaceOrPickUp = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canPlaceOrPickUp)
        {
            if (!isSpawned)
            {
                Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(turretPrefab, spawnPosition, Quaternion.identity);
                isSpawned = true;

                StartCoroutine(PlacementCooldown());  // Запускаем таймер после установки
            }
        }

        if (Input.GetMouseButtonDown(1) && canPlaceOrPickUp)
        {
            if (isSpawned)
            {
                GameObject turret = GameObject.FindWithTag("Turret");
                if (turret != null)
                {
                    Destroy(turret);
                    isSpawned = false;

                    StartCoroutine(PlacementCooldown());  // Запускаем таймер после удаления
                }
            }
        }
    }
    private IEnumerator PlacementCooldown()
    {
        canPlaceOrPickUp = false;
        yield return new WaitForSeconds(1f);  // Задержка в 1 секунду
        canPlaceOrPickUp = true;
    }
}
