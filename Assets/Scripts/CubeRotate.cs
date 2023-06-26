using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    private float _angleSpeed = 20;
    private Vector3 _axis = Vector3.up;
    private Transform _transform;

    private void Awake()
    {
        // キャッシュ
        _transform = transform;
    }

    // 回転処理
    private void Update()
    {
        var angle = _angleSpeed * Time.deltaTime;

        _transform.rotation = Quaternion.AngleAxis(angle, _axis) * _transform.rotation;
    }
}