using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmResource : MonoBehaviour
{
    [SerializeField] private ResourceItem _item;
    [SerializeField] private int _ticks;
    private int _ticksLeft;
    [SerializeField] private int _respawnTime;
    public ResourceItem Item {
        get { return _item; }
        private set { _item = value; }
    }
    [SerializeField] private int _tier;

    [SerializeField] private MonoBehaviour _shakeAnimation;
    [SerializeField] private GameObject _destroyPrefab;

    [SerializeField] private AudioSource _farmAudio;
    [SerializeField] private AudioSource _destroyAudio;

    private void Awake()
    {
        _ticksLeft = _ticks;
    }
    public int Farm(int toolTier)
    {
        _farmAudio.Play();
        _shakeAnimation.enabled = true;

        _ticksLeft -= toolTier;
        if (_ticksLeft <= 0) DestroyResource();

        return GetRandomAmount(toolTier);
    }

    private int GetRandomAmount(int toolTier)
    {
        int maxAmount = toolTier / _tier;
        int minAmount = maxAmount - 2;
        if (minAmount < 0) minAmount = 0;
        return Random.Range(minAmount, maxAmount);
    }

    private void DestroyResource()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        _destroyPrefab.SetActive(true);
        _destroyAudio.Play();
        Invoke("Respawn", _respawnTime);
    }

    private void Respawn()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        _ticksLeft = _ticks;
    }
}
