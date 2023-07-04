using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assy
{
    #region Property
    public Dictionary<int, LEGO> LEGODict { get { return _legoDict; } }
    public int[,,] Assembly { get { return _assembly; } }
    #endregion


    #region Member
    private Dictionary<int, LEGO> _legoDict;
    private int[,,] _assembly;

    private const int MAX_POCCHI_XY = 5;
    private const int MAX_POCCHI_Z = 3;
    #endregion


    #region Constructor
    public Assy(List<LEGO> legoList)
    {
        _legoDict = FromList(legoList);
        _assembly = Build(_legoDict);
    }
    #endregion


    #region Method
    private Dictionary<int, LEGO> FromList(List<LEGO> legoList)
    {
        Dictionary<int, LEGO> legoDict = new Dictionary<int, LEGO>();
        foreach (var (value, index) in legoList.Select((value, index) => (value, index)))
        {
            legoDict.Add(index, value);
        }
        return legoDict;
    }

    private int[,,] Build(Dictionary<int, LEGO> legoDict)
    {
        int[,,] assembly = Initialize();

        foreach (KeyValuePair<int, LEGO> kvp in legoDict)
        {
            int index = kvp.Key;
            LEGO lego = kvp.Value;

            assembly = UpdateAssembly(assembly, lego, index);
        }
        return assembly;
    }

    private int[,,] Initialize()
    {
        int[,,] assembly = new int[MAX_POCCHI_XY, MAX_POCCHI_XY, MAX_POCCHI_Z];

        for (int i = 0; i < assembly.GetLength(0); i++)
        {
            for (int j = 0; j < assembly.GetLength(1); j++)
            {
                for (int k = 0; k < assembly.GetLength(2); k++)
                {
                    assembly[i, j, k] = -1;
                }
            }
        }
        return assembly;
    }

    private int[,,] UpdateAssembly(int [,,] assembly, LEGO lego, int index)
    {
        var pocchiPosition = lego.Pocchi.position;
        var pocchiSize = lego.Pocchi.Size;

        for (int x = (int)pocchiPosition.x; x <= pocchiSize.x; x++)
        {
            for (int y = (int)pocchiPosition.y; y <= pocchiSize.y; y++)
            {
                assembly[x, y, (int)pocchiPosition.z] = index;
            }
        }
        return assembly;
    }
    #endregion
}
