using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.U2D.ScriptablePacker;

public class MarkController : MonoBehaviour
{
    private static MarkController _instance;
    private Dictionary<string, Transform> _markDictionary = new();

    public static MarkController GetInstance() => _instance;


    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Íà ñöåíå áîëüøå îäíîãî äèàëîãà");
        }
        _instance = this;


        GameObject marksObject = GameObject.Find("PositionMarks");
        var marks = marksObject.GetComponentsInChildren<Transform>();

        foreach( var mark in marks)
        {
            _markDictionary.Add(mark.name, mark);
        }
        
    }

    public void ObjectToMark(Transform objectTransform, string id)
    {
        _setObjectToMe(objectTransform, _markDictionary[id]);
    }

    private void _setObjectToMe(Transform objectTransform, Transform mark)
    {
        objectTransform.position = mark.transform.position;
    }
}
