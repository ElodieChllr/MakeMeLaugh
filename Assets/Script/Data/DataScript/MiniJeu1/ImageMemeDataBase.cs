using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDataBase", menuName = "DataBase/ImageMemeData", order = 2)]
public class ImageMemeDataBase : ScriptableObject
{
    public List<ImageMemeData> datas = new();
}
