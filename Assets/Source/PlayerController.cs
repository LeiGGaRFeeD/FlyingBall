using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gameOverCanvas; // ������ �� Canvas ���������
    public MoneyManager moneyManager; // ������ �� MoneyManager

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
        // �������� ������ ���������
        gameOverCanvas.SetActive(true); // ���������� Canvas ���������
        moneyManager.AddToTotalMoney(); // ��������� ������ �� ������ � ������ ����������
        // �� ������ �������� �������� ��� ������ ������� �����
    }
}
