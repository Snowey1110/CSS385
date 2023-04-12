using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject turretPrefab;
    private GameObject turretPreview;
    private bool isPlacingTurret = false;

    public void OnButtonClick()
    {
        if (!isPlacingTurret)
        {
            isPlacingTurret = true;
            CreateTurretPreview();
        }
    }

    void Update()
    {
        if (isPlacingTurret)
        {
            Vector3 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Pos.z = 0;

            turretPreview.transform.position = Pos;

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(turretPrefab, Pos, Quaternion.identity);
                Destroy(turretPreview);
                isPlacingTurret = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(turretPreview);
                isPlacingTurret = false;
            }
        }
    }

    void CreateTurretPreview()
    {
        SpriteRenderer turretSpriteRenderer = turretPrefab.GetComponent<SpriteRenderer>();
        turretPreview = new GameObject("Turret Preview");
        SpriteRenderer spriteRenderer = turretPreview.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = turretSpriteRenderer.sprite;
        spriteRenderer.color = new Color(turretSpriteRenderer.color.r, turretSpriteRenderer.color.g, turretSpriteRenderer.color.b, 0.5f);
        turretPreview.transform.localScale = turretPrefab.transform.localScale;
    }
}