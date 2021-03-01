using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{

    public static class Constants
    {

        public static Vector3 HeightOffGrid = new Vector3(0,2,0);
        
        public static float PacManSpeed = 17;

        public static float GhostStartDelay = 4.0f;
        
        public static float GhostNormalSpeed = 10.05f;
        public static float GhostEatableSpeed = 2.5f;
        public static float GhostAggressiveTime = 10.0f;
        public static float GhostEatbleTime = 5.0f;
        public static int GhostFrameRecalculation = 1;
        
        public static int PlayerLives = 3;


    }
}