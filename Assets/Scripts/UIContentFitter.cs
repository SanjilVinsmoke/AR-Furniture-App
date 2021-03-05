using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContentFitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HorizontalOrVerticalLayoutGroup hg = GetComponent<HorizontalOrVerticalLayoutGroup>();
        int childCount = transform.childCount - 1;
        float height = transform.GetComponent<RectTransform>().rect.height;
        float childWidth = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        float width = hg.spacing * childCount + childCount * childWidth + hg.padding.left;

        GetComponent<RectTransform>().sizeDelta= new Vector2(width,170);
        
    }
    
}
