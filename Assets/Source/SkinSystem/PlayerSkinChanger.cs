using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinChanger : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer; // ��������� SpriteRenderer ������ ��� ����� �������� ����
    public Sprite[] playerSkins; // ������ �������� ��� ���� ��������� ������ ������
    public BoxCollider2D playerCollider; // BoxCollider ��� ���������� ��������
    public Vector2[] skinScaleFactors; // ������ ������������� ���������� ��� ������� �����

    void Start()
    {
        ChangeSkin(); // ������ ���� ��� ������ �����
    }

    // ����� ��� ����� �����
    public void ChangeSkin()
    {
        // �������� ��������� ���� �� PlayerPrefs
        int selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex", 0);

        // ���������, ���� �� ���� � ����� �������� � �������
        if (selectedSkinIndex >= 0 && selectedSkinIndex < playerSkins.Length)
        {
            // ������ ������ ������ �� ��������� ����
            playerSpriteRenderer.sprite = playerSkins[selectedSkinIndex];

            // ����������� ������ ������� � ����������
            AdjustColliderAndScale(selectedSkinIndex);
        }
        else
        {
            Debug.LogWarning("�������� ������ �����! ���������, ��� ��������� ���� �������� � �������.");
        }
    }

    // ����� ��� �������� ���������� � �������� �������
    private void AdjustColliderAndScale(int skinIndex)
    {
        if (playerSpriteRenderer.sprite == null || playerCollider == null)
        {
            Debug.LogWarning("����������� ������ ��� ��������� ��� �������� �������.");
            return;
        }

        // �������� ������� �������
        Vector2 spriteSize = playerSpriteRenderer.sprite.bounds.size;

        // ��������� ��������������� ������� � ����������� �� ������������ ��� �������� �����
        if (skinIndex >= 0 && skinIndex < skinScaleFactors.Length)
        {
            // ��������� ������� �� ������
            Vector3 newScale = new Vector3(skinScaleFactors[skinIndex].x, skinScaleFactors[skinIndex].y, 1);
            transform.localScale = newScale;
        }

        // ����������� ��������� � ������������ � ��������� ������� � ����������� ���������
        playerCollider.size = spriteSize; // ������������� ������ ����������
        playerCollider.offset = playerSpriteRenderer.sprite.bounds.center; // ������� ��������� � ����������� �� ������ �������
    }
}
