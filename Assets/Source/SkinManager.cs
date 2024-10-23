using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public Sprite[] skinIcons; // ������ ������ ������
    public int[] skinPrices; // ������ ��� ������

    public Image skinIcon; // UI-������� ��� ����������� ������ �����
    public Text skinStatusText; // ����� ��� ������ ������� ����� (���� ��� "�������")
    public Button buyButton; // ������ "������" ��� "�������"
    public Button previousButton, nextButton; // ������ ������������ ������

    private int currentSkinIndex; // ������ �������� �����
    private bool[] purchasedSkins; // ������ ��� ������������ ��������� ������
    private float currentMoney; // ������� ������ ������

    void Start()
    {
        LoadSkinData(); // ��������� ������
        UpdateSkinUI(); // ��������� ��������� ������

        // ��������� ����������� ������� ��� ������
        previousButton.onClick.AddListener(PreviousSkin);
        nextButton.onClick.AddListener(NextSkin);
        buyButton.onClick.AddListener(OnBuyOrSelectButtonClick);
    }

    void LoadSkinData()
    {
        // ��������� ������ �� PlayerPrefs
        currentMoney = PlayerPrefs.GetFloat("TotalMoney", 0f);

        // ������������� ������� ��������� ������
        purchasedSkins = new bool[skinIcons.Length];
        for (int i = 0; i < skinIcons.Length; i++)
        {
            purchasedSkins[i] = PlayerPrefs.GetInt("Skin_" + i, 0) == 1; // 1 = �������, 0 = �� �������
        }

        // ��������� ��������� ���� �� PlayerPrefs
        currentSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex", 0);
    }

    void UpdateSkinUI()
    {
        // ������������� ������ �������� �����
        skinIcon.sprite = skinIcons[currentSkinIndex];

        // ���������, ������ �� ������� ����
        if (purchasedSkins[currentSkinIndex])
        {
            skinStatusText.text = "�������";
            buyButton.GetComponentInChildren<Text>().text = "�������";
        }
        else
        {
            skinStatusText.text = "����: " + skinPrices[currentSkinIndex] + " �����";
            buyButton.GetComponentInChildren<Text>().text = "������";
        }
    }

    void PreviousSkin()
    {
        currentSkinIndex--;
        if (currentSkinIndex < 0)
        {
            currentSkinIndex = skinIcons.Length - 1; // ����������� ���������
        }
        UpdateSkinUI();
    }

    void NextSkin()
    {
        currentSkinIndex++;
        if (currentSkinIndex >= skinIcons.Length)
        {
            currentSkinIndex = 0; // ����������� ���������
        }
        UpdateSkinUI();
    }

    void OnBuyOrSelectButtonClick()
    {
        if (purchasedSkins[currentSkinIndex])
        {
            // ���� ���� ������, ������ �������� ���
            PlayerPrefs.SetInt("SelectedSkinIndex", currentSkinIndex);
            PlayerPrefs.Save();
            Debug.Log("������ ����: " + currentSkinIndex);
        }
        else
        {
            // ���� ���� �� ������, ��������� ���������� �� �����
            currentMoney = PlayerPrefs.GetFloat("TotalMoney", 0f); // ��������� ���������� �����
            if (currentMoney >= skinPrices[currentSkinIndex])
            {
                // �������� ����
                currentMoney -= skinPrices[currentSkinIndex];
                PlayerPrefs.SetFloat("TotalMoney", currentMoney); // ��������� ������ � PlayerPrefs

                purchasedSkins[currentSkinIndex] = true;
                PlayerPrefs.SetInt("Skin_" + currentSkinIndex, 1); // ���������, ��� ���� ������
                PlayerPrefs.SetInt("SelectedSkinIndex", currentSkinIndex); // ��������� �����
                PlayerPrefs.Save();

                Debug.Log("���� ������: " + currentSkinIndex);
                UpdateSkinUI(); // ��������� UI ����� �������
            }
            else
            {
                Debug.Log("������������ ����� ��� ������� �����");
            }
        }
    }
}
