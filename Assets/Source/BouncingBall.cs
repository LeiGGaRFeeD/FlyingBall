using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public float jumpForce = 5f; // Сила прыжка
    public float forwardForce = 3f; // Сила движения вперед
    public float returnSpeed = 2f; // Скорость возврата в исходное положение
    public Vector3 startPosition; // Начальная позиция шарика
    private Rigidbody2D rb; // Rigidbody шарика
    private bool isJumping = false; // Флаг для отслеживания прыжка

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Сохраняем начальную позицию
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        // Возвращаем шарик в начальное положение
        if (isJumping)
        {
            // Плавно возвращаем шарик на начальную позицию
            transform.position = Vector3.Lerp(transform.position, startPosition, returnSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector3(forwardForce, jumpForce, 0); // Задаем скорость прыжка и движение вперед
            Invoke("StopJump", 0.5f); // Остановим прыжок через 0.5 секунды
        }
    }

    void StopJump()
    {
        isJumping = false; // Разрешаем следующий прыжок
        rb.velocity = Vector3.zero; // Обнуляем скорость
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Возвращаем шарик на начальное положение при столкновении с поверхностью
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
            rb.velocity = Vector3.zero; // Обнуляем скорость после приземления
        }
    }
}
