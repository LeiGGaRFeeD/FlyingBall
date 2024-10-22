using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour
{
    public Text totalMoneyText;
    public Text sessionMoneyTextCanvas1;
    public Text sessionMoneyTextCanvas2;
    public GameObject gameOverCanvas;
    public Button continueButton;
    public Button restartButton;
    public GameObject player; // Ссылка на игрока (объект с BouncingBall)
    public ObjectSpawner objectSpawner; // Ссылка на ObjectSpawner

    private BouncingBall bouncingBallScript;
    private float totalMoney;
    private float sessionMoney;
    private bool isGameOver = false;
    private float moneyPerSecond = 10f;
    private bool isCounting = true;

    private void Start()
    {
        gameOverCanvas.SetActive(false);
        continueButton.onClick.AddListener(ContinueGame);
        restartButton.onClick.AddListener(RestartGame);

        bouncingBallScript = player.GetComponent<BouncingBall>();

        // Загрузка сохраненных данных
        totalMoney = PlayerPrefs.GetFloat("TotalMoney", 0f);
        sessionMoney = 0f; // Обнуляем деньги за сессию при старте игры

        // Начинаем спавн объектов
        objectSpawner.StartSpawning();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            AddMoneyOverTime();
            UpdateUI();
        }
    }

    private void AddMoneyOverTime()
    {
        if (isCounting)
        {
            float moneyToAdd = moneyPerSecond * Time.deltaTime;
            totalMoney += moneyToAdd;
            sessionMoney += moneyToAdd;
        }
    }

    private void UpdateUI()
    {
        totalMoneyText.text = $"Total Money: {totalMoney.ToString("F0")}";
        sessionMoneyTextCanvas1.text = $"Session Money: {sessionMoney.ToString("F0")}";
        sessionMoneyTextCanvas2.text = $"Session Money: {sessionMoney.ToString("F0")}";
    }

    public void PlayerHit()
    {
        isGameOver = true;
        isCounting = false;

        // Отключаем управление игроком и спавн объектов при проигрыше
        bouncingBallScript.EnableControl(false);
        objectSpawner.StopSpawning();

        // Сохраняем общее количество денег
        PlayerPrefs.SetFloat("TotalMoney", totalMoney);
        PlayerPrefs.Save();

        gameOverCanvas.SetActive(true);
    }

    private void ContinueGame()
    {
        isGameOver = false;
        isCounting = true;
        gameOverCanvas.SetActive(false);

        // Включаем управление игроком и спавн объектов при продолжении игры
        bouncingBallScript.EnableControl(true);
        objectSpawner.StartSpawning();
    }

    private void RestartGame()
    {
        // Сохраняем перед перезапуском
        PlayerPrefs.SetFloat("TotalMoney", totalMoney);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHit();
    }
}
