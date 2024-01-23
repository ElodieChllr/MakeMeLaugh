using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDataBase", menuName = "DataBase/JoueursData", order = 1)]
public class JoueursDataBase : ScriptableObject
{
   public List<JoueursData> datas = new();
}


