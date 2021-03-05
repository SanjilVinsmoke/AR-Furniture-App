using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.XR.ARFoundation;
using  UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ARObjectPlacement : MonoBehaviour
{
  
    private GameObject _objectToBeCreated;
    private InputManager _inputManager;
    [SerializeField] private ARRaycastManager _arRaycastManager;
    static List<ARRaycastHit> hits= new List<ARRaycastHit>();
    
    

    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _inputManager = GetComponent<InputManager>();

    }
    
    
   
    // Update is called once per frame
    void Update()
    {
        if(!_inputManager.TryGetTouchPosition(out Vector2 touchPosition))
            return;
        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPos = hits[0].pose;
            if (_objectToBeCreated == null)
            {
             _objectToBeCreated= Instantiate<GameObject>(_inputManager.PlacedPrefabs, hitPos.position, hitPos.rotation);
            }
            else
            {
                _objectToBeCreated.transform.SetPositionAndRotation(hitPos.position,hitPos.rotation);
            }
           
            
        }
    }
   
}
