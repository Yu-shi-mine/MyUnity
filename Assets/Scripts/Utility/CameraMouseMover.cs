using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMouseMover : MonoBehaviour
{
    // �E�h���b�O�F�I�u�W�F�N�g�̉�]
    // �z�C�[���N���b�N�F�O�㍶�E�̈ړ�
    // �z�C�[����]�F�X�N���[��
    // �X�y�[�X�F�I�u�W�F�N�g����̗L���E�����̐؂�ւ�
    // P�F��]�����s���̏�Ԃɏ���������

    #region Member
    // �J�����̈ړ���
    [SerializeField, Range(0.1f, 10.0f)]
    private float _positionStep = 6.0f;

    // �}�E�X���x
    [SerializeField, Range(30.0f, 150.0f)]
    private float _mouseSensitive = 90.0f;

    // �X�N���[�����x
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

    // �E�N���b�N�F��]
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

    // �z�C�[���h���b�O�F�p��
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

    // �z�C�[���X�N���[���F�O��ړ�
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
