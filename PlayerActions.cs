using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    // Input Manager
    private InputManager _inputs;
    // ActionBar
    [SerializeField] private ActionBar _actionBar;

    public System.Action<GameObject> TriggerAction;
    public bool IsTriggeringAction { get; private set; }
    public bool IsExecutingAction { get; set; }

    void Start()
    {
        _inputs = FindObjectOfType<InputManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsTriggeringAction && _inputs.Fire)
        {
            IsTriggeringAction = true;
            TriggerAction.Invoke(gameObject);
        }
        else if(IsTriggeringAction && !_inputs.Fire)
        {
            IsTriggeringAction = false;
        }
    }
}
