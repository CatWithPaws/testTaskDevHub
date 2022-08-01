using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(Rigidbody))]
public class CubeStack : MonoBehaviour
{
    public static int currID = 0;


    [SerializeField] TextMeshPro[] textMeshes;
    [SerializeField] CubeStackProperty cubeProperty;
    [SerializeField] Rigidbody rb;
    public Rigidbody Rb => rb;
    [SerializeField] MeshRenderer meshRenderer;

    public int Value => cubeProperty.Value;

    public int ID;
    public void Start()
    {
        ID = currID++;
       //rb.velocity = Vector3.forward * 20;
    }

    public void UpdateCubeByProperty(CubeStackProperty cubeProperty)
    {
        meshRenderer.material.color = cubeProperty.Color;

        foreach(var item in textMeshes)
        {
            item.text = cubeProperty.Value.ToString();
        }
        this.cubeProperty = cubeProperty;
    }

    public void OnMerged()
    {
        rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out CubeStack otherCube))
        {
            if (otherCube.Value == this.Value)
            {
                GameObject objToDestroy;
                CubeStack objToMerge;

                objToDestroy = otherCube.ID < this.ID ? otherCube.gameObject : this.gameObject;
                objToMerge = otherCube.ID < this.ID  ? this : otherCube;

                Destroy(objToDestroy);

                transform.rotation = Quaternion.identity;
                rb.velocity = Vector3.up;
                CubeStackProperty property = PropertyHolder.instance.GetProperty(objToMerge.Value * 2);

                
                UpdateCubeByProperty(property);
                objToMerge.OnMerged();
            }
        }
    }

    public void Move(Vector3 direction)
    {
        rb.velocity = direction;
    }
    public void MoveHorizontal(float xAxis)
    {
        rb.velocity = new Vector3(xAxis, rb.velocity.y, rb.velocity.z);
    }
}
