using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public GameObject startCanvas; // Стартовый Canvas
    public GameObject gameCanvas; // Игровой Canvas (если есть)
    public Button startButton; // Кнопка "Start"
    public MoneyManager moneyManager; // Ссылка на MoneyManager
    public GameObject player; // Игрок
    public ObjectSpawner objectSpawner; // Ссылка на ObjectSpawner

    private void Start()
    {
        // Подписываемся на нажатие кнопки
        startButton.onClick.AddListener(StartGame);

        // Отключаем игровые элементы до начала игры
        gameCanvas.SetActive(false);
        player.SetActive(false);
        objectSpawner.enabled = false;
    }

    // Метод для начала игры
    private void StartGame()
    {
        // Отключаем стартовый Canvas
        startCanvas.SetActive(false);

        // Включаем игровой Canvas (если нужен)
        gameCanvas.SetActive(true);

        // Включаем игрока и спавн объектов
        player.SetActive(true);
        objectSpawner.enabled = true;

        // Запускаем MoneyManager, если нужно начать отсчет денег
        if (moneyManager != null)
        {
            moneyManager.enabled = true; // Активируем MoneyManager, если он не активен
        }
    }
}
