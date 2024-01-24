using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDataBase", menuName = "DataBase/ThemesData", order = 1)]
public class ThemeActingDataBase : ScriptableObject
{
    public List<ThemeActingData> datas = new();
}
