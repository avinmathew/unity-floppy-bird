using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public float strength = 4.9f;
    public float gravity = -9.8f;

    public AudioSource audioSource;
    public AudioClip flapSound;
    public AudioClip dieSound;

    private Vector3 _direction;
    private float _dropTime = 0.75f;
    private float _timer = 0;

    private void OnEnable()
    {
        // Reset position
        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;
        _direction = Vector3.zero;

        GetComponent<Animator>().enabled = true;
    }

    private void Update()
    {
        bool isTouch = false;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                isTouch = true;
            }
        }
        bool isSpace = Input.GetKeyDown(KeyCode.Space);
        bool isMouseButton = Input.GetMouseButtonDown(0);
        if ((isTouch || isSpace || isMouseButton))
        {
            _direction = Vector3.up * strength;

            transform.rotation = Quaternion.Euler(0, 0, 24);
            _timer = 0;

            PlayFlapSound();
        }

        if (_timer > _dropTime)
        {
            transform.rotation = Quaternion.Euler(0, 0, -24);
        }
        else
        {
            _timer += Time.deltaTime;
        }

        // Apply gravity

        _direction.y += gravity * Time.deltaTime;

        // Apply movement
        transform.position += _direction * Time.deltaTime;

        // If bird goes off screen at bottom
        // We don't check top, as it would always hit the pipe collider
        if (gameObject.transform.position.y < -5)
        {
            killBird();
        }
    }

    private void PlayFlapSound()
    {
        audioSource.clip = flapSound;
        audioSource.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        killBird();
    }

    private void killBird()
    {
        audioSource.clip = dieSound;
        audioSource.Play();

        GetComponent<Animator>().enabled = false;
        GameManagerScript.instance.GameOver();
    }
}
