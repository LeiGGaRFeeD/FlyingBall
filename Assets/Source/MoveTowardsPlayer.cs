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

    void Start()
    {
        // Находим объект игрока по тегу
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Проверяем, должен ли спрайт следовать за игроком
        if (followPlayer && player != null)
        {
            // Рассчитываем расстояние до игрока
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Если игрок в пределах следования
            if (distanceToPlayer < followDistance)
            {
                // Уклонение
                if (Random.value > dodgeChance)
                {
                    // Перемещаемся к игроку
                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;
                }
            }
            else
            {
                // Если игрок далеко, просто перемещаемся влево
                MoveLeft();
            }
        }
        else
        {
            // Если не следуем за игроком, просто перемещаемся влево
            MoveLeft();
        }
    }

    void MoveLeft()
    {
        // Перемещаем спрайт влево
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
