using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceItem", menuName = "ScriptableObjects/Item/MeleeWeapon", order = 1)]
public class MeleeWeaponItem : ActionItem
{
    [SerializeField] private int _tier;
    [SerializeField] private int _damage;
    [SerializeField] private float _range;
    [SerializeField] private Vector2 _areaOfEffect;
    [Tooltip("Time (seconds) to wait before the attack animation has finished after hit")]
    [SerializeField] private float _attackAnimationDuration;
    [Tooltip("Time (seconds) to synchronize attack animation and hit detection")]
    [SerializeField] private float _waitBeforeHit;
    [SerializeField] private string _weaponAnimatorParameter;
    private int _weaponAnimatorParameterId;
    private int _fireAnimatorParameterId;
    [SerializeField] private LayerMask _layerMask;

    void OnValidate()
    {
        _weaponAnimatorParameterId = Animator.StringToHash(_weaponAnimatorParameter);
        _fireAnimatorParameterId = Animator.StringToHash("Fire");
    }

    public override void Equip(GameObject player)
    {
        player.GetComponent<Animator>().SetBool(_weaponAnimatorParameterId, true);
    }
    public override void Unequip(GameObject player)
    {
        player.GetComponent<Animator>().SetBool(_weaponAnimatorParameterId, false);
    }
    public override void TriggerAction(GameObject player)
    {
        var playerActions = player.GetComponent<PlayerActions>();
        if (playerActions.IsExecutingAction) return;
        playerActions.StartCoroutine(Attack(player));
    }

    private IEnumerator Attack(GameObject player)
    {
        var playerActions = player.GetComponent<PlayerActions>();
        var animator = player.GetComponent<Animator>();

        playerActions.IsExecutingAction = true;
        animator.SetBool(_fireAnimatorParameterId, true);
        do
        {
            yield return new WaitForSeconds(_waitBeforeHit);
            Hit(player);
            yield return new WaitForSeconds(_attackAnimationDuration - _waitBeforeHit);
        } while (playerActions.IsTriggeringAction);
        playerActions.IsExecutingAction = false;
        animator.SetBool(_fireAnimatorParameterId, false);
    }

    private void Hit(GameObject player)
    {
        var playerLookDirection = player.GetComponent<PlayerMovement>().LookDirection();

        var hit = Physics2D.BoxCast(player.transform.position.AsVector2() + (Vector2.up * 0.4f) + playerLookDirection, _areaOfEffect, 0, Vector2.zero, _range, _layerMask);
        if (hit)
        {
            if (hit.collider.CompareTag("Player"))
            {
                // Dañar al jugador con damage
            }
            else
            {
                var resource = hit.collider.GetComponent<FarmResource>();
                var quantity = resource.Farm(_tier);
                player.GetComponentInChildren<Inventory>().Add(resource.Item, quantity);
            }
        }
    }
    //static public RaycastHit2D BoxCast(Vector2 origen, Vector2 size, float angle, Vector2 direction, float distance, int mask)
    //{
    //    RaycastHit2D hit = Physics2D.BoxCast(origen, size, angle, direction, distance, mask);

    //    //Setting up the points to draw the cast
    //    Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
    //    float w = size.x * 0.5f;
    //    float h = size.y * 0.5f;
    //    p1 = new Vector2(-w, h);
    //    p2 = new Vector2(w, h);
    //    p3 = new Vector2(w, -h);
    //    p4 = new Vector2(-w, -h);

    //    Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
    //    p1 = q * p1;
    //    p2 = q * p2;
    //    p3 = q * p3;
    //    p4 = q * p4;

    //    p1 += origen;
    //    p2 += origen;
    //    p3 += origen;
    //    p4 += origen;

    //    Vector2 realDistance = direction.normalized * distance;
    //    p5 = p1 + realDistance;
    //    p6 = p2 + realDistance;
    //    p7 = p3 + realDistance;
    //    p8 = p4 + realDistance;


    //    //Drawing the cast
    //    Color castColor = hit ? Color.red : Color.green;
    //    Debug.DrawLine(p1, p2, castColor);
    //    Debug.DrawLine(p2, p3, castColor);
    //    Debug.DrawLine(p3, p4, castColor);
    //    Debug.DrawLine(p4, p1, castColor);

    //    Debug.DrawLine(p5, p6, castColor);
    //    Debug.DrawLine(p6, p7, castColor);
    //    Debug.DrawLine(p7, p8, castColor);
    //    Debug.DrawLine(p8, p5, castColor);

    //    Debug.DrawLine(p1, p5, Color.grey);
    //    Debug.DrawLine(p2, p6, Color.grey);
    //    Debug.DrawLine(p3, p7, Color.grey);
    //    Debug.DrawLine(p4, p8, Color.grey);
    //    if (hit)
    //    {
    //        Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
    //    }

    //    return hit;
    //}
}
