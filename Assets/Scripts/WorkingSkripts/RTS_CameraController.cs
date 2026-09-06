using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTS_CameraController : MonoBehaviour
{
    public bool invertMouse = false;
    public float moveSpeed = 10f; 
    public float zoomSpeed = 5f; 
    public float rotationSpeed = 5f; 

    public float maxDistanceX = 10f; 
    public float maxDistanceZ = 10f; 
    public float minY = 5f; 
    public float maxY = 20f; 

    public Transform rotatePoint; 

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);
        transform.Translate(moveDirection, Space.Self);

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -maxDistanceX, maxDistanceX);
        position.z = Mathf.Clamp(position.z, -maxDistanceZ, maxDistanceZ);
        transform.position = position;

        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        transform.Translate(0, zoom, 0, Space.World);

        float yPos = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);

        if (Input.GetMouseButton(2))
        {
            float rotateX = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.RotateAround(rotatePoint.position, Vector3.up, rotateX);
        }
        MoveMapWithMouse(); // добавляем вызов метода перемещения карты с мышью
    }

    void OnDrawGizmos()
    {
        // Отрисовываем квадрат
        Vector3 center = transform.position;
        center.y = minY;
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(center, new Vector3(maxDistanceX * 2, 0, maxDistanceZ * 2));
    }
    void MoveMapWithMouse()
{
    if (Input.GetMouseButton(1)) // Если зажата левая кнопка мыши
    {
        float moveX = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime * (invertMouse ? -1f : 1f);
        float moveZ = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime * (invertMouse ? -1f : 1f);
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);
        transform.Translate(moveDirection, Space.Self);
    }
}

}

