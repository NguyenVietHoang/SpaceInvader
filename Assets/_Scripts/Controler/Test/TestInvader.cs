using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInvader : MonoBehaviour
{
    public ObjectPooling testInvaderPool;
    public int invaderID;
    public PathModel path;
    public StageModel stage;

    private void Start()
    {
        testInvaderPool.Initialize();
        path.Initialize();
    }

    public void SpawnInvader()
    {
        Debug.Log("[TestInvader][SpawnInvader] Spawn Invader...");

        PoolingElt tmp = testInvaderPool.GetPooledObject();
        if(tmp != null)
        {
            InvaderControler invaderControl = tmp.GetComponent<InvaderControler>();

            if (invaderControl != null)
            {
                invaderControl.Initialize(path, stage.invaderData);
            }
        }        
    }
}
