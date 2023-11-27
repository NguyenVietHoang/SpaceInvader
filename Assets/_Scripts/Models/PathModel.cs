using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathModel : MonoBehaviour
{
    [SerializeField]
    private List<Transform> PathPos;

    [Space(30)]
    [SerializeField]
    private Color32 PathColorForTest;

    List<PathElt> pathList;
    public void Initialize()
    {
        Debug.Log("[PathModel][Initialize] Init the path...");
        pathList = new List<PathElt>();

        if(PathPos != null && PathPos.Count > 1)
        {
            PathElt preElt = new PathElt(null, null, 0, PathPos[0]);
            pathList.Add(preElt);
            for (int i = 1; i < PathPos.Count; i++)
            {
                PathElt curElt = new PathElt(preElt, null, i, PathPos[i]);

                preElt.SetNext(curElt);
                curElt.SetPre(preElt);

                pathList.Add(curElt);
                preElt = curElt;
            }
        }        
    }

    /// <summary>
    /// Get the path root.
    /// </summary>
    /// <returns></returns>
    public PathElt GetPathRoot()
    {
        if(pathList != null && pathList.Count > 0)
        {
            return pathList[0];
        }
        else
        {
            return null;
        }
    }

    #region Only  for Test
    void OnDrawGizmos()
    {
        if (PathPos != null && PathPos.Count > 2)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = PathColorForTest;
            Gizmos.DrawLine(PathPos[0].position, PathPos[1].position);

            if(PathPos.Count > 3)
            {
                for (int i = 1; i < PathPos.Count - 1; i++)
                {
                    Gizmos.DrawLine(PathPos[i].position, PathPos[i + 1].position);
                }
            }            
        }
    }
    #endregion
}

public class PathElt
{
    public PathElt Pre { get; private set; }
    public PathElt Next { get; private set; }
    public int EltID { get; private set; }
    public Transform PathPos { get; private set; }

    public PathElt(PathElt _pre, PathElt _next, int _eltID, Transform _pathPos)
    {
        Pre = _pre;
        Next = _next;
        EltID = _eltID;
        PathPos = _pathPos;
    }

    public void SetPre(PathElt _Pre)
    {
        Pre = _Pre;
    }

    public void SetNext(PathElt _Next)
    {
        Next = _Next;
    }
}
