using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // ������ �������� ��� ������
    public Transform[] spawnPoints; // ������ ����� ������
    public float spawnInterval = 2f; // �������� ������ � ��������

    private Coroutine spawnCoroutine; // ������ �������� ��� ��������� � �������������

    void Start()
    {
        StartSpawning(); // ��������� ����� ��������
    }

    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnObjects()); // ��������� �������� ��� ������ ��������
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine); // ������������� ����� ��������
            spawnCoroutine = null;
        }
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnRandomObject(); // ����� ���������� �������
            yield return new WaitForSeconds(spawnInterval); // ���� �������� ��������
        }
    }

    void SpawnRandomObject()
    {
        // ���������, ���� �� ������� � ����� ������
        if (prefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("��� ��������� �������� ��� ����� ������!");
            return;
        }

        // �������� ��������� ������
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];

        // �������� ��������� ����� ������
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // ������� ������ � ��������� �����
        Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
