using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuster : Gimmick
{
    public override void CollidedWithPacMan()
    {
        GameManager.instance.MakeGhostsEatable();
        GameManager.instance.ChangeScore(100);
        gameObject.SetActive(false);
    }
}
