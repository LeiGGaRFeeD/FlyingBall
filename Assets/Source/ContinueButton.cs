using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public GameObject buttonToHide;          // Кнопка, которую нужно переместить
    public Text timerText;                   // UI текст для отображения времени
    public Slider timerSlider;               // Слайдер для отображения прогресса времени
    public float destructionDelay = 5f;      // Время до перемещения кнопки за экран после столкновения
    public Vector3 offScreenPosition = new Vector3(-1000, -1000, 0); // Позиция за пределами экрана

    private bool isCountingDown = false;     // Отслеживает, начат ли отсчет
    private bool hasAppearedOnce = false;    // Отслеживает, была ли кнопка показана ранее
    private float timer;                     // Текущее время отсчета

    void Start()
    {
        timer = destructionDelay;
      //  MoveOffScreen();                     // Перемещаем элементы за пределы экрана при старте
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasAppearedOnce)                // Проверяем, была ли кнопка показана ранее
        {
            isCountingDown = true;
            hasAppearedOnce = true;          // Отмечаем, что кнопка была показана
         //   MoveOnScreen();                  // Перемещаем элементы на экран
        }
    }

    void Update()
    {
        if (isCountingDown)
        {
            timer -= Time.deltaTime;         // Уменьшаем таймер
            timerText.text = timer.ToString("F1"); // Обновляем текст таймера с одним знаком после запятой
            timerSlider.value = timer / destructionDelay; // Обновляем слайдер прогресса

            if (timer <= 0f)
            {
                MoveOffScreen();             // Перемещаем элементы за экран, когда таймер достигает нуля
                isCountingDown = false;      // Останавливаем отсчет
            }
        }
    }

    private void MoveOffScreen()
    {
        // Перемещаем кнопку, таймер и слайдер за пределы экрана
        buttonToHide.transform.position = offScreenPosition;
        timerText.transform.position = offScreenPosition;
        timerSlider.transform.position = offScreenPosition;
    }

    public void MoveOnScreen()
    {
        // Размещаем элементы в видимых позициях
        buttonToHide.transform.position = new Vector3(0, -10000, 0); // Задайте нужную позицию на экране
        timerText.transform.position = new Vector3(0, -10000, 0);  // Задайте нужную позицию для текста
        timerSlider.transform.position = new Vector3(0, -10000, 0); // Задайте нужную позицию для слайдера
    }
}
