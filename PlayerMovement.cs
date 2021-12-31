using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Input Manager
    private InputManager _inputs;

    // Movement attributes
    private float _lastMoveV;
    private float _lastMoveH;
    private Vector3 _direction;
    [SerializeField] private float _speed;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; _animator.SetFloat(_speedId, _speed * 0.5f); }
    }
    private bool _canMove = true;
    
    // Animator attributes
    private Animator _animator;
    private int _inputVId;
    private int _inputHId;
    private int _lastMoveVId;
    private int _lastMoveHId;
    private int _speedId;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _inputVId = Animator.StringToHash("InputV");
        _inputHId = Animator.StringToHash("InputH");
        _lastMoveVId = Animator.StringToHash("LastMoveV");
        _lastMoveHId = Animator.StringToHash("LastMoveH");
        _speedId = Animator.StringToHash("Speed");
    }
    // Start is called before the first frame update
    void Start()
    {
        _inputs = FindObjectOfType<InputManager>();
        _animator.SetFloat(_speedId, _speed * 0.5f);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_canMove) { 
            _direction = new Vector3(_inputs.InputH, _inputs.InputV, 0).normalized;

            if (_direction.sqrMagnitude != 0) {
                if (_lastMoveH != _inputs.InputH) 
            {
                    _lastMoveH = _inputs.InputH;
                    _animator.SetFloat(_lastMoveHId, _lastMoveH);
                }
                if (_lastMoveV != _inputs.InputV)
                {
                    _lastMoveV = _inputs.InputV;
                    _animator.SetFloat(_lastMoveVId, _lastMoveV);
                }
            }

            // Move
            transform.Translate(_direction * _speed * Time.deltaTime);
            _animator.SetFloat(_inputVId, _inputs.InputV);
            _animator.SetFloat(_inputHId, _inputs.InputH);
        }
    }

    public void StopMove()
    {
        _canMove = false;
    }
    public void AllowMove()
    {
        _canMove = true;
    }

    public Vector2 LookDirection()
    {
        if (_lastMoveV < 0) return new Vector2(0, -0.45f);
        if (_lastMoveV > 0) return new Vector2(0, 0.001f);
        if (_lastMoveH < 0) return new Vector2(-0.3f, 0);
        if (_lastMoveH > 0) return new Vector2(0.3f, 0);
        else return new Vector2(0, -0.45f);
    }
}
