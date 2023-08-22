using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
{
    public bool isClikedJump;

    private float cliktime = 0.1f;
    private float time = 0;
    
    public void TaskOnClickJump()
    {
        isClikedJump = true;
        time = 0;
    }

    private void Update()
    {
       time += Time.deltaTime;
        if (time > cliktime)
        {
            isClikedJump = false;
        }
    }
}
