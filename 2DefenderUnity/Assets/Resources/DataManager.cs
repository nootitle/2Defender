using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    void Start()
    {
        List<Dictionary<string, object>> DataList = new List<Dictionary<string, object>>();
        DataList = CSVReader.Read("Data"); //Resource 폴더 외의 경우, 확장자를 써야함

        for(int i = 0; i < DataList.Count; ++i)
        {
            Debug.Log(DataList[i]["aa"] + " " + DataList[i]["ee"]);
        }
    }
}
