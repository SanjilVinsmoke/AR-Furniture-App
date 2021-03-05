using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _arCam;
    [SerializeField] private ARRaycastManager _raycastManager;


    private static List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private PlacedObjects[] placedObjects;

    private Vector2 touchPosition = default;
    private bool onTouchHold = false;
    private PlacedObjects lastSelectedObject;


    private Pose _pose;
    private Touch _touch;
    


    void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // if (Input.touchCount > 0)
        // {
        //     _touch = Input.GetTouch(0);
        //     touchPosition = _touch.position;
        //     if (_touch.phase==TouchPhase.Began)
        //     {
        //         Ray ray = _arCam.ScreenPointToRay(touchPosition);
        //         RaycastHit hitObjects;
        //
        //         if (Physics.Raycast(ray, out hitObjects))
        //         {
        //             if (hitObjects.transform.GetComponent<PlacedObjects>())
        //             {
        //                 _onTouchHold = true;
        //             }
        //         }
        //         
        //        
        //         if (_touch.phase == TouchPhase.Ended)
        //         {
        //             _onTouchHold = false;
        //         }
        //
        //         if (_onTouchHold)
        //         {
        //             if(_raycastManager.Raycast(touchPosition, _hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        //             {
        //                 Pose hitPose = _hits[0].pose;
        //                 Instantiate(GameObject, hitPose.position, hitPose.rotation);
        //                 
        //                 // if (placedObjects == null)
        //                 // {
        //                 //     Instantiate(GameObject, hitPose.position, hitPose.rotation);
        //                 // }
        //                 // else
        //                 // {
        //                 //     if (_onTouchHold)
        //                 //     {
        //                 //         placedObjects.transform.position = hitPose.position;
        //                 //         placedObjects.transform.rotation = hitPose.rotation;
        //                 //     }
        //                 // }
        //                
        //             }
        //         }
        //
        //     }
        //     
        //     
        //     



        //
        // if(!TryGetTouchPosition(out Vector2 touchPosition))
        //     return;
        //
        // if(_raycastManager.Raycast(touchPosition, _hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        // {
        //     var hitPose = _hits[0].pose;
        //     //if (IsPointerOverUI(_touch)) return; 
        //     Instantiate(DataContainer.Instance.Furniture, hitPose.position, hitPose.rotation);
        // }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = _arCam.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    lastSelectedObject = hitObject.transform.GetComponent<PlacedObjects>();
                    if (lastSelectedObject != null)
                    {
                        PlacedObjects[] allOtherObjects = FindObjectsOfType<PlacedObjects>();
                        foreach (PlacedObjects placementObject in allOtherObjects)
                        {
                            placementObject.Selected = placementObject == lastSelectedObject;
                        }
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                lastSelectedObject.Selected = false;
            }
        }

        if (_raycastManager.Raycast(touchPosition, _hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = _hits[0].pose;

            if (lastSelectedObject == null)
            {
                lastSelectedObject = Instantiate<GameObject>(DataContainer.Instance.Furniture, hitPose.position, hitPose.rotation)
                    .GetComponent<PlacedObjects>();
            }
            else
            {
                lastSelectedObject.transform.SetPositionAndRotation(hitPose.position,hitPose.rotation);
            }



        }
        



        bool IsPointerOverUI(Touch touch)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = new Vector2(_touch.position.x, _touch.position.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }


    }

    public bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            touchPosition = _touch.position;

            return true;
        }

        touchPosition = default;

        return false;
    
    }
}