using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public GameObject startCanvas; // ��������� Canvas
    public GameObject gameCanvas; // ������� Canvas (���� ����)
    public Button startButton; // ������ "Start"
    public MoneyManager moneyManager; // ������ �� MoneyManager
    public GameObject player; // �����
    public ObjectSpawner objectSpawner; // ������ �� ObjectSpawner

    private void Start()
    {
        // ������������� �� ������� ������
        startButton.onClick.AddListener(StartGame);

        // ��������� ������� �������� �� ������ ����
        gameCanvas.SetActive(false);
        player.SetActive(false);
        objectSpawner.enabled = false;
    }

    // ����� ��� ������ ����
    private void StartGame()
    {
        // ��������� ��������� Canvas
        startCanvas.SetActive(false);

        // �������� ������� Canvas (���� �����)
        gameCanvas.SetActive(true);

        // �������� ������ � ����� ��������
        player.SetActive(true);
        objectSpawner.enabled = true;

        // ��������� MoneyManager, ���� ����� ������ ������ �����
        if (moneyManager != null)
        {
            moneyManager.enabled = true; // ���������� MoneyManager, ���� �� �� �������
        }
    }
}
