using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartDlgText : MonoBehaviour
{
    public GameObject textObject;
    public float activationDistance = 5f;
    public Transform player;
    private bool isTextActive = false;


    void Start()
    {
        if (textObject != null)
        {
            textObject.SetActive(false);
        }

        player = GameObject.Find("Peasant Nolant(Free Version)").transform;
    }

    private void Update()
    {
        if (player != null && textObject != null)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            if (distance <= activationDistance && !isTextActive)
            {
                textObject.SetActive(true);
            }
            else if (distance > activationDistance)
            {
                textObject.SetActive(false);
                isTextActive = false;
            }
            
            if (Input.GetKeyDown(KeyCode.E) && textObject.activeSelf)
            {
                textObject.SetActive(false);
                isTextActive = true;
            }
        }
       
    }
}
