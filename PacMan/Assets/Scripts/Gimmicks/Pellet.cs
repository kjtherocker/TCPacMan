using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : Gimmick
{
    public override void CollidedWithPacMan()
    {
        GameManager.instance.ChangeScore(1);
        gameObject.SetActive(false);
    }
}
