using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectsOnCollision : MonoBehaviour
{
    public string[] tagsToDestroy; // ������ ����� �������� ��� �����������

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ��� ������������ ��������� � �������
     //   if (collision.gameObject.CompareTag("Player"))
        {
            DestroyObjectsWithTags();
        }
    }

    private void DestroyObjectsWithTags()
    {
        // �������� �� ������� ���� � �������
        foreach (string tag in tagsToDestroy)
        {
            // ������� ��� ������� � ���� �����
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tag);

            // ���������� ������ ��������� ������
            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }
        }
    }
}
