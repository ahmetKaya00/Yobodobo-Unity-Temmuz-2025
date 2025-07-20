using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcherController : MonoBehaviour
{
    [SerializeField]private List<Camera> cameras;
    void Start()
    {
        ActiveCamera(0);
    }
    void Update()
    {
        for(int i = 0; i < cameras.Count; i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                ActiveCamera(i);
            }
        }
    }
    private void ActiveCamera(int index)
    {
        if(index < 0 || index >= cameras.Count)
        {
            Debug.LogError("Kamera Bulunamadý!");
            return;
        }
        for(int i = 0; i<cameras.Count; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
        }
    }
}
