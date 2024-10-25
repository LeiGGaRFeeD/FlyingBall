using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float speed = 5f; // �������� ��������
    public bool followPlayer = false; // ���������, ������� �� ������� �� �������
    public float followDistance = 2f; // ����������, �� ������� ������ ����� �������� ��������� �� �������
    public float dodgeChance = 0.2f; // ����������� ��������� (20%)

    private Transform player; // ������ �� ������
    public static bool isGameStarted = false; // ������ ���� (����� ��� ���� ��������)

    void Start()
    {
        // ������� ������ ������ �� ����
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        // ���������, �������� �� ����
        if (!isGameStarted) return;

        // ���� ���� ��������, ����������
        if (followPlayer && player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer < followDistance)
            {
                if (Random.value > dodgeChance)
                {
                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;
                }
            }
            else
            {
                MoveLeft();
            }
        }
        else
        {
            MoveLeft();
        }
    }

    void MoveLeft()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    // ����������� ����� ��� ������ ����
    public static void StartGame()
    {
        isGameStarted = true;
    }

    public static void StopGame()
    {
        isGameStarted = false;
    }
}
