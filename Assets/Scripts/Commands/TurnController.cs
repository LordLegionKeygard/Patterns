using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField] private ActionRecorder _actionRecorder;
    [SerializeField] private Unit _playerUnit;

    private void OnMove()
    {
        var vector = value.Get<Vector2>();
        var direction = Direction.FromVector2(vector);
        var action = new MoveAction(_playerUnit,direction);
        _actionRecorder.Record(action);
    }

    private void OnJump()
    {
        var action = new JumpAction(_playerUnit);
        _actionRecorder.Record(action);
    }

    private void OnSpin()
    {
        var action = new SpinAction(_playerUnit);
        _actionRecorder.Record(action);
    }
}
