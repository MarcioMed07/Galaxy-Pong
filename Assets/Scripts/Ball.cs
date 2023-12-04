using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    public float acceleration = 0.1f;

    public float initialSpeed = 4f;
    public float speed = 5f;

    public GameObject paddle1;
    public GameObject paddle2;
    private Rigidbody2D _rigidBody;
    private TrailRenderer _trailRenderer;
    public ParticleSystem _particleSystem;
    private Light2D _light2d;
    public GameController gameController;
    private AudioSource _beep;
    private AudioSource _boom;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
        _light2d = GetComponent<Light2D>();
        var audios = GetComponents<AudioSource>();
        _beep = audios[0];
        _boom = audios[1];
    }

    public void StopBall()
    {
        _particleSystem.Play();
        speed = 0;
        _rigidBody.velocity = Vector2.zero;
        _light2d.enabled = true;
        _boom.Play();
    }

    public void ResetBall(float yDirection)
    {
        speed = initialSpeed;
        transform.position = (yDirection > 0 ? paddle1.transform.position : paddle2.transform.position) + (new Vector3(0, 1, 0) * yDirection);
        _rigidBody.velocity = (new Vector2(Random.Range(-0.5f, 0.5f), yDirection)).normalized * speed;
        _light2d.enabled = false;
        // _trailRenderer.emitting = true;
        _trailRenderer.Clear();
        _particleSystem.Clear();
        _particleSystem.Stop();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player1" ||
            collision.transform.tag == "Player2" ||
            collision.transform.tag == "AI")
        {
            print($"{speed} + {acceleration} = {speed + acceleration}");
            speed += acceleration;
            float x_direction = collision.otherCollider.transform.position.x - collision.collider.transform.position.x;
            _rigidBody.velocity = new Vector2(x_direction, collision.contacts[0].normal.y).normalized * speed;
        }

        if (collision.transform.tag == "Goal1")
        {
            gameController.ScorePoint(2);
        }
        else if (collision.transform.tag == "Goal2")
        {
            gameController.ScorePoint(1);
        }
        else
        {
            _beep.Play();
        }
    }
}
