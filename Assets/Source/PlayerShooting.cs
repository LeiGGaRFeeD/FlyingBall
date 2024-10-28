using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;           // ������ ����
    public Transform firePoint;               // �����, �� ������� ���������� ����
    public Text reloadText;                   // ����� ��� ����������� ������� �����������
    public float bulletSpeed = 10f;           // �������� ����
    public float reloadTime = 1f;             // ����� ����������� ����� ����������

    private bool isReloading = false;         // ����, ����������� �� ��������� �����������
    private float reloadTimer = 0f;           // ������ �����������

    void Update()
    {
        if (isReloading)
        {
            // ��������� ������ �����������
            reloadTimer -= Time.deltaTime;
            reloadText.text = reloadTimer.ToString("F1"); // ��������� ����� � ���������� ��������

            // ��������� �����������, ���� ������ ������ ����
            if (reloadTimer <= 0f)
            {
                isReloading = false;
                reloadText.text = "Ready"; // ����� "������" ����� ���������� �����������
            }
        }
        else
        {
            // ��������� ������� ������� "���" (KeyCode.BackQuote) ��� "������" (KeyCode.Space)
            if (Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        // ������� ���� � ����� firePoint � ������ �� ����������� ������
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.right * bulletSpeed;

        // �������� �����������
        isReloading = true;
        reloadTimer = reloadTime;
    }
}