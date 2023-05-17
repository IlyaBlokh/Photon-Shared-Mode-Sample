using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _playerSpeed = 2f;
    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
            return;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * _playerSpeed;
        _controller.Move(move);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

    }
}