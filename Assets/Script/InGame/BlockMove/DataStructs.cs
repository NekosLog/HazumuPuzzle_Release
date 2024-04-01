using UnityEngine;
using System.Collections.Generic;

public struct DropData
{
    // �h���b�v�̒��S�_
    public Vector3 _originPosition;

    // �h���b�v�̔��a
    public float _radius;

    // �R���X�g���N�^
    public DropData(Vector3 originPosition, float radius)
    {
        _originPosition = Vector3.zero;
        this._radius = radius;
    }
}