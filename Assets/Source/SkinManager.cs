using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public Sprite[] skinIcons; // Массив иконок скинов
    public int[] skinPrices; // Массив цен скинов

    public Image skinIcon; // UI-элемент для отображения иконки скина
    public Text skinStatusText; // Текст для вывода статуса скина (цена или "Открыто")
    public Button buyButton; // Кнопка "Купить" или "Выбрать"
    public Button previousButton, nextButton; // Кнопки переключения скинов

    private int currentSkinIndex; // Индекс текущего скина
    private bool[] purchasedSkins; // Массив для отслеживания купленных скинов
    private float currentMoney; // Текущие деньги игрока

    void Start()
    {
        LoadSkinData(); // Загружаем данные
        UpdateSkinUI(); // Обновляем интерфейс скинов

        // Добавляем обработчики событий для кнопок
        previousButton.onClick.AddListener(PreviousSkin);
        nextButton.onClick.AddListener(NextSkin);
        buyButton.onClick.AddListener(OnBuyOrSelectButtonClick);
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
            skinStatusText.text = "Открыто";
            buyButton.GetComponentInChildren<Text>().text = "Выбрать";
        }
        else
        {
            skinStatusText.text = "Цена: " + skinPrices[currentSkinIndex] + " монет";
            buyButton.GetComponentInChildren<Text>().text = "Купить";
        }
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
            Debug.Log("Выбран скин: " + currentSkinIndex);
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

                Debug.Log("Скин куплен: " + currentSkinIndex);
                UpdateSkinUI(); // Обновляем UI после покупки
            }
            else
            {
                Debug.Log("Недостаточно денег для покупки скина");
            }
        }
    }
}
