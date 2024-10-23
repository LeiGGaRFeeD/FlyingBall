using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public Sprite[] skinIcons; // ������ ������ ������
    public int[] skinPrices; // ������ ��� ������

    public Image skinIcon; // UI-������� ��� ����������� ������ �����
    public Text skinStatusText; // ����� ��� ������ ������� ����� (����, "�������" ��� "�������")
    public Text moneyText; // ����� ��� ������ �������� ���������� �����
    public Button buyButton; // ������ "������" ��� "�������"
    public Button previousButton, nextButton; // ������ ������������ ������

    public bool debugMode; // �������� ����� Debug ��� ������ ������
    private int currentSkinIndex; // ������ �������� �����
    private bool[] purchasedSkins; // ������ ��� ������������ ��������� ������
    private float currentMoney; // ������� ������ ������
    private Animator buyButtonAnimator; // �������� ��� ������ �������

    void Start()
    {
        LoadSkinData(); // ��������� ������
        UpdateSkinUI(); // ��������� ��������� ������

        // ��������� ����������� ������� ��� ������
        previousButton.onClick.AddListener(PreviousSkin);
        nextButton.onClick.AddListener(NextSkin);
        buyButton.onClick.AddListener(OnBuyOrSelectButtonClick);

        buyButtonAnimator = buyButton.GetComponent<Animator>(); // �������� ��������� ���������

        UpdateMoneyUI(); // ��������� ����� �����
    }

    void Update()
    {
        // ���� ������� ����� Debug � ������ ������� Y
        if (debugMode && Input.GetKeyDown(KeyCode.Y))
        {
            ResetPurchasedSkins();
        }
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
            // ���� ��������� ���� ������� � ����� "�������"
            if (PlayerPrefs.GetInt("SelectedSkinIndex") == currentSkinIndex)
            {
                skinStatusText.text = "�������";
                buyButton.GetComponentInChildren<Text>().text = "�������";
            }
            else
            {
                skinStatusText.text = "�������";
                buyButton.GetComponentInChildren<Text>().text = "�������";
            }
        }
        else
        {
            skinStatusText.text = "����: " + skinPrices[currentSkinIndex] + " �����";
            buyButton.GetComponentInChildren<Text>().text = "������";
        }
    }

    void UpdateMoneyUI()
    {
        // ��������� ����� ����� � UI
        moneyText.text = "������: " + currentMoney.ToString("F2");
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
            UpdateSkinUI(); // ��������� UI, ����� ���������� "�������"
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

                // ������ �������� ������� ��� �������� �������
                StartCoroutine(JumpAnimation(buyButton));

                UpdateMoneyUI(); // ��������� ���������� ����� � UI
                UpdateSkinUI(); // ��������� UI ����� �������
            }
            else
            {
                // ������ �������� �������� ��� ���������� �������
                StartCoroutine(ShakeAnimation(buyButton));
            }
        }
    }

    IEnumerator JumpAnimation(Button button)
    {
        // ������� �������� �������������
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Vector3 originalScale = rectTransform.localScale;
        Vector3 targetScale = originalScale * 1.2f;

        // ���������� ��������
        for (float t = 0f; t < 0.2f; t += Time.deltaTime)
        {
            rectTransform.localScale = Vector3.Lerp(originalScale, targetScale, t / 0.2f);
            yield return null;
        }

        // ����������� � ������������� ��������
        for (float t = 0f; t < 0.2f; t += Time.deltaTime)
        {
            rectTransform.localScale = Vector3.Lerp(targetScale, originalScale, t / 0.2f);
            yield return null;
        }
    }

    IEnumerator ShakeAnimation(Button button)
    {
        // ������� �������� ��������
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Vector3 originalPosition = rectTransform.localPosition;

        float duration = 0.3f;
        float magnitude = 10f;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            rectTransform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y, originalPosition.z);
            yield return null;
        }

        rectTransform.localPosition = originalPosition; // ���������� �� �������� �������
    }

    void ResetPurchasedSkins()
    {
        // ���������� ��������� �����
        for (int i = 0; i < skinIcons.Length; i++)
        {
            PlayerPrefs.SetInt("Skin_" + i, 0); // 0 = �� �������
        }
        PlayerPrefs.SetInt("SelectedSkinIndex", 0); // ���������� ����� �����
        PlayerPrefs.Save();

        // ��������� ������ � UI
        LoadSkinData();
        UpdateSkinUI();

        Debug.Log("��� ����� ��������");
    }
}
