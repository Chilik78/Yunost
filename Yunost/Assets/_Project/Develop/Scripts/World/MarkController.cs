using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.U2D.ScriptablePacker;

public class MarkController : MonoBehaviour
{
    private Dictionary<string, Transform> _markDictionary = new();
    

    void Awake()
    {
        GameObject marksObject = GameObject.Find("PositionMarks");
        var marks = marksObject.GetComponentsInChildren<Transform>();

        foreach( var mark in marks)
        {
            _markDictionary.Add(mark.name, mark);
        }
    }

    public void ObjectToMark(Transform objectTransform, string id)
    {
        SetObjectToMe(objectTransform, _markDictionary[id]);
    }

    public void SetObjectToMe(Transform objectTransform, Transform mark)
    {
        objectTransform.position = mark.transform.position;
    }
}
