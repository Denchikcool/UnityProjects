using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float RotateSpeed = 10.0f;
    public float Speed = 10.0f;
    public float ZoomSpeed = 10.0f;

    private float _cameraAcceleration = 1f;

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        float rotate = 0f;
        if (Input.GetKey(KeyCode.Q)) rotate = -1f;
        else if (Input.GetKey(KeyCode.E)) rotate = 1f;

        _cameraAcceleration = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;

        transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime * rotate * _cameraAcceleration, Space.World);

        transform.Translate(new Vector3(horizontalMovement, 0, verticalMovement) * Time.deltaTime * _cameraAcceleration * Speed, Space.Self);

        transform.position += transform.up * ZoomSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -20f, 30f),transform.position.z);
    }
}
