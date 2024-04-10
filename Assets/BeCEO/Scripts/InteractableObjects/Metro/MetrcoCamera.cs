using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetrcoCamera : MonoBehaviour
{
    // Параметри коливання
    public float shakeAmount = 0.01f; // Величина коливання
    public float shakeSpeed = 10.0f; // Швидкість коливання

    // Початкова позиція камери
    private Vector3 initialPosition;

    void Start()
    {
        // Зберегти початкову позицію камери
        initialPosition = transform.position;
    }

    void Update()
    {
        // Генерувати рандомну величину коливання для кожного кадру
        float offsetX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0.0f) * 2.0f - 1.0f;
        float offsetY = Mathf.PerlinNoise(0.0f, Time.time * shakeSpeed) * 2.0f - 1.0f;

        // Застосувати коливання до позиції камери
        Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0.0f) * shakeAmount;
        transform.position = initialPosition + shakeOffset;
    }
}
