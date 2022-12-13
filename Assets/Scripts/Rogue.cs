using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Rogue : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastPosition;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _lastPosition = gameObject.transform.position;
    }

    private void Update()
    {
        if ((transform.position - _lastPosition).normalized == Vector3.left)
            _spriteRenderer.flipX = true;
        else if ((transform.position - _lastPosition).normalized == Vector3.right)
            _spriteRenderer.flipX = false;

        _lastPosition = transform.position;
    }
}
