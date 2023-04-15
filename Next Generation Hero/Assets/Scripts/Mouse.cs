using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse : MonoBehaviour
{

    [SerializeField] private float offsetX = 15f;
    [SerializeField] private float offsetY = -20f;
    [SerializeField] private float flySpeed = 200f;
    [SerializeField] public Button Button;

    private bool isFlying = false;

    private void Start()
    {
        Cursor.visible = false;
        Button.onClick.AddListener(FlyOffScreen);
    }

    void Update()
    {
        if (!isFlying)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;

            // Apply the offsets
            mousePosition.x += offsetX;
            mousePosition.y += offsetY;

            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        } else
        {
            FlyOffScreen();
        }
    }

    private void FlyOffScreen()
    {
        isFlying = true;
        transform.position += Quaternion.Euler(0, 0, 100) * transform.right * flySpeed * Time.deltaTime;
    }
}
