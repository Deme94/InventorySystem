using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Items que se pueden colocar en el inventario, pero tambien en la barra de accion
public abstract class ActionItem : Item
{
    public abstract void Equip(GameObject player);
    public abstract void Unequip(GameObject player);
    public abstract void TriggerAction(GameObject player);
}

