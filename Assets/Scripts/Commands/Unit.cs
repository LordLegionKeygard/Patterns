using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Animator _animator;

    private bool _isActing;

    public void Move(Direction direction)
    {
        if(_isActing) return;
    }

    public void Jump()
    {

    }

    public void Spin()
    {

    }
}
