using CrazyGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float followSpeed = 5f; // Скорость следования за курсором
    public Collider2D movementBounds; // Коллайдер, ограничивающий движение
    private bool isGameActive = false; // Индикатор активности игры

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
        // Получаем позицию курсора на экране и переводим её в мировые координаты
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = transform.position.z;

        // Плавно перемещаем игрока к позиции курсора
        Vector3 targetPosition = Vector3.Lerp(transform.position, cursorPosition, followSpeed * Time.deltaTime);

        // Ограничиваем движение внутри границ коллайдера
        Vector3 clampedPosition = ClampPositionWithinBounds(targetPosition);
        transform.position = clampedPosition;
    }

    Vector3 ClampPositionWithinBounds(Vector3 position)
    {
        if (movementBounds == null)
        {
            Debug.LogWarning("Отсутствует движение, ограничивающее движение.");
            return position;
        }

        // Получаем границы коллайдера
        Bounds bounds = movementBounds.bounds;

        // Ограничиваем позицию игрока в пределах границ
        float clampedX = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        float clampedY = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);

        return new Vector3(clampedX, clampedY, position.z);
    }

    public void StartGame()
    {
        isGameActive = true; // Включаем управление при старте игры
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
            // Завершаем рекламу и восстанавливаем управление
            isGameActive = true; // Включаем управление после завершения рекламы
            Time.timeScale = 1.0f;
            Debug.Log("Ad finished");
        });
    }

    public void EndGame()
    {
        isGameActive = false; // Отключаем управление при конце игры
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Отключаем управление при столкновении, но можно его включить снова через ContGame
        EndGame();
    }
}
