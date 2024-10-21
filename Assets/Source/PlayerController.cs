using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gameOverCanvas; // Ссылка на Canvas проигрыша
    public MoneyManager moneyManager; // Ссылка на MoneyManager

    /*private void OnColliderEnter2D(Collision2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Wall"))
        {
            TriggerGameOver();
        }
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall"))
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        // Вызываем логику проигрыша
        gameOverCanvas.SetActive(true); // Активируем Canvas проигрыша
        moneyManager.AddToTotalMoney(); // Добавляем деньги за сессию к общему количеству
        // Вы можете добавить анимацию или другие эффекты здесь
    }
}
