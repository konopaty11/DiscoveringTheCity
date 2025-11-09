using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    [SerializeField] GameObject _platformPrefab;
    [SerializeField] Transform _target;

    List<GameObject> _platforms;
    Transform _prefPlatform;

    SpriteRenderer _spriteRenderer;

    float _minDistance = 100f;
    float _currentHeight = 0f;
    float _sideOffset;
    float _minHeightStep = 2f;
    float _maxHeightStep = 3.5f;
    float _screenWidthWorld;

    void Start()
    {
        _platforms = new();
        _spriteRenderer = _platformPrefab.GetComponent<SpriteRenderer>();
        _sideOffset = _spriteRenderer.bounds.size.x / 2;
        _screenWidthWorld = Camera.main.orthographicSize * 2f * ((float)Screen.width / Screen.height);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return null;

            if (_prefPlatform != null && _prefPlatform.position.y - _target.position.y >= _minDistance)
                continue;

            Vector3 _position = new(Random.Range(_sideOffset - _screenWidthWorld / 2, _screenWidthWorld / 2 - _sideOffset), _currentHeight, 0f);
            //Debug.Log(_position);
            GameObject _platform = Instantiate(_platformPrefab, _position, Quaternion.identity);
            _currentHeight += Random.Range(_minHeightStep, _maxHeightStep);
            _platforms.Add(_platform);
            _prefPlatform = _platform.transform;
        }
    }
}
