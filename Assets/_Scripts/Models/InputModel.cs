using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Input", menuName = "SpaceInvader/Input", order = 2)]
public class InputModel : ScriptableObject
{
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Shoot;
}
