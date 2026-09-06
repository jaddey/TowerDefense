using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    public int x, y;
    public bool isOccupied = false;

    public void Occupy()
    {
        isOccupied = true;
    }

    public void Vacate()
    {
        isOccupied = false;
    }
}
