using UnityEngine;

public class Character : Singleton<Character>
{
    public float Speed = 0.3f;
    private CharacterController _controller;

    public bool HasMoved { get; private set; }

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (InteractibleUIController.Instance.IsInteracting)
        {
            return;
        }
        HasMoved = false;
        Vector3 move = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
        _controller.Move(move * Time.deltaTime * Speed);
        if (move != Vector3.zero)
        {
            transform.forward = move;
            HasMoved = true;
        }
    }

}