using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    // Start is called before the first frame update
    public float axis = 0.0f;
    public float speed = 1f;

    public Transform ballTransform;

    private Rigidbody2D _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (tag == "AI")
        {
            switch (GameInfo.gameMode)
            {
                case GameMode.ComputerEasy:
                    axis = Mathf.Clamp(ballTransform.position.x - transform.position.x, -1, 1);
                    break;
                case GameMode.ComputerMedium:
                    axis = Mathf.Clamp(ballTransform.position.x - transform.position.x, -1, 1) * 1.5f;
                    break;
                case GameMode.ComputerHard:
                default:
                    axis = Mathf.Clamp(ballTransform.position.x - transform.position.x, -1, 1) * 2f;
                    break;
            }

        }
        else
        {
            axis = 0;
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; ++i)
                {
                    Vector2 inputPosition = PositionRelativeToCamera(Input.GetTouch(i).position);
                    if (tag == "Player1" && inputPosition.y < 0.5f)
                    {
                        axis = inputPosition.x > 0.5f ? 1 : -1;
                    }
                    else if (tag == "Player2" && inputPosition.y > 0.5f)
                    {
                        axis = inputPosition.x > 0.5f ? 1 : -1;
                    }
                }

            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 inputPosition = PositionRelativeToCamera(Input.mousePosition);
                if (tag == "Player1" && inputPosition.y < 0.5f)
                {
                    axis = inputPosition.x > 0.5f ? 1 : -1;
                }
                else if (tag == "Player2" && inputPosition.y > 0.5f)
                {
                    axis = inputPosition.x > 0.5f ? 1 : -1;
                }
            }
            else
            {
                axis = Input.GetAxis(tag == "Player1" ? "Horizontal1" : "Horizontal2");

            }
        }
    }

    Vector2 PositionRelativeToCamera(Vector2 position)
    {
        return new Vector2(position.x / Camera.main.pixelWidth,
            position.y / Camera.main.pixelHeight);
    }


    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + Vector2.right * axis * speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
