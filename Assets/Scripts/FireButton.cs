using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    public bool isClikedFire;

    private float cliktime = 0.1f;
    private float time = 0;
    
    public void TaskOnClickFire()
    {
        isClikedFire = true;
        time = 0;
    }

    private void Update()
    {
       time += Time.deltaTime;
        if (time > cliktime)
        {
            isClikedFire = false;
        }
    }
}
