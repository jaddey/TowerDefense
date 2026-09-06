using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class BuildingManager : MonoBehaviour
{
    public BuildingObject[] buildingPrefabs; // Массив всех доступных префабов построек

    private GameObject currentBuildingPrefab; // Текущий выбранный префаб постройки
    private GameObject currentBuildingPrefabHologram; // Текущий выбранный префаб голограммы
    private GameObject currentBuilding; // Экземпляр текущей постройки на сцене
    public bool isBuilding; // Флаг, показывающий, находимся ли мы в режиме строительства
    public BuildPlatform buildPlatformEx;
    private bool openWindowParams = false;
    public GameObject WindowParams;
    public bool isBuildable = true;
    public bool isOccupied = true;
    public TMP_Text statusText;
    public string buildableText = "Building is buildable";
    public string unbuildableText = "Building is not buildable";
    public string occupiedText = "Building is occupied";
    public string unoccupiedText = "Building is unoccupied";
    public event System.Action<bool> BuildingStateChanged;

    private void Start()
    {
        // Выбираем первый префаб из списка по умолчанию
        if (buildingPrefabs.Length > 0)
        {
            currentBuildingPrefab = buildingPrefabs[0].buildingPrefab;
            currentBuildingPrefabHologram = buildingPrefabs[0].hologramPrefab;
            currentBuilding = Instantiate(currentBuildingPrefabHologram, Vector3.zero, Quaternion.identity);
            currentBuilding.SetActive(false);
            BuildingStateChanged?.Invoke(isBuilding);
        }
    }

    private void Update()
    {
        if (isBuilding)
        {
            // Создаем луч от камеры к месту, куда смотрит курсор
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Если луч сталкивается с чем-то, перемещаем префаб постройки в эту точку
            if (Physics.Raycast(ray, out hit))
            {
                currentBuilding.transform.position = hit.point;

                // Если мы навели на объект с коллайдером и тегом "place", вызываем метод Build на компоненте BuildPlatform
                if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("place"))
                {
                    BuildPlatform buildPlatform = hit.collider.GetComponent<BuildPlatform>();
                    if (buildPlatform != null)
                    {
                        buildPlatform.buildableObject = currentBuildingPrefab;
                        buildPlatform.Build();
                        isBuilding = false;
                        BuildingStateChanged?.Invoke(isBuilding);
                        Destroy(currentBuilding);
                        UpdateStatusText();
                    }
                }
                else
                {
                    
                }
            }

            // Если правая кнопка мыши нажата, выходим из режима строительства
            if (Input.GetMouseButtonDown(1))
            {
                isBuilding = false;
                BuildingStateChanged?.Invoke(isBuilding);
                Destroy(currentBuilding);
            }
        }
        else
        {
            // Если мы не в режиме строительства и щелкнули правой кнопкой мыши по объекту с тегом "place", выделяем объект
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(buildPlatformEx != null)
                        {
                            buildPlatformEx.SetAllChildLayers("Default");
                        }

                if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("place"))
                {
                    BuildPlatform buildPlatform = hit.collider.GetComponent<BuildPlatform>();
                    //buildPlatform.UpdateChildObjects();
                    buildPlatform.SetAllChildLayers("Outline");
                    if (buildPlatform != null)
                    {
                        buildPlatformEx = buildPlatform;
                        openWindowParams = true;
                        WindowParams.SetActive(true);
                        UpdateStatusText();
                    }
                }
                else
                {
                openWindowParams = false;
                WindowParams.SetActive(false);
                if(buildPlatformEx != null)
                {
                    buildPlatformEx.SetAllChildLayers("Default");
                }
                buildPlatformEx = null;
                }
            }
        }
    }

    // Метод для выбора префаба постройки
    public void SelectBuilding(int buildingIndex)
    {
        if (buildingIndex >= 0 && buildingIndex < buildingPrefabs.Length)
        {
            currentBuildingPrefabHologram = buildingPrefabs[buildingIndex].hologramPrefab;
            currentBuildingPrefab = buildingPrefabs[buildingIndex].buildingPrefab;
            isBuilding = true;
            BuildingStateChanged?.Invoke(isBuilding);

            if (currentBuilding != null)
            {
                Destroy(currentBuilding);
            }

            currentBuilding = Instantiate(currentBuildingPrefabHologram, Vector3.zero, Quaternion.identity);
            currentBuilding.SetActive(true);
        }
    }
    public void RepairBuilding()
    {
        if (buildPlatformEx != null)
        {
            buildPlatformEx.Repair();
            UpdateStatusText();
        }
    }

    public void DestroyBuilding()
    {
        if (buildPlatformEx != null)
        {
            buildPlatformEx.DestroyBuilding();
            UpdateStatusText();
        }
    }
    private void UpdateStatusText()
    {
        if(buildPlatformEx != null)
        {
            isBuildable = buildPlatformEx.isBuildable;
            isOccupied = buildPlatformEx.isOccupied;
        if (isBuildable)
        {
            if (isOccupied)
            {
                statusText.text = occupiedText;
            }
            else
            {
                statusText.text = unoccupiedText;
            }
        }
        else
        {
            if (isOccupied)
            {
                statusText.text = unbuildableText + ", " + occupiedText;
            }
            else
            {
                statusText.text = unbuildableText;
            }
        }
        }
    }
}





