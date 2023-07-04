using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(LEGO))]
public class LEGEditor : Editor
{
    #region Member
    LEGO _lego;
    #endregion


    #region Method
    private void OnEnable()
    {
        _lego = (LEGO)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();

        var label = "Color";
        var selectedIndex = _lego.ColorIndex;

        System.Func<string, int, string> selector = (string name, int number) => $"{number}: \r{name}";
        var displayOptions = _lego.ColorName.Select(selector).ToArray();
        var index = displayOptions.Length > 0 ? EditorGUILayout.Popup(label, selectedIndex, displayOptions) : -1;

        if (EditorGUI.EndChangeCheck())
        {
            var objectToUndo = _lego;
            var name = "LEGO";

            Undo.RecordObject(objectToUndo, name);
            _lego.ColorIndex = index;
        }
    }
    #endregion
}
#endif
