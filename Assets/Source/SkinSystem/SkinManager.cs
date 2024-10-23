using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public Sprite[] skinIcons; // Массив иконок скинов
    public int[] skinPrices; // Массив цен скинов

    public Image skinIcon; // UI-элемент для отображения иконки скина
    public Text skinStatusText; // Текст для вывода статуса скина (цена, "Открыто" или "Выбрано")
    public Text moneyText; // Текст для вывода текущего количества денег
    public Button buyButton; // Кнопка "Купить" или "Выбрать"
    public Button previousButton, nextButton; // Кнопки переключения скинов

    public bool debugMode; // Включить режим Debug для сброса скинов
    private int currentSkinIndex; // Индекс текущего скина
    private bool[] purchasedSkins; // Массив для отслеживания купленных скинов
    private float currentMoney; // Текущие деньги игрока
    private Animator buyButtonAnimator; // Аниматор для кнопки покупки

    void Start()
    {
        LoadSkinData(); // Загружаем данные
        UpdateSkinUI(); // Обновляем интерфейс скинов

        // Добавляем обработчики событий для кнопок
        previousButton.onClick.AddListener(PreviousSkin);
        nextButton.onClick.AddListener(NextSkin);
        buyButton.onClick.AddListener(OnBuyOrSelectButtonClick);

        buyButtonAnimator = buyButton.GetComponent<Animator>(); // Получаем компонент аниматора

        UpdateMoneyUI(); // Обновляем вывод денег
    }

    void Update()
    {
        // Если включен режим Debug и нажата клавиша Y
        if (debugMode && Input.GetKeyDown(KeyCode.Y))
        {
            ResetPurchasedSkins();
        }
    }

    void LoadSkinData()
    {
        // Загружаем деньги из PlayerPrefs
        currentMoney = PlayerPrefs.GetFloat("TotalMoney", 0f);

        // Инициализация массива купленных скинов
        purchasedSkins = new bool[skinIcons.Length];
        for (int i = 0; i < skinIcons.Length; i++)
        {
            purchasedSkins[i] = PlayerPrefs.GetInt("Skin_" + i, 0) == 1; // 1 = куплено, 0 = не куплено
        }

        // Загружаем выбранный скин из PlayerPrefs
        currentSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex", 0);
    }

    void UpdateSkinUI()
    {
        // Устанавливаем иконку текущего скина
        skinIcon.sprite = skinIcons[currentSkinIndex];

        // Проверяем, куплен ли текущий скин
        if (purchasedSkins[currentSkinIndex])
        {
            // Если выбранный скин текущий — пишем "Выбрано"
            if (PlayerPrefs.GetInt("SelectedSkinIndex") == currentSkinIndex)
            {
                skinStatusText.text = "Выбрано";
                buyButton.GetComponentInChildren<Text>().text = "Выбрать";
            }
            else
            {
                skinStatusText.text = "Открыто";
                buyButton.GetComponentInChildren<Text>().text = "Выбрать";
            }
        }
        else
        {
            skinStatusText.text = "Цена: " + skinPrices[currentSkinIndex] + " монет";
            buyButton.GetComponentInChildren<Text>().text = "Купить";
        }
    }

    void UpdateMoneyUI()
    {
        // Обновляем вывод денег в UI
        moneyText.text = "Монеты: " + currentMoney.ToString("F2");
    }

    void PreviousSkin()
    {
        currentSkinIndex--;
        if (currentSkinIndex < 0)
        {
            currentSkinIndex = skinIcons.Length - 1; // Циклическая прокрутка
        }
        UpdateSkinUI();
    }

    void NextSkin()
    {
        currentSkinIndex++;
        if (currentSkinIndex >= skinIcons.Length)
        {
            currentSkinIndex = 0; // Циклическая прокрутка
        }
        UpdateSkinUI();
    }

    void OnBuyOrSelectButtonClick()
    {
        if (purchasedSkins[currentSkinIndex])
        {
            // Если скин куплен, просто выбираем его
            PlayerPrefs.SetInt("SelectedSkinIndex", currentSkinIndex);
            PlayerPrefs.Save();
            UpdateSkinUI(); // Обновляем UI, чтобы отобразить "Выбрано"
        }
        else
        {
            // Если скин не куплен, проверяем достаточно ли денег
            currentMoney = PlayerPrefs.GetFloat("TotalMoney", 0f); // Обновляем количество денег
            if (currentMoney >= skinPrices[currentSkinIndex])
            {
                // Покупаем скин
                currentMoney -= skinPrices[currentSkinIndex];
                PlayerPrefs.SetFloat("TotalMoney", currentMoney); // Обновляем деньги в PlayerPrefs

                purchasedSkins[currentSkinIndex] = true;
                PlayerPrefs.SetInt("Skin_" + currentSkinIndex, 1); // Сохраняем, что скин куплен
                PlayerPrefs.SetInt("SelectedSkinIndex", currentSkinIndex); // Сохраняем выбор
                PlayerPrefs.Save();

                // Запуск анимации радости при успешной покупке
                StartCoroutine(JumpAnimation(buyButton));

                UpdateMoneyUI(); // Обновляем количество денег в UI
                UpdateSkinUI(); // Обновляем UI после покупки
            }
            else
            {
                // Запуск анимации дрожания при недостатке средств
                StartCoroutine(ShakeAnimation(buyButton));
            }
        }
    }

    IEnumerator JumpAnimation(Button button)
    {
        // Простая анимация подпрыгивания
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Vector3 originalScale = rectTransform.localScale;
        Vector3 targetScale = originalScale * 1.2f;

        // Увеличение масштаба
        for (float t = 0f; t < 0.2f; t += Time.deltaTime)
        {
            rectTransform.localScale = Vector3.Lerp(originalScale, targetScale, t / 0.2f);
            yield return null;
        }

        // Возвращение к оригинальному масштабу
        for (float t = 0f; t < 0.2f; t += Time.deltaTime)
        {
            rectTransform.localScale = Vector3.Lerp(targetScale, originalScale, t / 0.2f);
            yield return null;
        }
    }

    IEnumerator ShakeAnimation(Button button)
    {
        // Простая анимация дрожания
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Vector3 originalPosition = rectTransform.localPosition;

        float duration = 0.3f;
        float magnitude = 10f;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            rectTransform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y, originalPosition.z);
            yield return null;
        }

        rectTransform.localPosition = originalPosition; // Возвращаем на исходную позицию
    }

    void ResetPurchasedSkins()
    {
        // Сбрасываем купленные скины
        for (int i = 0; i < skinIcons.Length; i++)
        {
            PlayerPrefs.SetInt("Skin_" + i, 0); // 0 = не куплено
        }
        PlayerPrefs.SetInt("SelectedSkinIndex", 0); // Сбрасываем выбор скина
        PlayerPrefs.Save();

        // Обновляем данные и UI
        LoadSkinData();
        UpdateSkinUI();

        Debug.Log("Все скины сброшены");
    }
}
