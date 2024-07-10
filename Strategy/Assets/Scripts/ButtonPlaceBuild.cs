using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlaceBuild : MonoBehaviour
{
    public GameObject Building;

    public void PlaceBuild()
    {
        Instantiate(Building, Vector3.zero, Quaternion.identity);
    }
}
