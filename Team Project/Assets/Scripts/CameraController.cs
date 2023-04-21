using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float panSpeed = 10f;
    private float panBorderThickness = 10f;
    private float zoomSpeed = 1000f;
    private float minZoom = 1f;
    private float maxZoom = 30f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Resetting camera");
            Vector3 defaultPosition = new Vector3(0, 0, -10);
            pos = defaultPosition;
            cam.orthographicSize = 5;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float newZoom = cam.orthographicSize - scroll * zoomSpeed * Time.deltaTime;

        cam.orthographicSize = Mathf.Clamp(newZoom, minZoom, maxZoom);
        transform.position = pos;
    }

    void ResetCameraToDefault()
    {
        Vector3 defaultPosition = new Vector3(0, 0, -10);
        transform.position = defaultPosition;
        cam.orthographicSize = 5;
    }
}