using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;
    public float difficultyIncreaseRate = 0.05f;
    public float minimumSpawnInterval = 0.5f;
    public float maxSpawnInterval = 2f; // Максимальный интервал для ограничения

    private Coroutine spawnCoroutine;
    private float currentSpawnInterval;
    private bool isGameActive = false; // Проверка активности игры

    void Start()
    {
        currentSpawnInterval = spawnInterval;
    }

    public void StartGame()
    {
        if (!isGameActive)
        {
            isGameActive = true;
            StartSpawning();
        }
    }

    public void EndGame()
    {
        isGameActive = false;
        StopSpawning();
    }

    public void StartSpawning()
    {
        if (spawnCoroutine == null && isGameActive)
        {
            spawnCoroutine = StartCoroutine(SpawnObjects());
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnRandomObject();

            // Увеличиваем сложность
            currentSpawnInterval = Mathf.Clamp(currentSpawnInterval - difficultyIncreaseRate, minimumSpawnInterval, maxSpawnInterval);

            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    void SpawnRandomObject()
    {
        if (prefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Нет доступных префабов или точек спавна!");
            return;
        }

        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
