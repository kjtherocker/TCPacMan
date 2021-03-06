﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Floor_Classic : Floor
{
    // Start is called before the first frame update
    public override void Initialize()
    {
        m_GridDimensionX = 20;
        m_GridDimensionY = 20;
        

        m_FloorBlueprint = new short[]
        {   
            10, 11, 15, 11, 11,  9,  0, 10, 11, 11, 15, 11,  9,  0,  0,  0,  0,  0,  0,  0, //1
             6,  0,  6,  0,  0,  6,  0,  6,  0,  0,  6,  0,  6,  0,  0,  0,  0,  0,  0,  0, //2
            14, 11,  5, 11, 15, 12, 11, 12, 15, 11,  5, 11, 13,  0,  0,  0,  0,  0,  0,  0, //3
             6,  0,  6,  0,  6,  0,  0,  0,  6,  0,  6,  0,  6,  0,  0,  0,  0,  0,  0,  0, //4
             8, 11, 13,  0,  8,  9,  0, 10,  7,  0, 14, 11,  7,  0,  0,  0,  0,  0,  0,  0, //5
             0,  0,  6,  0, 0,   6,  0,  6,  0,  0,  6,  0,  0,  0,  0,  0,  0,  0,  0,  0, //6
             0,  0,  6,  0, 10, 12, 11, 12,  9,  0,  6,  0,  0,  0,  0,  0,  0,  0,  0,  0, //7
             0,  0, 14, 11, 13,  4, 12,  3, 14, 11, 13,  0, 0,  0,  0,  0,  0,  0,  0,  0, //8
             0,  0,  6,  0, 14, 11, 11, 11, 13,  0,  6,  0,  0,  0,  0,  0,  0,  0,  0,  0, //9
             0,  0,  6,  0,  6,  0,  0,  0,  6,  0,  6,  0,  0,  0,  0,  0,  0,  0,  0,  0, //10
            10, 11,  5, 11, 12,  9,  0, 10, 12, 11,  5, 11,  9,  0,  0,  0,  0,  0,  0,  0, //11
             6,  0,  6,  0,  0,  6,  0,  6,  0,  0,  6,  0,  6,  0,  0,  0,  0,  0,  0,  0, //12
             8,  9, 14, 11, 15, 12, 11, 12, 15, 11, 13, 10,  7,  0,  0,  0,  0,  0,  0,  0, //13
             0, 14,  7,  0,  6,  0,  0,  0,  6,  0,  8, 13,  0,  0,  0,  0,  0,  0,  0,  0, //14
            10,  7,  0,  0,  8,  9,  0, 10,  7,  0,  0, 8,  9,  0,  0,  0,  0,  0,  0,  0, //15
             6,  0,  0,  0,  0,  6,  0,  6,  0,  0,  0,  0,  6,  0,  0,  0,  0,  0,  0,  0, //16
             8, 11, 11, 11, 11, 12, 11, 12, 11, 11, 11, 11,  7,  0,  0,  0,  0,  0,  0,  0, //17
             0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, //18
             0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, //19
             0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0  //20
        //  1   2   3   4   5   6   7   8   9  10  11   12  13  14  15  16  17  18  19  20
        }; 
        
        
        m_GoalsBlueprint = new short[]
        {   
             1,  1,  1,  1,  1,  1,  0,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0, //1
             2,  0,  1,  0,  0,  1,  0,  1,  0,  0,  1,  0,  2,  0,  0,  0,  0,  0,  0,  0, //2
             1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0, //3
             1,  0,  1,  0,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  0,  0,  0,  0,  0,  0, //4
             1,  1,  1,  0,  1,  1,  0,  1,  1,  0,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0, //5
             0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0, //6
             0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0, //7
             0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0, //8
             0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0, //9
             0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0, //10
             1,  1,  1,  1,  1,  1,  0,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0, //11
             2,  0,  1,  0,  0,  1,  0,  1,  0,  0,  1,  0,  2,  0,  0,  0,  0,  0,  0,  0, //12
             1,  1,  1,  1,  1,  1,  0,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0, //13
             0,  1,  1,  0,  1,  0,  0,  0,  1,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0, //14
             1,  1,  0,  0,  1,  1,  0,  1,  1,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0, //15
             1,  0,  0,  0,  0,  1,  0,  1,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0, //16
             1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0, //17
             0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, //18
             0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, //19
             0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0  //20
        //  1   2   3   4   5   6   7   8   9  10  11   12  13  14  15  16  17  18  19  20
        }; 
        
        

        SpawnPlayer();
        SpawnGhosts();
    }
    
    public virtual void SpawnPlayer()
    {
        m_DefaultSpawnPosition = new Vector2Int(12,6);
    }



    public override void SpawnGhosts()
    {
        m_GhostSpawnPositions = new Dictionary<Ghosts.GhostTypes, Vector2Int>();
        m_GhostSpawnPositions.Add(Ghosts.GhostTypes.Blinky, new Vector2Int(6,6));
        m_GhostSpawnPositions.Add(Ghosts.GhostTypes.Inky, new Vector2Int(7,5));
        m_GhostSpawnPositions.Add(Ghosts.GhostTypes.Pinky, new Vector2Int(7,6));
        m_GhostSpawnPositions.Add(Ghosts.GhostTypes.Cylde, new Vector2Int(7,7));
        
        m_GhostCornerPositions = new Dictionary<Ghosts.GhostTypes, Vector2Int>();
        
        m_GhostCornerPositions.Add(Ghosts.GhostTypes.Blinky, new Vector2Int(0,12));
        m_GhostCornerPositions.Add(Ghosts.GhostTypes.Inky, new Vector2Int(0,0));
        m_GhostCornerPositions.Add(Ghosts.GhostTypes.Pinky, new Vector2Int(16,0));
        m_GhostCornerPositions.Add(Ghosts.GhostTypes.Cylde, new Vector2Int(16,12));
    }
}
