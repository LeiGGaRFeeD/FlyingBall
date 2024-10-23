using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinChanger : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer; // Компонент SpriteRenderer игрока для смены внешнего вида
    public Sprite[] playerSkins; // Массив спрайтов для всех возможных скинов игрока
    public BoxCollider2D playerCollider; // BoxCollider для подстройки размеров
    public Vector2[] skinScaleFactors; // Массив коэффициентов увеличения для каждого скина

    void Start()
    {
        ChangeSkin(); // Меняем скин при старте сцены
    }

    // Метод для смены скина
    public void ChangeSkin()
    {
        // Получаем выбранный скин из PlayerPrefs
        int selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex", 0);

        // Проверяем, есть ли скин с таким индексом в массиве
        if (selectedSkinIndex >= 0 && selectedSkinIndex < playerSkins.Length)
        {
            // Меняем спрайт игрока на выбранный скин
            playerSpriteRenderer.sprite = playerSkins[selectedSkinIndex];

            // Настраиваем размер объекта и коллайдера
            AdjustColliderAndScale(selectedSkinIndex);
        }
        else
        {
            Debug.LogWarning("Неверный индекс скина! Убедитесь, что выбранный скин доступен в массиве.");
        }
    }

    // Метод для подгонки коллайдера и масштаба объекта
    private void AdjustColliderAndScale(int skinIndex)
    {
        if (playerSpriteRenderer.sprite == null || playerCollider == null)
        {
            Debug.LogWarning("Отсутствует спрайт или коллайдер для подгонки размера.");
            return;
        }

        // Получаем размеры спрайта
        Vector2 spriteSize = playerSpriteRenderer.sprite.bounds.size;

        // Применяем масштабирование объекта в зависимости от коэффициента для текущего скина
        if (skinIndex >= 0 && skinIndex < skinScaleFactors.Length)
        {
            // Применяем масштаб на объект
            Vector3 newScale = new Vector3(skinScaleFactors[skinIndex].x, skinScaleFactors[skinIndex].y, 1);
            transform.localScale = newScale;
        }

        // Настраиваем коллайдер в соответствии с размерами спрайта и примененным масштабом
        playerCollider.size = spriteSize; // Устанавливаем размер коллайдера
        playerCollider.offset = playerSpriteRenderer.sprite.bounds.center; // Смещаем коллайдер в зависимости от центра спрайта
    }
}
