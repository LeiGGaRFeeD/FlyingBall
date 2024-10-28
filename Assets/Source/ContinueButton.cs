using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public GameObject buttonToDestroy;       // ������, ������� ����� ����������
    public Text timerText;                   // UI ����� ��� ����������� �������
    public Slider timerSlider;               // ������� ��� ����������� ��������� �������
    public float destructionDelay = 5f;      // ����� �� ����������� ������ ����� ������������

    private bool isCountingDown = false;     // �����������, ����� �� ������
    private float timer;                     // ������� ����� �������

    void Start()
    {
        timer = destructionDelay;
        timerText.text = "";                 // ������� ����� ������� ��� ������
        timerSlider.value = 1f;              // ������� �������� ��������� � ������
        timerSlider.gameObject.SetActive(false); // �������� ������� �� ������������
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCountingDown)                 // ��������� ������ ������ ��� ������ ������������
        {
            isCountingDown = true;
            timerSlider.gameObject.SetActive(true); // ���������� ������� ��� ������������
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
                DestroyButton();             // ���������� ������, ����� ������ ��������� ����
            }
        }
    }

    private void DestroyButton()
    {
        if (buttonToDestroy != null)
        {
            Destroy(buttonToDestroy);        // ���������� ������
        }
        timerText.text = "";                 // ������� �����
        timerSlider.gameObject.SetActive(false); // �������� �������
    }
    public void DestButton()
    {
        Destroy(buttonToDestroy);
        Destroy(timerText);
        Destroy(timerSlider);
    }
}