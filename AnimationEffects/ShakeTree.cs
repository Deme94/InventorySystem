using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTree : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _speed;
    [SerializeField] private float _intensity;

    private float _enableTime;
    private float _timePassed;

    void OnEnable()
    {
        _enableTime = Time.time;
    }

    void OnDisable()
    {
        transform.eulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        _timePassed = Time.time - _enableTime;
        if(_timePassed > _duration)
        {
            enabled = false;
            return;
        }
        transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(_timePassed * _speed) * _intensity);
    }

    public void Restart()
    {
        transform.eulerAngles = Vector3.zero;
        _enableTime = Time.time;
    }
}
