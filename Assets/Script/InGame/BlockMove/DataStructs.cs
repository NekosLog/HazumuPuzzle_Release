using UnityEngine;
using System.Collections.Generic;

public struct DropData
{
    // ドロップの中心点
    public Vector3 _originPosition;

    // ドロップの半径
    public float _radius;

    // コンストラクタ
    public DropData(Vector3 originPosition, float radius)
    {
        _originPosition = Vector3.zero;
        this._radius = radius;
    }
}