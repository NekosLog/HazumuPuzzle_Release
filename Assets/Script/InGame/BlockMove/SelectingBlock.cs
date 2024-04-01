/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;
 
/// <summary>
/// ブロックを選択するためのクラス
/// </summary>
public static class SelectingBlock 
{
    // 選択中のブロック
	private static GameObject _selectingBlock = default;
	
    /// <summary>
    /// ブロックを選択する処理
    /// </summary>
    /// <param name="selectBlock">選択するブロック</param>
	public static void SetBlock(GameObject selectBlock)
    {
        // 既に選択中のブロックがあった場合
        if (_selectingBlock != null)
        {
            // 前のブロックの外形戦を消す
            _selectingBlock.transform.GetChild(0).GetComponent<Outline>().enabled = false;
        }

        // 選択中のブロックを更新する
        _selectingBlock = selectBlock;

        // 新しいブロックの外形戦を作る
        _selectingBlock.transform.GetChild(0).GetComponent<Outline>().enabled = true;
    }
    
    /// <summary>
    /// 選択中のブロックを取得するためのプロパティ
    /// </summary>
    public static GameObject GetBlock
    {
        get { return _selectingBlock; }
    }
}