using System.Collections;
using System.Collections.Generic;

using Unity.XR.CoreUtils;

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class PlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject PlacedPrefab;
    public GameObject placedPrefab
    {
        get
        {
            return PlacedPrefab;
        }
        set 
        { 
            PlacedPrefab = value; 
        }
    }
    private ARRaycastManager arRayCastManager;
    private void Awake()
    {
        arRayCastManager= GetComponent<ARRaycastManager>();
    }
    bool tryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount>0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
        void Update()
        {
            if(!tryGetTouchPosition(out Vector2 touchPOsition))
            {
                return;
                
            }
        if (arRayCastManager.Raycast(touchPOsition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitpos = hits[0].pose;
            Instantiate(placedPrefab, hitpos.position, hitpos.rotation);
        }
        }
    static List<ARRaycastHit> hits= new List<ARRaycastHit>();
}
