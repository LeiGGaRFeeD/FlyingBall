using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float speed = 5f; // Скорость движения
    public bool followPlayer = false; // Указывает, следует ли спрайту за игроком
    public float followDistance = 2f; // Расстояние, на котором спрайт будет начинать следовать за игроком
    public float dodgeChance = 0.2f; // Вероятность уклонения (20%)

    private Transform player; // Ссылка на игрока
    public static bool isGameStarted = false; // Статус игры (общий для всех объектов)

    void Start()
    {
        // Находим объект игрока по тегу
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        // Проверяем, началась ли игра
        if (!isGameStarted) return;

        // Если игра началась, продолжаем
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

    // Статический метод для начала игры
    public static void StartGame()
    {
        isGameStarted = true;
    }

    public static void StopGame()
    {
        isGameStarted = false;
    }
}
