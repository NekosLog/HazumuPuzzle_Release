/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockData : MonoBehaviour
{
    public const int VERTEX_COUNT = 8;

    E_NormalDirection[][] _vertexVectors = new E_NormalDirection[8][]
    {
        new E_NormalDirection[3]{ E_NormalDirection.Right, E_NormalDirection.Up  , E_NormalDirection.Forward},
        new E_NormalDirection[3]{ E_NormalDirection.Left , E_NormalDirection.Up  , E_NormalDirection.Forward},
        new E_NormalDirection[3]{ E_NormalDirection.Right, E_NormalDirection.Up  , E_NormalDirection.Back   },
        new E_NormalDirection[3]{ E_NormalDirection.Left , E_NormalDirection.Up  , E_NormalDirection.Back   },
        new E_NormalDirection[3]{ E_NormalDirection.Right, E_NormalDirection.Down, E_NormalDirection.Forward},
        new E_NormalDirection[3]{ E_NormalDirection.Left , E_NormalDirection.Down, E_NormalDirection.Forward},
        new E_NormalDirection[3]{ E_NormalDirection.Right, E_NormalDirection.Down, E_NormalDirection.Back   },
        new E_NormalDirection[3]{ E_NormalDirection.Left , E_NormalDirection.Down, E_NormalDirection.Back   }
    };

    public Vector3[] GetVertexPositions()
    {
        Vector3[] vertexPositions = new Vector3[VERTEX_COUNT];

        for (int i = 0; i < vertexPositions.Length; i++)
        {
            E_NormalDirection[] vertexVector = _vertexVectors[i];

            Vector3 vertexDirection = GetVertexDirection(vertexVector);

            vertexPositions[i] = transform.TransformPoint(vertexDirection);
        }

        return vertexPositions;
    }

    public Vector3 GetVertexDirection(E_NormalDirection[] vertexVector)
    {
        Vector3 returnVector = new Vector3();

        if (vertexVector[0] == E_NormalDirection.Right)
        {
            returnVector.x = 0.5f;
        }
        else
        {
            returnVector.x = -0.5f;
        }

        if (vertexVector[1] == E_NormalDirection.Up)
        {
            returnVector.y = 0.5f;
        }
        else
        {
            returnVector.y = -0.5f;
        }

        if (vertexVector[2] == E_NormalDirection.Forward)
        {
            returnVector.z = 0.5f;
        }
        else
        {
            returnVector.z = -0.5f;
        }

        return returnVector;
    }

    public Vector3 GetNormal(E_NormalDirection direction)
    {
        Vector3 returnNormal = default;

        switch (direction)
        {
            case E_NormalDirection.Up:
                returnNormal = transform.up;
                break;

            case E_NormalDirection.Down:
                returnNormal = -transform.up;
                break;

            case E_NormalDirection.Right:
                returnNormal = transform.right;
                break;

            case E_NormalDirection.Left:
                returnNormal = -transform.right;
                break;

            case E_NormalDirection.Forward:
                returnNormal = transform.forward;
                break;

            case E_NormalDirection.Back:
                returnNormal = -transform.forward;
                break;
        }

        return returnNormal;
    }

    public E_NormalDirection[] GetAdjacent(int vertexIndex)
    {
        E_NormalDirection[] returnAdjacent = new E_NormalDirection[3];

        returnAdjacent = _vertexVectors[vertexIndex];

        return returnAdjacent;
    }

    public Vector3 GetEdgeVector(List<E_NormalDirection> adjacentSurface, int nearestVertexIndex)
    {
        int[,] checkedDirections = new int[6, 4]
        {
            // 025,23
            { 1,3,5,7},
            { 0,2,4,6},
            { 4,5,6,7},
            { 0,1,2,3},
            { 2,3,6,7},
            { 0,1,4,5}
        };

        List<int> unCheckedDirectionList = new List<int>{ 0,1,2,3,4,5,6,7};

        int adjacentDirection1 = (int)adjacentSurface[0];
        int adjacentDirection2 = (int)adjacentSurface[1];

        // unCheckedDirectionListからcheckedDirectionList[adjacentDirection1]とcheckedDirectionList[adjacentDirection2]と一致する値を削除

        for (int i = 0; i < 4; i++)
        {
            unCheckedDirectionList.Remove(checkedDirections[adjacentDirection1, i]);
        }

        for (int i = 0; i < 4; i++)
        {
            unCheckedDirectionList.Remove(checkedDirections[adjacentDirection2, i]);
        }

        unCheckedDirectionList.Remove(nearestVertexIndex);

        Vector3 startVertexDirection = GetVertexDirection(_vertexVectors[nearestVertexIndex]);

        Vector3 pairVertexDirection = GetVertexDirection(_vertexVectors[unCheckedDirectionList[0]]);

        Vector3 startVertexPosition = transform.TransformPoint(startVertexDirection);

        Vector3 pairVertexPosition = transform.TransformPoint(pairVertexDirection);

        Vector3 edgeVector = pairVertexPosition - startVertexPosition;

        Debug.Log($"始点：{startVertexPosition},{nearestVertexIndex}　　終点：{pairVertexPosition},{unCheckedDirectionList[0]}　　ベクトル：{edgeVector}");

        return edgeVector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}