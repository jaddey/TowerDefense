using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlatform : MonoBehaviour
{
    [SerializeField]
    public bool isBuildable;// Флаг, показывающий, доступна ли платформа для постройки
    //public BuildPlatformData buildPlatformData;
    public GameObject buildableObject; // Префаб постройки, который можно разместить на платформе
    public Transform buildPoint; // Точка для постройки, куда будет размещена постройка
    public GameObject platform; //Префаб рабочей платформы
    public GameObject broken;   //Префаб сломанной скалы
    public bool isOccupied = false; // Флаг, показывающий, занята ли платформа постройкой
    private GameObject builtObject; // Ссылка на объект, построенный на данной платформе
    private GameObject[] childObjects;
    

private void Awake()
    {
        if (isBuildable)
        {
            platform.SetActive(true);
            broken.SetActive(false);
        }
        else
        {
            platform.SetActive(false);
            broken.SetActive(true);
        }
        UpdateChildObjects();
    }

public void Build()
{
    if (isBuildable && !isOccupied)
    {
        if (buildableObject != null)
        {
            // Размещаем постройку на точке для постройки или на платформе
            Vector3 buildPosition = (buildPoint != null) ? buildPoint.position : transform.position;
            Quaternion buildRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // Получаем поворот объекта со скриптом
            builtObject = Instantiate(buildableObject, buildPosition, buildRotation); // Инициируем префаб с тем же поворотом по оси Y

            // Делаем размещенный объект дочерним для buildPoint
            builtObject.transform.SetParent(buildPoint);

            // Делаем платформу недоступной и занятой
            isOccupied = true;
            UpdateChildObjects();
        }
    }
    else if (!isOccupied)
    {
        return;
    }
}

public void Repair()
{
    isBuildable = true;
    platform.SetActive(true);
    broken.SetActive(false);
}

public void DestroyBuilding()
{
    if (builtObject != null)
    {
        // Удаляем объект, построенный на данной платформе
        Destroy(builtObject);
        builtObject = null;
        
    }

    // Делаем платформу доступной и незанятой
        isOccupied = false;
        
}
public void UpdateChildObjects()
{
    List<GameObject> allChildObjects = new List<GameObject>();
    AddChildObjectsToList(transform, allChildObjects);
    childObjects = allChildObjects.ToArray();
}

private void AddChildObjectsToList(Transform parent, List<GameObject> childList)
{
    for (int i = 0; i < parent.childCount; i++)
    {
        Transform child = parent.GetChild(i);
        
        // Игнорируем объекты с тегом VFX
        if (child.CompareTag("VFX"))
        {
            continue;
        }
        
        childList.Add(child.gameObject);
        if (child.childCount > 0)
        {
            AddChildObjectsToList(child, childList);
        }
    }
}

public void SetAllChildLayers(string layerName)
{
    int layer = LayerMask.NameToLayer(layerName);
    foreach (GameObject child in childObjects)
    {
        if(child != null)
        {
            child.layer = layer;
        }
    }
}


}








