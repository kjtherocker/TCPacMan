using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.ChangeLives(-1);
        
    }
}
