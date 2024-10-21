using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // Массив префабов для спавна
    public Transform[] spawnPoints; // Массив точек спавна
    public float spawnInterval = 2f; // Интервал спавна в секундах

    void Start()
    {
        StartCoroutine(SpawnObjects()); // Запускаем корутину для спавна объектов
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnRandomObject(); // Спавн случайного объекта
            yield return new WaitForSeconds(spawnInterval); // Ждем заданный интервал
        }
    }

    void SpawnRandomObject()
    {
        // Проверяем, есть ли префабы и точки спавна
        if (prefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Нет доступных префабов или точек спавна!");
            return;
        }

        // Выбираем случайный префаб
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];

        // Выбираем случайную точку спавна
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Спавним объект в случайной точке
        Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
