// ----------------------------------------------------------------------------
// 作成者 
// 作成開始日 
// 特筆事項 
// ----------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StageDatas", menuName = "ScriptableObjects/CreateStageDatas")]
public class StageDataObject : ScriptableObject
{
    public List<StageData> StageDataList = new List<StageData>();
}

[System.Serializable]
public class StageData
{
    public int stageNumber = default;
    public Vector3 dropStartPosition = default;
}