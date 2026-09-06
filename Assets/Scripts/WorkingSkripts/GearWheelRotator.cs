using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearWheelRotator : MonoBehaviour
{
    public Transform targetObject;
    public Axis targetAxis = Axis.Y;
    public Axis rotateAxis = Axis.Z;
    public float multiplier = 1.0f;

    private Quaternion startRotation;

    private float lastRotation;

    private void Start()
    {
        // Сохраняем начальное значение угла поворота объекта
        startRotation = transform.localRotation;

        // Сохраняем начальное значение угла поворота по заданной оси целевого объекта
        lastRotation = GetAxisRotation(targetObject.localRotation, targetAxis);
    }

    private void Update()
    {
        // Получаем текущее значение угла поворота по заданной оси целевого объекта
        float rotation = GetAxisRotation(targetObject.localRotation, targetAxis);

        // Проверяем, изменилось ли значение угла поворота
        if (Mathf.Abs(rotation - lastRotation) > Mathf.Epsilon)
        {
            // Создаем новый Quaternion на основе начальных значений углов поворота
            // и нового угла поворота по заданной оси, и применяем его к этому объекту

            // Копируем начальное значение углов поворота
            Quaternion newRotation = startRotation;

            // Получаем текущий угол поворота по заданной оси и вычисляем новый угол поворота
            float currentRotation = GetAxisRotation(startRotation, rotateAxis);
            float newRotationAngle = currentRotation + rotation * multiplier;

            // Устанавливаем новый угол поворота по заданной оси в копии начального значения углов поворота
            SetAxisRotation(ref newRotation, newRotationAngle, rotateAxis);

            // Применяем новое значение углов поворота к объекту
            transform.localRotation = newRotation;

            // Сохраняем текущее значение угла поворота как последнее
            lastRotation = rotation;
        }
    }

    private float GetAxisRotation(Quaternion rotation, Axis axis)
    {
        switch (axis)
        {
            case Axis.X:
                return rotation.eulerAngles.x;
            case Axis.Y:
                return rotation.eulerAngles.y;
            case Axis.Z:
                return rotation.eulerAngles.z;
            default:
                return 0.0f;
        }
    }

    private void SetAxisRotation(ref Quaternion rotation, float angle, Axis axis)
    {
        switch (axis)
        {
            case Axis.X:
                rotation = Quaternion.Euler(angle, rotation.eulerAngles.y, rotation.eulerAngles.z);
                break;
            case Axis.Y:
                rotation = Quaternion.Euler(rotation.eulerAngles.x, angle, rotation.eulerAngles.z);
                break;
            case Axis.Z:
                rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, angle);
                break;
        }
    }
}

public enum Axis
{
    X,
    Y,
    Z
}





