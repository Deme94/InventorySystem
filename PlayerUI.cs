using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    // Inventory
    [SerializeField] private Transform _inventory;

    // Input Manager
    private InputManager _inputs;

    // UI Flags
    private bool _inventoryFlag;

    void Start()
    {
        _inputs = FindObjectOfType<InputManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_inputs.Inventory)
        {
            if (_inventoryFlag) _inventory.localScale = new Vector3(0, 0, 0);
            else _inventory.localScale = new Vector3(1, 1, 1);
            _inventoryFlag = !_inventoryFlag;
        }
    }
}
