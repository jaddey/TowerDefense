using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Object", menuName = "Building Object")]
public class BuildingObject : ScriptableObject
{
    public GameObject buildingPrefab; // Префаб турели
    public GameObject hologramPrefab; // Префаб голограммы турели
}
