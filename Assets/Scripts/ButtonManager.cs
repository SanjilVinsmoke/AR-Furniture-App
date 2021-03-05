using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ButtonManager : MonoBehaviour
{
    private Button _button;
    
    [SerializeField]private RawImage _buttonImage;
    [SerializeField]private float _scale=2;
    [SerializeField]private float _duration=.2f;
    

    private int _itemId;

    private Sprite _buttonSprite;

    public int ItemId
    {
        set => _itemId = value;
    }

    public Sprite _ButtonSprite
    {
        set
        {
            _buttonSprite = value;
            _buttonImage.texture = _buttonSprite.texture;

        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SelectObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.OnEntered(gameObject))

        {
            transform.DOScale(Vector3.one * _scale,_duration);
            //transform.localScale = Vector3.one * 2;

        }
        else
        {
            transform.DOScale(Vector3.one, _duration);
            // transform.localScale=Vector3.one;
        }
    }

    void SelectObject()
    {
        DataContainer.Instance.SetFurniture(_itemId);
    }
}
