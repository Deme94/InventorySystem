using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour, IItemContainer
{
    private int _size = 20;
    private int _index = 0;
    [SerializeField] private Image[] _itemIcons;
    [SerializeField] private Text[] _itemIconQuantities;
    private InventoryItem[] _items = new InventoryItem[20]; // Access index = row*width + column (borrar si no es necesario)
    // Resources
    [SerializeField] private int _wood;
    public int Wood
    {
        get { return _wood; }
        private set { _wood = value; }
    }
    // Drag and drop
    [SerializeField] private DragAndDrop _dragAndDrop;

    public bool Add(Item item)
    {
        return Add(item, 1, _index);
    }
    public bool Add(Item item, int quantity)
    {
        return Add(item, quantity, _index);
    }
    public bool Add(Item item, int quantity, int slot)
    {
        // El inventario esta lleno, no se añade
        if (item == null || !FitsOn(item)) return false;

        InventoryItem inventoryItem;
        if (item.IsStackable && (inventoryItem = GetFirstStackable(item)) != null)
        {
            // Se apila en un slot existente
            inventoryItem.Quantity++;
            // Actualiza la cantidad del item en la UI (El texto es el primer hijo child(0) del itemIcon)
            _itemIconQuantities[inventoryItem.Index].text = inventoryItem.Quantity.ToString();
        }
        else
        {
            // No se apila y se añade a un slot
            if (_items[slot] != null)
            {
                // 1. Guardamos el item que ocupa el slot en una variable temporal
                InventoryItem oldItem = _items[slot];
                // 2. El item que hemos sustituido lo guardamos en el ultimo slot libre
                _items[_index] = oldItem;
                _itemIcons[_index].sprite = oldItem.Item.Sprite;
                if(oldItem.Quantity > 1)
                    _itemIconQuantities[_index].text = oldItem.Quantity.ToString();
            }

            // 3. Asignamos el nuevo item al slot deseado
            _items[slot] = new InventoryItem(item, quantity, slot);
            _itemIcons[slot].sprite = item.Sprite;
            if (quantity > 1)
                _itemIconQuantities[slot].text = quantity.ToString();
            else
                _itemIconQuantities[slot].text = "";

            // Una vez añadido, el _index se va al primer hueco que encuentre libre
            _index = 0;
            while (_index < _size && _items[_index] != null)
                _index++;
        }

        try
        {
            AddResource(((ResourceItem) item).Type);
        }
        catch (System.Exception) { }
        return true;
    }

    private void RemoveAt(int slot)
    {
        _items[slot] = null;
        _itemIcons[slot].sprite = null;
        _itemIconQuantities[slot].text = "";
        _index = slot;
    }

    private void AddResource(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Wood: _wood++; break;
            case ResourceType.Stone: break;
        }
    }
    private bool FitsOn(Item item)
    {
        if (_index >= _size)
        {
            if (!item.IsStackable || GetFirstStackable(item) == null)
                return false;
        }

        return true;
    }

    // Obtiene el primer item del inventario del mismo tipo que "item" que aun pueda apilarse
    private InventoryItem GetFirstStackable(Item item)
    {
        return _items.Where(i => i != null && i.Item.Equals(item)).Where(i => i.Quantity < 100).FirstOrDefault();
    }

    public void Drag(int slot)
    {
        if (_items[slot] == null) return;

        _dragAndDrop.Drag(_items[slot].Item, _items[slot].Quantity, this, slot);
        RemoveAt(slot);
    }

    public void Drop(int slot)
    {
        if (_dragAndDrop.DraggedItem == null) return;

        _dragAndDrop.Drop(Add(_dragAndDrop.DraggedItem, _dragAndDrop.Quantity, slot));
    }

    // Una clase exclusiva del inventario para almacenar no solo el item, si no tambien una cantidad asociada
    private class InventoryItem
    {
        public Item Item;
        public int Quantity { get; set; }
        public int Index { get; private set; }

        public InventoryItem(Item item, int quantity, int index)
        {
            Item = item;
            Quantity = quantity;
            Index = index;
        }
    }
}
