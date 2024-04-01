/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;
 
public class BlockSelector : MonoBehaviour 
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            LayerMask blockLayer = 1 << 6;
            RaycastHit hitBlock = default;
            if (Physics.Raycast(ray,out hitBlock,500f,blockLayer))
            {
                SelectingBlock.SetBlock(hitBlock.transform.parent.gameObject);
            }
        }
    }
}