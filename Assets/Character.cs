using UnityEngine;

public class Character : MonoBehaviour
{
    public float Speed = 0.3f;
    private CharacterController _controller;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
        _controller.Move(move * Time.deltaTime * Speed);
    }

}