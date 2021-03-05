using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjects : MonoBehaviour
{
    [SerializeField]
    private bool IsSelected;

    [SerializeField]
    private bool IsLocked;

    public bool Selected 
    { 
        get 
        {
            return this.IsSelected;
        }
        set 
        {
            IsSelected = value;
        }
    }

    public bool Locked 
    { 
        get 
        {
            return this.IsLocked;
        }
        set 
        {
            IsLocked = value;
        }
    }

}
