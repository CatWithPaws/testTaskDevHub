using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CubeGenerator : MonoBehaviour
{
    public Action Every20CubesEvent;
    [SerializeField] private ScreenInput screenInput;
    [SerializeField] private CubeStack cube;
    [SerializeField] private GameObject cubePrefab;
    public int cubeShots = 0;
    private float sensivity = 2;

    private bool isCubeTrown = false;
    // Update is called once per frame
    private void Start()
    {
        SpawnNewCube();
    }

    void Update()
    {
        if (screenInput.CurrTouch.Equals(default))
        {
            return;
        }
        
        float xAxis = screenInput.CurrTouch.deltaPosition.x/ sensivity;
        if(xAxis != 0 && cube != null)
        {
            cube.MoveHorizontal(xAxis);
        }
        if((screenInput.CurrTouch.phase == TouchPhase.Ended || screenInput.CurrTouch.phase == TouchPhase.Canceled) && !isCubeTrown)
        {
            cube.Move(Vector3.forward * 20);
            Invoke("SpawnNewCube", 0.3f);
            isCubeTrown = true;
        }
    }

    private void SpawnNewCube()
    {
        GameObject cubeObj = Instantiate(cubePrefab, transform.position, Quaternion.identity);
        CubeStack cubeStack = cubeObj.GetComponent<CubeStack>();
        cubeStack.UpdateCubeByProperty(PropertyHolder.instance.GetRandomPropertyTo64());
        cube = cubeStack;
        isCubeTrown = false;
        cubeShots++;
        if(cubeShots % 20 == 0)
        {
            cubeShots = 0;
            Every20CubesEvent?.Invoke();
        }
    }
}
