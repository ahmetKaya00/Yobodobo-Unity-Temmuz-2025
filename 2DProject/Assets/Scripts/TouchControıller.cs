using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControıller : MonoBehaviour
{
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("Dokunma Başladı!");
                        break;
                case TouchPhase.Moved:
                    Debug.Log("Dokunma hareket etti!");
                        break;
                case TouchPhase.Stationary:
                    Debug.Log("Dokunma sabit!");
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Dokunma sona erdi!");
                    break;
                case TouchPhase.Canceled:
                    Debug.Log("Dokunma iptal edildi!");
                    break;
            }

            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            if(touch.position.x < screenWidth / 2)
            {
                Debug.Log("Sola Dokundu");
            }
            else
            {
                Debug.Log("Sağına Dokundu");
            }
            if (touch.position.y < screenHeight / 2)
            {
                Debug.Log("Aşağıda Dokundu");
            }
            else
            {
                Debug.Log("Yukarıda Dokundu");
            }

        }
    }
}
