using System.Collections.Generic;
using UnityEngine;

public class MarkController : MonoBehaviour
{
    private Dictionary<string, MarkData> _markDictionary = new();
    

    void Awake()
    {
        GameObject marksObject = GameObject.Find("PositionMarks");
        var marks = marksObject.GetComponentsInChildren<MarkData>();

        foreach( var mark in marks)
        {
            _markDictionary.Add(mark.ID, mark);
        }
    }

    public void ObjectToMark(Transform objectTransform, string id)
    {
        _markDictionary[id].SetObjectToMe(objectTransform);
    }

    
}
