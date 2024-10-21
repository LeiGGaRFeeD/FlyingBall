using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas; // Ссылка на Canvas с сообщением о проигрыше
    public Text sessionMoneyText; // UI текст для отображения денег за сессию
    public Text totalMoneyText; // UI текст для отображения общего количества денег
    public MoneyManager moneyManager; // Ссылка на MoneyManager для доступа к деньгам
    private bool isGameOver = false; // Флаг, указывающий, проиграна ли игра

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isGameOver)
        {
            isGameOver = true;
            ShowGameOverScreen();
        }
    }

    void ShowGameOverScreen()
    {
        gameOverCanvas.SetActive(true); // Включаем Canvas с проигрышем

        // Обновляем текст на Canvas
        sessionMoneyText.text = "Деньги за сессию: " + moneyManager.GetSessionMoney();
        totalMoneyText.text = "Общее количество: " + moneyManager.GetTotalMoney();

        // Запускаем анимацию, если нужно
        StartCoroutine(AnimateMoneyDisplay());
    }

    private IEnumerator AnimateMoneyDisplay()
    {
        // Анимация уменьшения денег за сессию и увеличения общего количества
        int sessionMoney = moneyManager.GetSessionMoney();
        int totalMoney = moneyManager.GetTotalMoney();

        while (sessionMoney > 0)
        {
            sessionMoney--; // Уменьшаем деньги за сессию
            totalMoney++; // Увеличиваем общее количество
            sessionMoneyText.text = "Деньги за сессию: " + sessionMoney;
            totalMoneyText.text = "Общее количество: " + totalMoney;

            yield return new WaitForSeconds(0.1f); // Задержка для анимации
        }

        // Обновляем финальные значения
        sessionMoneyText.text = "Деньги за сессию: 0";
        totalMoneyText.text = "Общее количество: " + totalMoney;
    }

    public void RestartGame()
    {
        moneyManager.ResetSessionMoney(); // Обнуляем деньги за сессию
        // Здесь можно добавить логику для перезагрузки уровня или начальной позиции игрока
        // Например:
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueGame()
    {
        // Логика для продолжения игры с текущими значениями
        // Здесь можно переместить игрока в безопасное место и сбросить флаг
        isGameOver = false;
        gameOverCanvas.SetActive(false);
        // Перемещение игрока в безопасное место, если необходимо
    }
}
