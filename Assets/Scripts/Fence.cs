using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Fence : MonoBehaviour
{
    [SerializeField] private UnityEvent _activated;
    [SerializeField] private UnityEvent _deactivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _activated.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _deactivated.Invoke();
    }
}
