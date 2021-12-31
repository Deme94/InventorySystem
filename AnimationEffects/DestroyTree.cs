using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTree : MonoBehaviour
{
    [SerializeField] private int _duration;
    [SerializeField] private float _intensity;

    private float _x;
    private float _y;

    private float _enableTime;
    private bool _isFalling;

    void OnEnable()
    {
        _x = 0;
        _enableTime = Time.time;
        _isFalling = true;
        transform.eulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(_x > 1)
        {
            if (_isFalling)
            {
                Invoke("Disable", 2f);
                _isFalling = false;
            }
            return;
        }

        _x = (Time.time - _enableTime) / _duration;
        _y = EaseInOutElastic(_x);
        transform.eulerAngles = new Vector3(0, 0, _y*_intensity);
    }

    // Funcion matematica para la caida del arbol
    private float EaseInOutElastic(float x) {
        const float c5 = (2 * Mathf.PI) / 4.5f;

        return x == 0
            ? 0
            : x == 1
            ? 1
            : x< 0.5
            ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2
            : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2 + 1;
    }

    // No borrar, se utiliza en el Invoke() del Update()
    private void Disable() => gameObject.SetActive(false);
}
