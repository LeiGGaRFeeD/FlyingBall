using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShield : MonoBehaviour
{
    public Button shieldButton;                // Кнопка щита
    public Text buttonText;                    // Текст на кнопке для отображения времени
    public float shieldDuration = 3f;          // Время действия щита в секундах
    public float cooldownDuration = 5f;        // Время перезарядки в секундах
    public int shieldCost = 50;                // Стоимость активации щита
    public Color activeColor = Color.green;    // Цвет кнопки при активном щите
    public Color cooldownColor = Color.gray;   // Цвет кнопки при перезарядке
    public MoneyManager moneyManager;          // Ссылка на MoneyManager

    private Collider2D playerCollider;         // Коллайдер игрока
    private bool isShieldActive = false;       // Флаг активации щита
    private bool isCooldown = false;           // Флаг перезарядки
    private float shieldTimer;                 // Таймер для времени действия щита
    private float cooldownTimer;               // Таймер для перезарядки

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        shieldButton.onClick.AddListener(TryActivateShield);
        UpdateButtonState();                   // Начальное обновление кнопки
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))  // Проверка нажатия клавиши "1"
        {
            TryActivateShield();
        }

        if (isShieldActive)
        {
            UpdateShieldTimer();               // Обновляем таймер щита
        }
        else if (isCooldown)
        {
            UpdateCooldownTimer();             // Обновляем таймер перезарядки
        }
    }

    private void TryActivateShield()
    {
        // Проверяем, хватает ли денег для покупки щита
        if (moneyManager != null && moneyManager.GetTotalMoney() >= shieldCost && !isShieldActive && !isCooldown)
        {
            moneyManager.SpendMoney(shieldCost); // Списываем стоимость щита из MoneyManager
            StartCoroutine(ActivateShield());
        }
        else if (moneyManager != null && moneyManager.GetTotalMoney() < shieldCost)
        {
            StartCoroutine(ShakeButton());     // Подрагивание кнопки при недостатке средств
        }
    }

    private IEnumerator ActivateShield()
    {
        isShieldActive = true;
        playerCollider.enabled = false;        // Отключаем коллайдер игрока
        shieldButton.interactable = false;     // Делаем кнопку неактивной
        shieldButton.image.color = activeColor;
        shieldTimer = shieldDuration;          // Устанавливаем таймер щита

        while (shieldTimer > 0)
        {
            buttonText.text = $"Active: {shieldTimer.ToString("F1")}s"; // Обновляем текст времени действия
            shieldTimer -= Time.deltaTime;
            yield return null;
        }

        playerCollider.enabled = true;         // Включаем коллайдер игрока
        isShieldActive = false;
        StartCoroutine(StartCooldown());       // Запускаем перезарядку
    }

    private IEnumerator StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = cooldownDuration;
        shieldButton.image.color = cooldownColor;

        while (cooldownTimer > 0)
        {
            buttonText.text = $"Cooldown: {cooldownTimer.ToString("F1")}s"; // Обновляем текст таймера перезарядки
            cooldownTimer -= Time.deltaTime;
            yield return null;
        }

        isCooldown = false;
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        shieldButton.interactable = !isCooldown && !isShieldActive;
        buttonText.text = "Shield";
        shieldButton.image.color = Color.white;
    }

    private void UpdateShieldTimer()
    {
        // Уменьшаем и отображаем оставшееся время действия щита
        shieldTimer -= Time.deltaTime;
        buttonText.text = $"Active: {shieldTimer.ToString("F1")}s";
    }

    private void UpdateCooldownTimer()
    {
        // Уменьшаем и отображаем оставшееся время перезарядки
        cooldownTimer -= Time.deltaTime;
        buttonText.text = $"Cooldown: {cooldownTimer.ToString("F1")}s";
    }

    private IEnumerator ShakeButton()
    {
        Vector3 originalPosition = shieldButton.transform.position;
        float shakeIntensity = 5f;
        float shakeDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeIntensity;
            float offsetY = Random.Range(-1f, 1f) * shakeIntensity;
            shieldButton.transform.position = originalPosition + new Vector3(offsetX, offsetY, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        shieldButton.transform.position = originalPosition;
    }
}
