using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Invader", menuName = "SpaceInvader/Invader", order = 3)]
public class InvaderModel : ScriptableObject
{
    public int InvaderID = 0;
    public float InvaderSpd = 1.0f;
    public float InvaderAcc = 0.2f;

    public int Score = 1;
}
