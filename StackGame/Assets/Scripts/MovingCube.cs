using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }
    public MoveDirection MoveDirection { get; set; }
    [SerializeField] float moveSpeed = 1f;

    private void OnEnable()
    {
        if(LastCube == null)
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        CurrentCube = this;
        GetComponent<Renderer>().material.color = GetRandomColor();
        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0.1f, 1f), UnityEngine.Random.Range(0.1f, 1f), UnityEngine.Random.Range(0.1f, 1f));
    }

    public void Stop()
    {
        moveSpeed = 0f;
        float breakZ = GetBreak();

        float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;

        if (Math.Abs(breakZ) >= max)
        {
            LastCube = null;
            CurrentCube = null;
            SceneManager.LoadScene(0);
        }

        float direction = breakZ > 0 ? 1f : -1f;
        if(MoveDirection == MoveDirection.Z)
            SplitCubeZ(breakZ,direction);
        else
            SplitCubeX(breakZ, direction);
        LastCube = this;
    }

    private float GetBreak()
    {
        if (MoveDirection == MoveDirection.Z)
            return transform.position.z - LastCube.transform.position.z;
        else
            return transform.position.x - LastCube.transform.position.x;
    }

    private void SplitCubeZ(float breakZ,float direction)
    {
        float newSize = LastCube.transform.localScale.z - Math.Abs(breakZ);
        float fallingBlockSize = transform.localScale.z - newSize;
        float newPosition = LastCube.transform.localPosition.z + (breakZ / 2);
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y,newSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition);

        float cubeEdge = transform.position.z + (newSize / 2 * direction);
        float fallingBlockZpos = cubeEdge + fallingBlockSize / 2f * direction;

        DropCube(fallingBlockZpos, fallingBlockSize);
    }
    private void SplitCubeX(float breakZ,float direction)
    {
        float newSize = LastCube.transform.localScale.x - Math.Abs(breakZ);
        float fallingBlockSize = transform.localScale.x - newSize;
        float newPosition = LastCube.transform.localPosition.x + (breakZ / 2);
        transform.localScale = new Vector3(newSize,transform.localScale.y,transform.localScale.z);
        transform.position = new Vector3(newPosition,transform.position.y, transform.position.z);

        float cubeEdge = transform.position.z + (newSize / 2 * direction);
        float fallingBlockZpos = cubeEdge + fallingBlockSize / 2f * direction;

        DropCube(fallingBlockZpos, fallingBlockSize);
    }
    private void DropCube(float fallingBlockZPos, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (MoveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPos);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize,transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockZPos,transform.position.y, transform.position.z);
        }
        
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f);
    } 

    private void Update()
    {
        if(MoveDirection == MoveDirection.Z)
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        else
            transform.position += transform.right * Time.deltaTime * moveSpeed;
    }



}
