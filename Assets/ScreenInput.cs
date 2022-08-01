using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInput : MonoBehaviour
{
   
    private Touch currTouch;
    public Touch CurrTouch => currTouch;

    public void Update()
    {
        if(Input.touchCount > 0)
        {
            currTouch = Input.GetTouch(0);
        }
        else
        {
            currTouch = default;
        }
    }

}
