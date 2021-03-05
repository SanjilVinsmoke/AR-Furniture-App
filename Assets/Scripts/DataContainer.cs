using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class DataContainer : MonoBehaviour
{
   
    public GameObject Furniture;

    [SerializeField] private ButtonManager _buttonPrefab;
    [SerializeField] private GameObject _buttonContainer;
    [SerializeField] private List<Item> _items;

    private int _current_id = 0;
    
    private static DataContainer _instance;
    public static DataContainer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataContainer>();
            }

            return _instance;
        }
    }

    void Start()
    {
        LoadItems();
        CreateButton();
    }
    void LoadItems()
    {
        var items_obj = Resources.LoadAll("Items", typeof(Item));
        foreach (var item in items_obj)
        {
            _items.Add(item as Item);
        }
    }
    void CreateButton()
    {
        foreach (Item i in _items)
        {
            ButtonManager b = Instantiate(_buttonPrefab, _buttonContainer.transform);
            b.ItemId = _current_id;
            b._ButtonSprite = i.itemImage;
            _current_id++;
            
        }
    }

    public void SetFurniture(int id)
    {
        Furniture = _items[id].itemPrefab;
    }
}
