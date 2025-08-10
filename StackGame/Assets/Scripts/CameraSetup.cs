using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraSetup : MonoBehaviour
{
    [SerializeField] float topMarginViewport = 0.15f;
    [SerializeField] float stepUpAmount = 0.6f;
    [SerializeField] float smoothTime = 0.15f;

    float targetY;
    float yVel;
    Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
        targetY = transform.position.y;
    }
    void Update()
    {
        if (MovingCube.LastCube == null) return;

        var cube = MovingCube.LastCube.transform;
        Vector3 v = cam.WorldToViewportPoint(cube.position);

        if (v.z < 0f) return;
        float halHeighyViewportY = (cube.localScale.y * 0.5f) / cam.orthographicSize;
        float cuveToViewportY = v.y + halHeighyViewportY;
        float ceiling = 1f - topMarginViewport;

        if (cuveToViewportY > ceiling)
            targetY += stepUpAmount;

        float newY = Mathf.SmoothDamp(transform.position.y,targetY,ref yVel,smoothTime);

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
