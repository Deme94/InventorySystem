using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour, IItemContainer
{
    // Player
    [SerializeField] private GameObject _player;
    // Input Manager
    private InputManager _inputs;
    // Drag and Drop
    [SerializeField] private DragAndDrop _dragAndDrop;

    private int _selectedSlot = -1; // Default = -1 (Nota: representa el index del array, no el número de botón)
    [SerializeField] private Image[] _itemIcons;
    [SerializeField] private ActionItem _defaultActionItem;
    private ActionItem[] _items = new ActionItem[10];

    void Awake()
    {
        _inputs = FindObjectOfType<InputManager>();
    }

    void Start()
    {
        SelectSlot(-1);
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputs.Equip1) SelectSlot(1);
        else if (_inputs.Equip2) SelectSlot(2);
        else if (_inputs.Equip3) SelectSlot(3);
    }

    private void SelectSlot(int slot)
    {
        if(_selectedSlot == slot)
        {
            _selectedSlot = -1;
            _defaultActionItem.Equip(_player);
            _player.GetComponent<PlayerActions>().TriggerAction = _defaultActionItem.TriggerAction;
            // Deselect slot UI
        }
        else
        {
            _selectedSlot = slot;
            _items[_selectedSlot].Equip(_player);
            _player.GetComponent<PlayerActions>().TriggerAction = _items[_selectedSlot].TriggerAction;
            // Select slot UI
        }
    }

    public void Drag(int slot)
    {
        if (_items[slot] == null) return;

        _dragAndDrop.Drag(_items[slot], 1, this, slot);
        _items[slot] = null;
        _itemIcons[slot].sprite = null;
        _itemIcons[slot].enabled = false;
    }

    public void Drop(int slot)
    {
        if (_dragAndDrop.DraggedItem == null) return;

        ActionItem item;
        try
        {
            item = (ActionItem)_dragAndDrop.DraggedItem;
        }
        catch (System.InvalidCastException) { 
            _dragAndDrop.Drop(false);
            return;
        }

        if (_items[slot] != null)
        {
            // 1. Guardamos el item que ocupa el slot en una variable temporal
            ActionItem oldItem = _items[slot];
            // 2. Registramos el origen del drop en una variable temporal
            IItemContainer source = _dragAndDrop.Source;
            int sourceSlot = _dragAndDrop.SourceSlot;
            // 3. El item que hemos sustituido lo guardamos en el origen del slot del drop (tenemos que hacer otro drag and drop)
            _dragAndDrop.Drag(oldItem, 1, this, slot);
            source.Drop(sourceSlot);
        }

        // Guardamos el item dropeado exitosamente
        _items[slot] = item;
        _itemIcons[slot].sprite = _items[slot].Sprite;
        _itemIcons[slot].enabled = true;
        _dragAndDrop.Drop(true);
    }
}
