/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
 
public interface IBlockAction
{
    void BlockAction(int input);
    public bool _isMaxPattern { get; }

    public bool _isMinPattern { get; } 
}