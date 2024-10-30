using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public GameObject buttonToHide;          // ������, ������� ����� �����������
    public Text timerText;                   // UI ����� ��� ����������� �������
    public Slider timerSlider;               // ������� ��� ����������� ��������� �������
    public float destructionDelay = 5f;      // ����� �� ����������� ������ �� ����� ����� ������������
    public Vector3 offScreenPosition = new Vector3(-1000, -1000, 0); // ������� �� ��������� ������

    private bool isCountingDown = false;     // �����������, ����� �� ������
    private bool hasAppearedOnce = false;    // �����������, ���� �� ������ �������� �����
    private float timer;                     // ������� ����� �������

    void Start()
    {
        timer = destructionDelay;
      //  MoveOffScreen();                     // ���������� �������� �� ������� ������ ��� ������
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasAppearedOnce)                // ���������, ���� �� ������ �������� �����
        {
            isCountingDown = true;
            hasAppearedOnce = true;          // ��������, ��� ������ ���� ��������
         //   MoveOnScreen();                  // ���������� �������� �� �����
        }
    }

    void Update()
    {
        if (isCountingDown)
        {
            timer -= Time.deltaTime;         // ��������� ������
            timerText.text = timer.ToString("F1"); // ��������� ����� ������� � ����� ������ ����� �������
            timerSlider.value = timer / destructionDelay; // ��������� ������� ���������

            if (timer <= 0f)
            {
                MoveOffScreen();             // ���������� �������� �� �����, ����� ������ ��������� ����
                isCountingDown = false;      // ������������� ������
            }
        }
    }

    private void MoveOffScreen()
    {
        // ���������� ������, ������ � ������� �� ������� ������
        buttonToHide.transform.position = offScreenPosition;
        timerText.transform.position = offScreenPosition;
        timerSlider.transform.position = offScreenPosition;
    }

    public void MoveOnScreen()
    {
        // ��������� �������� � ������� ��������
        buttonToHide.transform.position = new Vector3(0, -10000, 0); // ������� ������ ������� �� ������
        timerText.transform.position = new Vector3(0, -10000, 0);  // ������� ������ ������� ��� ������
        timerSlider.transform.position = new Vector3(0, -10000, 0); // ������� ������ ������� ��� ��������
    }
}
