using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // ������ �������� ��� ������
    public Transform[] spawnPoints; // ������ ����� ������
    public float spawnInterval = 2f; // �������� ������ � ��������

    void Start()
    {
        StartCoroutine(SpawnObjects()); // ��������� �������� ��� ������ ��������
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
