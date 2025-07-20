using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1, camera2, camera3,camera4;
    void Start()
    {
        ActiveCamera(camera1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActiveCamera(camera1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActiveCamera(camera2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActiveCamera(camera3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActiveCamera(camera4);
        }
    }
    void ActiveCamera(Camera activeCamera)
    {
        camera1.gameObject.SetActive(false);
        camera2.gameObject.SetActive(false);
        camera3.gameObject.SetActive(false);
        camera4.gameObject.SetActive(false);

        activeCamera.gameObject.SetActive(true);
    }
}
