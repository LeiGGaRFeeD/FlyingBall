using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CrazyGames; // Не забудьте добавить CrazyGames SDK

public class ContinueButton : MonoBehaviour
{
    public Button continueButton; // Ссылка на кнопку "Продолжить"
    public float activeDuration = 5f; // Время, в течение которого кнопка будет активной
    private float timer;
    private bool isAdShowing = false;

    void Start()
    {
        continueButton.gameObject.SetActive(false); // Отключаем кнопку при старте игры
        timer = activeDuration;
    }

    void Update()
    {
        if (continueButton.gameObject.activeSelf && !isAdShowing)
        {
            // Уменьшаем таймер
            timer -= Time.deltaTime;

            // Проверяем, если таймер истек
            if (timer <= 0f)
            {
                DeactivateButton(); // Деактивируем кнопку после истечения времени
            }
        }
    }

    public void ActivateButton()
    {
        continueButton.gameObject.SetActive(true); // Включаем кнопку
        timer = activeDuration; // Сбрасываем таймер
    }

   /* public void OnContinueButtonClick() //Need to fix 
    {
        if (!isAdShowing)
        {
            // Ставим игру на паузу
            Time.timeScale = 0f;

            // Отображаем рекламу с использованием CrazyGames SDK
            isAdShowing = true;
            CrazyAds.Instance.beginAdBreak(() => {
                // Callback после завершения рекламы
                ResumeGame();
            });
        }
    }*/

    private void ResumeGame()
    {
        // Продолжаем игру
        Time.timeScale = 1f;
        isAdShowing = false;
        DeactivateButton(); // Деактивируем кнопку после просмотра рекламы
    }

    private void DeactivateButton()
    {
        continueButton.gameObject.SetActive(false); // Отключаем кнопку
    }
}
