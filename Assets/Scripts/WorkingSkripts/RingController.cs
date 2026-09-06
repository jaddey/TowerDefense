using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    public GameObject objectToControl;
    public BuildPlatform buildPlatform;
    private BuildingManager buildingManager;

    private void Start()
{
    buildingManager = FindObjectOfType<BuildingManager>();

    // Подписываемся на событие изменения состояния режима строительства
    buildingManager.BuildingStateChanged += OnBuildingStateChanged;
}

private void OnDestroy()
{
    // Отписываемся от события при уничтожении объекта
    buildingManager.BuildingStateChanged -= OnBuildingStateChanged;
}

private void OnBuildingStateChanged(bool isBuilding)
{
    if (isBuilding && buildPlatform.isBuildable && buildPlatform.isOccupied == false)
    {
        objectToControl.SetActive(true);
    }
    else
    {
        objectToControl.SetActive(false);
    }
}
}

