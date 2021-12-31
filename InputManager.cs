using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float InputV { get; private set; }
    public float InputH { get; private set; }

    public bool Fire { get; private set; }

    public bool Equip1 { get; private set; }
    public bool Equip2 { get; private set; }
    public bool Equip3 { get; private set; }

    public bool Inventory { get; private set; }

    public MeleeWeaponItem StoneHatchet; // BORRAR TEMPORAL

    // Update is called once per frame
    void Update()
    {
        InputV = Input.GetAxisRaw("Vertical");
        InputH = Input.GetAxisRaw("Horizontal");

        Fire = Input.GetMouseButton(0);

        Equip1 = Input.GetKeyDown(KeyCode.Alpha1);
        Equip2 = Input.GetKeyDown(KeyCode.Alpha2);
        Equip3 = Input.GetKeyDown(KeyCode.Alpha3);

        Inventory = Input.GetKeyDown(KeyCode.I);

        if (Input.GetKeyDown(KeyCode.P)) FindObjectOfType<Inventory>().Add(StoneHatchet); // BORRAR TEMPORAL
    }
}
