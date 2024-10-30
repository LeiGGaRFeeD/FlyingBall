using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectsOnCollision : MonoBehaviour
{
    public string[] tagsToDestroy; // Массив тегов объектов для уничтожения

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, что столкновение произошло с игроком
     //   if (collision.gameObject.CompareTag("Player"))
        {
            DestroyObjectsWithTags();
        }
    }

    private void DestroyObjectsWithTags()
    {
        // Проходим по каждому тегу в массиве
        foreach (string tag in tagsToDestroy)
        {
            // Находим все объекты с этим тегом
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tag);

            // Уничтожаем каждый найденный объект
            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }
        }
    }
}
