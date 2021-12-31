using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemContainer
{
    public abstract void Drag(int slot);
    public abstract void Drop(int slot);
}
