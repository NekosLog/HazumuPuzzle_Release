/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;
 
public class GoalManager : MonoBehaviour 
{
    private Vector3 _myPosition = default;
    private Vector3 _boxSize = default;
    private Quaternion _myRotate = default;
    private LayerMask _dropLayer = 1 << 7;
    private Animator _UIAnimator = default;

    private void Awake()
    {
        _myPosition = transform.position;
        _UIAnimator = GameObject.FindWithTag("ClearStageUI").GetComponent<Animator>();
        _boxSize = transform.localScale / 2;
        _myRotate = transform.rotation;
    }

    private void Update()
    {
        if (StateManager.nowGameStateProperty.Value != E_GameState.InGame_Clear)
        {
            // ブロックと接触していないかを判定
            Collider[] hitDrop = Physics.OverlapBox(_myPosition, _boxSize, _myRotate, _dropLayer);

            // 接触いないかどうか
            if (hitDrop.Length == 0)
            {
                // 接触していない時はreturn
                return;
            }

            Debug.LogWarning("ゴール");

            _UIAnimator.SetTrigger("ClearTrigger");
            ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._GoalSE);
            TimeManager.StopTime();
            StateManager.SetGameState(E_GameState.InGame_Clear);
            StartCoroutine("GotoTitleScene");
        }
    }

    private IEnumerator GotoTitleScene()
    {
        yield return new WaitForSeconds(2f);
        StateManager.SetGameState(E_GameState.Title);
        ProgressManager.Instance.LoadScene(E_SceneName.TitleScene);
    }
}