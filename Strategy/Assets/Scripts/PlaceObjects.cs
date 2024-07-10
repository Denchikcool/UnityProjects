using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public LayerMask Layer;
    private float RotationSpeed = 60f;

    private void Start()
    {
        PositionObject();
    } 

    private void Update()
    {
        PositionObject();

        if (Input.GetMouseButtonDown(1))
        {
            gameObject.GetComponent<AutoCarCreate>().enabled = true;
            Destroy(gameObject.GetComponent<PlaceObjects>());
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * RotationSpeed);
        }
    }

    private void PositionObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, Layer))
        {
            transform.position = hit.point;
        }
    }
}
