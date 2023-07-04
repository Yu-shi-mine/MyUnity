using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(TableDataExporter))]
public class TableDataExporterEditor : Editor
{
    #region Member
    TableDataExporter _expoter;
    #endregion


    #region Method
    private void OnEnable()
    {
        _expoter = (TableDataExporter)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Export Table Data"))
        {
            _expoter.SendMessage("ExportTable", null, SendMessageOptions.DontRequireReceiver);
        }
    }
    #endregion
}
#endif
