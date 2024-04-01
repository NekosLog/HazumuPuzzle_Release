/* 制作日 2024/02/09
*　製作者 ニシガキ
*　最終更新日 2024/02/09
*/

using UnityEngine;
using System.Collections;
 
public class InputController : MonoBehaviour 
{
    [SerializeField, Tooltip("カメラ")]
    private GameObject _camera = default;

    [SerializeField, Tooltip("正面のカメラの位置")]
    private Transform _forwardTransform = default;

    [SerializeField, Tooltip("右のカメラの位置")]
    private Transform _rightTransform = default;

    [SerializeField, Tooltip("背後のカメラの位置")]
    private Transform _backTransform = default;

    [SerializeField, Tooltip("左のカメラの位置")]
    private Transform _leftTransform = default;

    
    [Tooltip("カメラの設置位置")]
    private enum E_CameraPosition
    {
        Forward   = 1,
        Right    = 2,
        Back     = 3,
        Left     = 4
    }

    // 今のカメラの位置
    private E_CameraPosition _nowCameraPosition;

    private void Update()
    {
        // 右を押されたとき
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            UpCameraPosition();
        }

        // 左を押されたとき
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DownCameraPosition();
        }
    }

    private void UpCameraPosition()
    {
        // Left以外のときは一つ増やす
        if (_nowCameraPosition < E_CameraPosition.Left)
        {
            _nowCameraPosition++;
        }
        // LeftのときはForwardにする
        else
        {
            _nowCameraPosition = E_CameraPosition.Forward;
        }

        // カメラの位置を設定する
        ChengeCameraPosition();
    }

    private void DownCameraPosition()
    {
        // Forward以外のときは一つ減らす
        if (_nowCameraPosition > E_CameraPosition.Forward)
        {
            _nowCameraPosition--;
        }
        // ForwardのときはLeftにする
        else
        {
            _nowCameraPosition = E_CameraPosition.Left;
        }

        // カメラの位置を設定する
        ChengeCameraPosition();
    }


    /// <summary>
    /// カメラの位置を指定した設置位置に移動させるメソッド
    /// </summary>
    private void ChengeCameraPosition()
    {
        switch (_nowCameraPosition)
        {
            // 正面のカメラの位置を設定
            case E_CameraPosition.Forward:
                _camera.transform.position = _forwardTransform.position;
                _camera.transform.rotation = _forwardTransform.rotation;
                break;

            // 右のカメラの位置を設定
            case E_CameraPosition.Right:
                _camera.transform.position = _rightTransform.position;
                _camera.transform.rotation = _rightTransform.rotation;
                break;

            // 背後のカメラの位置を設定
            case E_CameraPosition.Back:
                _camera.transform.position = _backTransform.position;
                _camera.transform.rotation = _backTransform.rotation;
                break;

            // 左のカメラの位置を設定
            case E_CameraPosition.Left:
                _camera.transform.position = _leftTransform.position;
                _camera.transform.rotation = _leftTransform.rotation;
                break;
        }
    }
}