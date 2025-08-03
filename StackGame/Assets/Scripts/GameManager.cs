using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnCubeSpawned = delegate { };
    private CubeRespawn[] spawners;
    private int spawnIndex;
    private CubeRespawn currentSpawn;

    private void Awake()
    {
        spawners = FindObjectsOfType<CubeRespawn>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                if(MovingCube.CurrentCube != null)
                    MovingCube.CurrentCube.Stop();
                spawnIndex = spawnIndex == 0 ? 1 : 0;
                currentSpawn = spawners[spawnIndex];
                currentSpawn.SpawnCube();

                OnCubeSpawned();
            }
        }
    }
}
