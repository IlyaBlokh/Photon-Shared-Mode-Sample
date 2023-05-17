using Fusion;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _playerSpeed = 2f;
    private CharacterController _controller;
    private Camera _camera;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public override void Spawned()
    {
        _camera = Camera.main;
        Debug.Assert(_camera != null, nameof(_camera) + " != null");
        _camera.GetComponent<FirstPersonCamera>().Target = GetComponent<NetworkTransform>().InterpolationTarget;
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
            return;

        Quaternion cameraRotationY = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);
        Vector3 move = cameraRotationY * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * _playerSpeed;
        _controller.Move(move);

        if (move != Vector3.zero) 
            gameObject.transform.forward = move;

    }
}