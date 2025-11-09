using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _camera;
    [SerializeField] Transform _target;
    [SerializeField] float _speed;

    float _heightThresold = 3f;

    void Start()
    {
        StartCoroutine(MoveUp());
    }

    IEnumerator MoveUp()
    {
        while (true)
        {
            yield return null;

            if (_target.position.y - _camera.position.y <= _heightThresold)
                continue;

            float _y = Mathf.Lerp(_camera.position.y, _target.position.y, Time.deltaTime * _speed);
            _camera.position = new(_camera.position.x, _y, _camera.position.z);
        }
    }
}
