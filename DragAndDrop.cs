using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DragAndDrop : MonoBehaviour
{
    public Item DraggedItem { get; private set; }
    public int Quantity { get; private set; }
    private Image _draggedIcon;

    public IItemContainer Source { get; private set; }
    public int SourceSlot { get; private set; }

    private Vector3 _mousePosition;

    void Awake()
    {
        _draggedIcon = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        if(DraggedItem != null)
        {
            _mousePosition = Input.mousePosition;
            //_mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
            _draggedIcon.transform.position = _mousePosition;
        }
    }

    public void Drag(Item item, int quantity, IItemContainer source, int sourceSlot)
    {
        Source = source;
        SourceSlot = sourceSlot;
        DraggedItem = item;
        Quantity = quantity;
        _draggedIcon.sprite = DraggedItem.Sprite;
        _draggedIcon.enabled = true;
    }

    public void Drop(bool success)
    {
        if (!success && Source != null)
        {
            Source.Drop(SourceSlot);
            Source = null;
        }

        DraggedItem = null;
        _draggedIcon.sprite = null;
        _draggedIcon.enabled = false;
    }
}
