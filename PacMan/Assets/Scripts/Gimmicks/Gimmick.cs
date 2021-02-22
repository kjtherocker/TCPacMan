﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick : MonoBehaviour
{
    public virtual void CollidedWithPacMan()
    {
    }
    
    public void OnTriggerEnter(Collider other)
    {
        CollidedWithPacMan();
    }
}
