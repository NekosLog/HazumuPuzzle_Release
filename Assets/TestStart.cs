/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;
 
public class TestStart : MonoBehaviour 
{
 
	private void Awake()
	{
        Application.targetFrameRate = 60;
        TimeManager.StopTime();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TimeManager.StartTime();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TimeManager.StopTime();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TimeManager.RewindTime();
        }
    }


}