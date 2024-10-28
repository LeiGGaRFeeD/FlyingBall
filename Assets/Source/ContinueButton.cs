using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public GameObject buttonToDestroy;       // Кнопка, которую нужно уничтожить
    public Text timerText;                   // UI текст для отображения времени
    public Slider timerSlider;               // Слайдер для отображения прогресса времени
    public float destructionDelay = 5f;      // Время до уничтожения кнопки после столкновения

    private bool isCountingDown = false;     // Отслеживает, начат ли отсчет
    private float timer;                     // Текущее время отсчета

    void Start()
    {
        timer = destructionDelay;
        timerText.text = "";                 // Очищаем текст таймера при старте
        timerSlider.value = 1f;              // Слайдер заполнен полностью в начале
        timerSlider.gameObject.SetActive(false); // Скрываем слайдер до столкновения
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCountingDown)                 // Запускаем отсчет только при первом столкновении
        {
            isCountingDown = true;
            timerSlider.gameObject.SetActive(true); // Отображаем слайдер при столкновении
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
                DestroyButton();             // Уничтожаем кнопку, когда таймер достигает нуля
            }
        }
    }

    private void DestroyButton()
    {
        if (buttonToDestroy != null)
        {
            Destroy(buttonToDestroy);        // Уничтожаем кнопку
        }
        timerText.text = "";                 // Очищаем текст
        timerSlider.gameObject.SetActive(false); // Скрываем слайдер
    }
    public void DestButton()
    {
        Destroy(buttonToDestroy);
        Destroy(timerText);
        Destroy(timerSlider);
    }
}