using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;           // Префаб пули
    public Transform firePoint;               // Точка, из которой появляется пуля
    public Text reloadText;                   // Текст для отображения времени перезарядки
    public float bulletSpeed = 10f;           // Скорость пули
    public float reloadTime = 1f;             // Время перезарядки между выстрелами

    private bool isReloading = false;         // Флаг, указывающий на состояние перезарядки
    private float reloadTimer = 0f;           // Таймер перезарядки

    void Update()
    {
        if (isReloading)
        {
            // Обновляем таймер перезарядки
            reloadTimer -= Time.deltaTime;
            reloadText.text = reloadTimer.ToString("F1"); // Обновляем текст с оставшимся временем

            // Завершаем перезарядку, если таймер достиг нуля
            if (reloadTimer <= 0f)
            {
                isReloading = false;
                reloadText.text = "Ready"; // Пишем "Готово" после завершения перезарядки
            }
        }
        else
        {
            // Проверяем нажатие клавиши "лом" (KeyCode.BackQuote) или "пробел" (KeyCode.Space)
            if (Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        // Создаем пулю в точке firePoint и задаем ей направление вправо
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.right * bulletSpeed;

        // Начинаем перезарядку
        isReloading = true;
        reloadTimer = reloadTime;
    }
}