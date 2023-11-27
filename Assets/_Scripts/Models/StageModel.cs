using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "SpaceInvader/Stage", order = 1)]
public class StageModel : ScriptableObject
{
    public int maxInvaderToSpawn;
    public InvaderModel invaderData;
    public float spawnDelay;
    public PATH path;
}

public enum PATH
{
    DEFAULT,
    MEDIUM,
}
