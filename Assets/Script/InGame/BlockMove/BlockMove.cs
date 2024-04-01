/* 制作日 2024/02/21
*　製作者 ニシガキ
*　最終更新日 2024/02/21
*/

using UnityEngine;
using System.Collections;
 
/// <summary>
/// ブロックが持つ挙動を実行するクラス
/// </summary>
public class BlockMove : MonoBehaviour 
{
    private const int RIGHT = 1;
    private const int LEFT = -1;

    /// <summary>
    /// 選択中のブロックの挙動を実行
    /// </summary>
    /// <param name="input">操作入力</param>
    public void ExecutionMove(int input)
    {
        // 選択中のブロックがない場合　または　準備ステータス以外の場合
        if(SelectingBlock.GetBlock == null || StateManager.nowGameStateProperty.Value != E_GameState.InGame_Ready)
        {
            // 何もしない
            return;
        }

        // 選択中のブロックのBlockActionを実行する
        SelectingBlock.GetBlock.GetComponent<IBlockAction>().BlockAction(input);
    }

    public void RightEvent()
    {
        ExecutionMove(1);
    }

    public void LeftEvent()
    {
        ExecutionMove(-1);
    }
}