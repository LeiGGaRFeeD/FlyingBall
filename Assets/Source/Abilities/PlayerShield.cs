using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShield : MonoBehaviour
{
    public Button shieldButton;                // ������ ����
    public Text buttonText;                    // ����� �� ������ ��� ����������� �������
    public float shieldDuration = 3f;          // ����� �������� ���� � ��������
    public float cooldownDuration = 5f;        // ����� ����������� � ��������
    public int shieldCost = 50;                // ��������� ��������� ����
    public Color activeColor = Color.green;    // ���� ������ ��� �������� ����
    public Color cooldownColor = Color.gray;   // ���� ������ ��� �����������
    public MoneyManager moneyManager;          // ������ �� MoneyManager

    private Collider2D playerCollider;         // ��������� ������
    private bool isShieldActive = false;       // ���� ��������� ����
    private bool isCooldown = false;           // ���� �����������
    private float shieldTimer;                 // ������ ��� ������� �������� ����
    private float cooldownTimer;               // ������ ��� �����������

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        shieldButton.onClick.AddListener(TryActivateShield);
        UpdateButtonState();                   // ��������� ���������� ������
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))  // �������� ������� ������� "1"
        {
            TryActivateShield();
        }

        if (isShieldActive)
        {
            UpdateShieldTimer();               // ��������� ������ ����
        }
        else if (isCooldown)
        {
            UpdateCooldownTimer();             // ��������� ������ �����������
        }
    }

    private void TryActivateShield()
    {
        // ���������, ������� �� ����� ��� ������� ����
        if (moneyManager != null && moneyManager.GetTotalMoney() >= shieldCost && !isShieldActive && !isCooldown)
        {
            moneyManager.SpendMoney(shieldCost); // ��������� ��������� ���� �� MoneyManager
            StartCoroutine(ActivateShield());
        }
        else if (moneyManager != null && moneyManager.GetTotalMoney() < shieldCost)
        {
            StartCoroutine(ShakeButton());     // ������������ ������ ��� ���������� �������
        }
    }

    private IEnumerator ActivateShield()
    {
        isShieldActive = true;
        playerCollider.enabled = false;        // ��������� ��������� ������
        shieldButton.interactable = false;     // ������ ������ ����������
        shieldButton.image.color = activeColor;
        shieldTimer = shieldDuration;          // ������������� ������ ����

        while (shieldTimer > 0)
        {
            buttonText.text = $"Active: {shieldTimer.ToString("F1")}s"; // ��������� ����� ������� ��������
            shieldTimer -= Time.deltaTime;
            yield return null;
        }

        playerCollider.enabled = true;         // �������� ��������� ������
        isShieldActive = false;
        StartCoroutine(StartCooldown());       // ��������� �����������
    }

    private IEnumerator StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = cooldownDuration;
        shieldButton.image.color = cooldownColor;

        while (cooldownTimer > 0)
        {
            buttonText.text = $"Cooldown: {cooldownTimer.ToString("F1")}s"; // ��������� ����� ������� �����������
            cooldownTimer -= Time.deltaTime;
            yield return null;
        }

        isCooldown = false;
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        shieldButton.interactable = !isCooldown && !isShieldActive;
        buttonText.text = "Shield";
        shieldButton.image.color = Color.white;
    }

    private void UpdateShieldTimer()
    {
        // ��������� � ���������� ���������� ����� �������� ����
        shieldTimer -= Time.deltaTime;
        buttonText.text = $"Active: {shieldTimer.ToString("F1")}s";
    }

    private void UpdateCooldownTimer()
    {
        // ��������� � ���������� ���������� ����� �����������
        cooldownTimer -= Time.deltaTime;
        buttonText.text = $"Cooldown: {cooldownTimer.ToString("F1")}s";
    }

    private IEnumerator ShakeButton()
    {
        Vector3 originalPosition = shieldButton.transform.position;
        float shakeIntensity = 5f;
        float shakeDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeIntensity;
            float offsetY = Random.Range(-1f, 1f) * shakeIntensity;
            shieldButton.transform.position = originalPosition + new Vector3(offsetX, offsetY, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        shieldButton.transform.position = originalPosition;
    }
}
