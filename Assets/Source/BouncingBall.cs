using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public float jumpForce = 5f; // ���� ������
    public float forwardForce = 3f; // ���� �������� ������
    public float returnSpeed = 2f; // �������� �������� � �������� ���������
    public Vector3 startPosition; // ��������� ������� ������
    private Rigidbody2D rb; // Rigidbody ������
    private bool isJumping = false; // ���� ��� ������������ ������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // ��������� ��������� �������
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        // ���������� ����� � ��������� ���������
        if (isJumping)
        {
            // ������ ���������� ����� �� ��������� �������
            transform.position = Vector3.Lerp(transform.position, startPosition, returnSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector3(forwardForce, jumpForce, 0); // ������ �������� ������ � �������� ������
            Invoke("StopJump", 0.5f); // ��������� ������ ����� 0.5 �������
        }
    }

    void StopJump()
    {
        isJumping = false; // ��������� ��������� ������
        rb.velocity = Vector3.zero; // �������� ��������
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���������� ����� �� ��������� ��������� ��� ������������ � ������������
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
            rb.velocity = Vector3.zero; // �������� �������� ����� �����������
        }
    }
}
