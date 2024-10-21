using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public int moneyPerSecond = 1; // Количество денег, добавляемых в секунду
    public Text moneyText; // UI текст для отображения денег за сессию
    public Text totalMoneyText; // UI текст для отображения общего количества денег

    private int sessionMoney = 0; // Деньги за сессию
    private int totalMoney = 0; // Общее количество денег

    void Start()
    {
        UpdateTotalMoneyText(); // Обновляем общее количество денег в начале
        StartCoroutine(GenerateMoney()); // Запускаем корутину для генерации денег
    }

    IEnumerator GenerateMoney()
    {
        while (true)
        {
            // Увеличиваем деньги за сессию
            sessionMoney += moneyPerSecond;
            StartCoroutine(UpdateMoneyTextSmoothly(sessionMoney)); // Обновляем текст с плавным переходом
            yield return new WaitForSeconds(1f); // Ждем 1 секунду
        }
    }

    public void UpdateTotalMoneyText()
    {
        totalMoneyText.text = "Общее количество: " + totalMoney.ToString() + "$";
    }

    private IEnumerator UpdateMoneyTextSmoothly(int targetAmount)
    {
        int startAmount = 0;

        // Безопасный разбор текущего количества денег
        if (!int.TryParse(moneyText.text.Replace("Деньги: ", "").Replace("$", ""), out startAmount))
        {
            startAmount = 0; // Если парсинг не удался, начинаем с 0
        }

        float duration = 1f; // Продолжительность анимации (в секундах)
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            int currentAmount = (int)Mathf.Lerp(startAmount, targetAmount, elapsed / duration);
            moneyText.text = "Деньги: " + currentAmount.ToString() + "$";
            yield return null; // Ждем следующего кадра
        }

        // Убедимся, что конечное значение правильно отображается
        moneyText.text = "Деньги: " + targetAmount.ToString() + "$";
    }

    public int GetSessionMoney()
    {
        return sessionMoney; // Возвращаем деньги за сессию
    }

    public int GetTotalMoney()
    {
        return totalMoney; // Возвращаем общее количество денег
    }

    public void AddToTotalMoney()
    {
        totalMoney += sessionMoney; // Добавляем деньги за сессию к общему количеству
        UpdateTotalMoneyText(); // Обновляем отображение общего количества
    }

    public void ResetSessionMoney()
    {
        sessionMoney = 0; // Сбрасываем деньги за сессию
        UpdateMoneyTextSmoothly(sessionMoney); // Обновляем текст с плавным переходом
    }
}
