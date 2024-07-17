using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    public enum PathTypes { lenear, loop }
    public PathTypes PathType;

    public int movementDirection = 1;
    public int movingTo = 0;
    public Transform[] PathElements;

    public void OnDrawGizmos()
    {
        if (PathElements == null || PathElements.Length < 2)
            return;

        for (int pointCount = 1; pointCount < PathElements.Length; pointCount++)
        {
            Gizmos.DrawLine(PathElements[pointCount - 1].position, PathElements[pointCount].position);
        }
        if (PathType == PathTypes.loop)
        {
            Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length - 1].position);
        }
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        Debug.Log("GetNextPathPoint");
        if (PathElements == null || PathElements.Length < 2)
        {
            Debug.Log("not enough path elements");
            yield break;
        }
        while (true)
        {
            yield return PathElements[movingTo];

            if (PathElements.Length == 1)
            {

                continue;
            }

            if (PathType == PathTypes.lenear)
            {
                if (movingTo <= 0)
                {
                    movementDirection = 1;
                }
                else if (movingTo >= PathElements.Length - 1)
                {
                    movementDirection = -1;
                }
            }
            movingTo = movingTo + movementDirection;

            if (PathType == PathTypes.loop)
            {
                if (movingTo >= PathElements.Length)
                {
                    movingTo = 0;
                }
                if (movingTo < 0)
                {
                    movingTo = PathElements.Length - 1;
                }
            }
        }
    }
}