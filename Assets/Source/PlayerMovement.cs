using CrazyGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float followSpeed = 5f; // �������� ���������� �� ��������
    public Collider2D movementBounds; // ���������, �������������� ��������
    private bool isGameActive = false; // ��������� ���������� ����

    private void Start()
    {
        CrazySDK.Init(() => { /** initialization finished callback */ });
    }

    void Update()
    {
        if (isGameActive)
        {
            FollowCursor();
        }
    }

    void FollowCursor()
    {
        // �������� ������� ������� �� ������ � ��������� � � ������� ����������
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = transform.position.z;

        // ������ ���������� ������ � ������� �������
        Vector3 targetPosition = Vector3.Lerp(transform.position, cursorPosition, followSpeed * Time.deltaTime);

        // ������������ �������� ������ ������ ����������
        Vector3 clampedPosition = ClampPositionWithinBounds(targetPosition);
        transform.position = clampedPosition;
    }

    Vector3 ClampPositionWithinBounds(Vector3 position)
    {
        if (movementBounds == null)
        {
            Debug.LogWarning("����������� ��������, �������������� ��������.");
            return position;
        }

        // �������� ������� ����������
        Bounds bounds = movementBounds.bounds;

        // ������������ ������� ������ � �������� ������
        float clampedX = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        float clampedY = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);

        return new Vector3(clampedX, clampedY, position.z);
    }

    public void StartGame()
    {
        isGameActive = true; // �������� ���������� ��� ������ ����
    }

    public void ContGame()
    {
        CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () =>
        {
            Debug.Log("Ad requested");
            Time.timeScale = 0f;

        }, (error) =>
        {
            Debug.Log("Error");

        }, () =>
        {
            // ��������� ������� � ��������������� ����������
            isGameActive = true; // �������� ���������� ����� ���������� �������
            Time.timeScale = 1.0f;
            Debug.Log("Ad finished");
        });
    }

    public void EndGame()
    {
        isGameActive = false; // ��������� ���������� ��� ����� ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��������� ���������� ��� ������������, �� ����� ��� �������� ����� ����� ContGame
        EndGame();
    }
}
