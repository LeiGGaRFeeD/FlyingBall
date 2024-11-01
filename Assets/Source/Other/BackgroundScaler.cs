using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    public Camera targetCamera; // Камера, под которую должен подстроиться фон

    private void Start()
    {
        if (targetCamera == null)
        {
            Debug.LogWarning("Камера не назначена! Назначьте камеру для корректной работы скрипта.");
            return;
        }

        ScaleBackground();
    }

    private void ScaleBackground()
    {
        // Получаем размеры камеры
        float height = 2f * targetCamera.orthographicSize;
        float width = height * targetCamera.aspect;

        // Устанавливаем позицию и масштаб фона
        transform.position = targetCamera.transform.position + new Vector3(0, 0, 10f); // Смещаем фон за камеру
        transform.localScale = new Vector3(width / GetComponent<SpriteRenderer>().bounds.size.x,
                                           height / GetComponent<SpriteRenderer>().bounds.size.y,
                                           1f);
    }
}
