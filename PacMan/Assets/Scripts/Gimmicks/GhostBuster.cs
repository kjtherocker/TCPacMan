using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuster : Gimmick
{
    public override void CollidedWithPacMan()
    {
        gameObject.SetActive(false);
    }
}
