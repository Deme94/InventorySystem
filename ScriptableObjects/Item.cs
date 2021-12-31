using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public Sprite Sprite;
    [SerializeField] private string _name;
    public string Name { get { return _name; } }
    [SerializeField] private bool _isStackable;
    public bool IsStackable { get { return _isStackable; } }
}
