using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TableDataExporter : MonoBehaviour
{
    #region Member
    private const string DELIMITER = ",";
    private readonly Encoding CSV_ENCODING = Encoding.GetEncoding("Shift_JIS");
    #endregion


    #region Method
    private void OutputCSV(Assy assy)
    {
        foreach (KeyValuePair<int, LEGO> kvp in assy.LEGODict)
        {
            var index = kvp.Key;
            var lego = kvp.Value;

            var line = GetLEGOInfo(lego);
            Debug.Log(line);
        }
    }

    private string GetLEGOInfo(LEGO lego)
    {
        return lego.Pocchi.position.ToString();
    }
    #endregion


    #region Event
    private void ExportTable()
    {
        List<LEGO> legoList = new List<LEGO>();
        foreach (LEGO lego in this.transform.GetComponentsInChildren<LEGO>())
        {
            lego.DefineLegoInfo();
            legoList.Add(lego);
        }
        Assy assy = new Assy(legoList);
        OutputCSV(assy);
    }
    #endregion
}
