using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GraphicRaycaster _raycaster;
    private PointerEventData _pData;
    private EventSystem _eventSystem;


    public Transform selectionPoint;
    
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }

            return _instance;
        }
    }

    void Start()
    {
        _raycaster = GetComponent<GraphicRaycaster>();
        _eventSystem = GetComponent<EventSystem>();
        _pData= new  PointerEventData(_eventSystem);

        _pData.position = selectionPoint.position;
        
    }
    

    public bool OnEntered(GameObject button)
    {
        List<RaycastResult> results= new List<RaycastResult>();
        _raycaster.Raycast(_pData,results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject==button)
            {

                return true;
                
            }
            
        }
        return false;
    }
}