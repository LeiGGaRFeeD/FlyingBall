using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public int moneyPerSecond = 1; // ���������� �����, ����������� � �������
    public Text moneyText; // UI ����� ��� ����������� ����� �� ������
    public Text totalMoneyText; // UI ����� ��� ����������� ������ ���������� �����

    private int sessionMoney = 0; // ������ �� ������
    private int totalMoney = 0; // ����� ���������� �����

    void Start()
    {
        UpdateTotalMoneyText(); // ��������� ����� ���������� ����� � ������
        StartCoroutine(GenerateMoney()); // ��������� �������� ��� ��������� �����
    }

    IEnumerator GenerateMoney()
    {
        while (true)
        {
            // ����������� ������ �� ������
            sessionMoney += moneyPerSecond;
            StartCoroutine(UpdateMoneyTextSmoothly(sessionMoney)); // ��������� ����� � ������� ���������
            yield return new WaitForSeconds(1f); // ���� 1 �������
        }
    }

    public void UpdateTotalMoneyText()
    {
        totalMoneyText.text = "����� ����������: " + totalMoney.ToString() + "$";
    }

    private IEnumerator UpdateMoneyTextSmoothly(int targetAmount)
    {
        int startAmount = 0;

        // ���������� ������ �������� ���������� �����
        if (!int.TryParse(moneyText.text.Replace("������: ", "").Replace("$", ""), out startAmount))
        {
            startAmount = 0; // ���� ������� �� ������, �������� � 0
        }

        float duration = 1f; // ����������������� �������� (� ��������)
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            int currentAmount = (int)Mathf.Lerp(startAmount, targetAmount, elapsed / duration);
            moneyText.text = "������: " + currentAmount.ToString() + "$";
            yield return null; // ���� ���������� �����
        }

        // ��������, ��� �������� �������� ��������� ������������
        moneyText.text = "������: " + targetAmount.ToString() + "$";
    }

    public int GetSessionMoney()
    {
        return sessionMoney; // ���������� ������ �� ������
    }

    public int GetTotalMoney()
    {
        return totalMoney; // ���������� ����� ���������� �����
    }

    public void AddToTotalMoney()
    {
        totalMoney += sessionMoney; // ��������� ������ �� ������ � ������ ����������
        UpdateTotalMoneyText(); // ��������� ����������� ������ ����������
    }

    public void ResetSessionMoney()
    {
        sessionMoney = 0; // ���������� ������ �� ������
        UpdateMoneyTextSmoothly(sessionMoney); // ��������� ����� � ������� ���������
    }
}
