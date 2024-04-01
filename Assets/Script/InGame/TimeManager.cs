/* 制作日 2024/02/22
*　製作者 ニシガキ
*　最終更新日 2024/02/22
*/

using UnityEngine;
using System.Collections;
 
public static class TimeManager 
{
    private static float _timeScale = 1;

    public static float _deltaTime { get { return 0.0167f * _timeScale; } }

    public static float getTimeScale { get { return _timeScale; } }


    public static void StopTime()
    {
        Debug.Log("停止");
        _timeScale = 0f;
    }

    public static void StartTime()
    {
        Debug.Log("開始");
        _timeScale = 1f;
    }

    public static void RewindTime()
    {
        Debug.Log("不明");
        _timeScale = -1f;
    }
}