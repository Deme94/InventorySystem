using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceItem", menuName = "ScriptableObjects/Item/Resource", order = 1)]
public class ResourceItem : Item
{
    [SerializeField] private ResourceType _type;
    public ResourceType Type { get { return _type; } }
}
public enum ResourceType
{
    Wood,
    Stone,
}
