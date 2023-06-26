using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMouseMover : MonoBehaviour
{
    // 右ドラッグ：オブジェクトの回転
    // ホイールクリック：前後左右の移動
    // ホイール回転：スクロール
    // スペース：オブジェクト操作の有効・無効の切り替え
    // P：回転を実行時の状態に初期化する

    #region Member
    // カメラの移動量
    [SerializeField, Range(0.1f, 10.0f)]
    private float _positionStep = 6.0f;

    // マウス感度
    [SerializeField, Range(30.0f, 150.0f)]
    private float _mouseSensitive = 90.0f;

    // スクロール感度
    [SerializeField, Range(0.1f, 10.0f)]
    private float _scrollSensitive = 1.0f;

    private bool _moveActive = true;
    private Camera _camera;
    private Transform _camTransform;
    private Vector3 _startMousePos;
    private Vector3 _presentPos;
    private Quaternion _initialRotation;
    #endregion


    #region Property

    #endregion


    #region Constructor
    private void Awake()
    {
        _camera = this.GetComponent<Camera>();
        _camTransform = this.gameObject.transform;
        _initialRotation = this.gameObject.transform.rotation;
    }
    #endregion


    #region Method
    private void Update()
    {
        ControlIsActive();

        if (_moveActive)
        {
            ResetRotation();
            RotationMouseControl();
            SlideMouseControl();
            WheelControl();
        }
    }


    #endregion


    #region Event
    public void ControlIsActive()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _moveActive = !_moveActive;
        }
    }

    private void ResetRotation()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.gameObject.transform.rotation = _initialRotation;
            Debug.Log("Cam Rotate : " + _initialRotation.ToString());
        }
    }

    // 右クリック：回転
    private void RotationMouseControl()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _startMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            float x = (_startMousePos.x - Input.mousePosition.x) / Screen.width;
            float y = (_startMousePos.y - Input.mousePosition.y) / Screen.height;
            _camTransform.RotateAround(Vector3.zero, _camTransform.up, -x * _mouseSensitive);
            _camTransform.RotateAround(Vector3.zero, _camTransform.right, y * _mouseSensitive);
            _startMousePos = Input.mousePosition;
        }
    }

    // ホイールドラッグ：パン
    private void SlideMouseControl()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _startMousePos = Input.mousePosition;
            _presentPos = _camTransform.position;
        }

        if (Input.GetMouseButton(2))
        {
            float x = (_startMousePos.x - Input.mousePosition.x) / Screen.width;
            float y = (_startMousePos.y - Input.mousePosition.y) / Screen.height;

            x = x * _positionStep;
            y = y * _positionStep;

            Vector3 velocity = _camTransform.rotation * new Vector3(x, y, 0);
            velocity = velocity + _presentPos;
            _camTransform.position = velocity;
        }
    }

    // ホイールスクロール：前後移動
    private void WheelControl()
    {
        if (Input.mouseScrollDelta.magnitude > 0)
        {
            var scroll = Input.mouseScrollDelta.y;
            _camTransform.position += _camTransform.forward * scroll * _scrollSensitive;
        }
    }
    #endregion
}
