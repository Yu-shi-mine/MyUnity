using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEGO : MonoBehaviour
{
    #region Property
    public Pocchi Pocchi { get { return _pocchi; } }
    public bool IsRotate { get { return _isRotate; } }
    public string[] ColorName { get { return _colorName; } }
    public int ColorIndex { get { return _colorIndex; } set { _colorIndex = value; } }
    #endregion


    #region Member
    private Pocchi _pocchi;
    [Header("Pocchi")]
    [SerializeField] private Vector3 _position;
    [SerializeField] private Vector2 _size;
    [SerializeField] private bool _isRotate;

    private static readonly string[] _colorName = { "White", "Red", "Blue" };
    private int _colorIndex = 0;
    #endregion


    #region Method
    public void DefineLegoInfo()
    {
        _pocchi = new Pocchi(_position, _size);
    }
    #endregion
}

public class Pocchi
{
    #region Property
    public Vector3 position { get { return _position; } }
    public Vector2 Size { get { return _size; } }

    #endregion


    #region Member
    private Vector3 _position;
    private Vector2 _size;
    #endregion


    #region Constructor
    public Pocchi(Vector3 position, Vector2 size)
    {
        _position = position;
        _size = size;
    }
    #endregion


    #region Method

    #endregion
}