using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetrcoCamera : MonoBehaviour
{
    // ��������� ���������
    public float shakeAmount = 0.01f; // �������� ���������
    public float shakeSpeed = 10.0f; // �������� ���������

    // ��������� ������� ������
    private Vector3 initialPosition;

    void Start()
    {
        // �������� ��������� ������� ������
        initialPosition = transform.position;
    }

    void Update()
    {
        // ���������� �������� �������� ��������� ��� ������� �����
        float offsetX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0.0f) * 2.0f - 1.0f;
        float offsetY = Mathf.PerlinNoise(0.0f, Time.time * shakeSpeed) * 2.0f - 1.0f;

        // ����������� ��������� �� ������� ������
        Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0.0f) * shakeAmount;
        transform.position = initialPosition + shakeOffset;
    }
}
