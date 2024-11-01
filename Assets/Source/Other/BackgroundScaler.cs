using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    public Camera targetCamera; // ������, ��� ������� ������ ������������ ���

    private void Start()
    {
        if (targetCamera == null)
        {
            Debug.LogWarning("������ �� ���������! ��������� ������ ��� ���������� ������ �������.");
            return;
        }

        ScaleBackground();
    }

    private void ScaleBackground()
    {
        // �������� ������� ������
        float height = 2f * targetCamera.orthographicSize;
        float width = height * targetCamera.aspect;

        // ������������� ������� � ������� ����
        transform.position = targetCamera.transform.position + new Vector3(0, 0, 10f); // ������� ��� �� ������
        transform.localScale = new Vector3(width / GetComponent<SpriteRenderer>().bounds.size.x,
                                           height / GetComponent<SpriteRenderer>().bounds.size.y,
                                           1f);
    }
}
