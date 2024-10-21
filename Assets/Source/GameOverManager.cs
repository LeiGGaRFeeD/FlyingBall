using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas; // ������ �� Canvas � ���������� � ���������
    public Text sessionMoneyText; // UI ����� ��� ����������� ����� �� ������
    public Text totalMoneyText; // UI ����� ��� ����������� ������ ���������� �����
    public MoneyManager moneyManager; // ������ �� MoneyManager ��� ������� � �������
    private bool isGameOver = false; // ����, �����������, ��������� �� ����

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
        gameOverCanvas.SetActive(true); // �������� Canvas � ����������

        // ��������� ����� �� Canvas
        sessionMoneyText.text = "������ �� ������: " + moneyManager.GetSessionMoney();
        totalMoneyText.text = "����� ����������: " + moneyManager.GetTotalMoney();

        // ��������� ��������, ���� �����
        StartCoroutine(AnimateMoneyDisplay());
    }

    private IEnumerator AnimateMoneyDisplay()
    {
        // �������� ���������� ����� �� ������ � ���������� ������ ����������
        int sessionMoney = moneyManager.GetSessionMoney();
        int totalMoney = moneyManager.GetTotalMoney();

        while (sessionMoney > 0)
        {
            sessionMoney--; // ��������� ������ �� ������
            totalMoney++; // ����������� ����� ����������
            sessionMoneyText.text = "������ �� ������: " + sessionMoney;
            totalMoneyText.text = "����� ����������: " + totalMoney;

            yield return new WaitForSeconds(0.1f); // �������� ��� ��������
        }

        // ��������� ��������� ��������
        sessionMoneyText.text = "������ �� ������: 0";
        totalMoneyText.text = "����� ����������: " + totalMoney;
    }

    public void RestartGame()
    {
        moneyManager.ResetSessionMoney(); // �������� ������ �� ������
        // ����� ����� �������� ������ ��� ������������ ������ ��� ��������� ������� ������
        // ��������:
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueGame()
    {
        // ������ ��� ����������� ���� � �������� ����������
        // ����� ����� ����������� ������ � ���������� ����� � �������� ����
        isGameOver = false;
        gameOverCanvas.SetActive(false);
        // ����������� ������ � ���������� �����, ���� ����������
    }
}
