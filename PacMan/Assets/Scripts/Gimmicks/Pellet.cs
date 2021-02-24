using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : Gimmick
{
    public override void CollidedWithPacMan()
    {
        gameObject.SetActive(false);
        GameManager.instance.ChangeScore(1);
        GameManager.instance.ChangeAmountOfPelletes();
        
    }
}
