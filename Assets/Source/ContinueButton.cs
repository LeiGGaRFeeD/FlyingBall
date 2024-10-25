using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CrazyGames; // �� �������� �������� CrazyGames SDK

public class ContinueButton : MonoBehaviour
{
    public Button continueButton; // ������ �� ������ "����������"
    public float activeDuration = 5f; // �����, � ������� �������� ������ ����� ��������
    private float timer;
    private bool isAdShowing = false;

    void Start()
    {
        continueButton.gameObject.SetActive(false); // ��������� ������ ��� ������ ����
        timer = activeDuration;
    }

    void Update()
    {
        if (continueButton.gameObject.activeSelf && !isAdShowing)
        {
            // ��������� ������
            timer -= Time.deltaTime;

            // ���������, ���� ������ �����
            if (timer <= 0f)
            {
                DeactivateButton(); // ������������ ������ ����� ��������� �������
            }
        }
    }

    public void ActivateButton()
    {
        continueButton.gameObject.SetActive(true); // �������� ������
        timer = activeDuration; // ���������� ������
    }

   /* public void OnContinueButtonClick() //Need to fix 
    {
        if (!isAdShowing)
        {
            // ������ ���� �� �����
            Time.timeScale = 0f;

            // ���������� ������� � �������������� CrazyGames SDK
            isAdShowing = true;
            CrazyAds.Instance.beginAdBreak(() => {
                // Callback ����� ���������� �������
                ResumeGame();
            });
        }
    }*/

    private void ResumeGame()
    {
        // ���������� ����
        Time.timeScale = 1f;
        isAdShowing = false;
        DeactivateButton(); // ������������ ������ ����� ��������� �������
    }

    private void DeactivateButton()
    {
        continueButton.gameObject.SetActive(false); // ��������� ������
    }
}
