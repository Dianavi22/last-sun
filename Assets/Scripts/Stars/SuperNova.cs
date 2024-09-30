using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperNova : MonoBehaviour
{
    private bool isCharging;
    [SerializeField] private float explosionRadius;
    [SerializeField] private GameObject _targetPlayer;
    private float _rotateY;
    [SerializeField] private GameObject _sprite;
    [SerializeField] GameManager _gameManager;
    private Camera _camera;

    [SerializeField] ParticleSystem _explosePart;
    [SerializeField] ParticleSystem _rotatePart;

    [SerializeField] AudioClip _explosion;
    private bool _soundPlayed = false;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _explosionNull;
    private bool isDie = false;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _camera = FindObjectOfType<Camera>();

        _audioSource = FindObjectOfType<AudioSource>();


    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_gameManager.isCapsule)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    StartCoroutine(CountExplosion());
                    explosionRadius += 0.5f;
                    _sprite.transform.localScale = new Vector3(explosionRadius * 2, explosionRadius * 2, explosionRadius * 2);
                }
            }
        }
        _rotateY = transform.localRotation.y;
        try
        {
            gameObject.transform.LookAt(_targetPlayer.transform);

        }
        catch
        {
            return;
        }

    }
    private IEnumerator DestroyStar()
    {
        if (!isDie)
        {
            isDie = true;
            this._sprite.SetActive(false);
            this.GetComponent<MeshRenderer>().enabled = false;
            _rotatePart.Stop();
            _explosePart.Play();
            if (!_soundPlayed)
            {
                _audioSource.PlayOneShot(_explosionNull, 0.6f);
                _soundPlayed = true;

            }
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }

    }

    private IEnumerator CountExplosion()
    {
        isCharging = true;
           yield return new WaitForSeconds(3);
        ExplodeStar(); 
        StartCoroutine(DestroyStar());

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    void ExplodeStar()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                Vector3 direction = collider.transform.position - transform.position;
                if (!_soundPlayed)
                {
                    _audioSource.PlayOneShot(_explosion, 0.6f);
                    _soundPlayed = true;

                }
                direction = direction.normalized;

                Rigidbody playerRigidbody = collider.gameObject.GetComponent<Rigidbody>();
                playerRigidbody.AddForce(direction * 50, ForceMode.Impulse);
                collider.GetComponent<Player>().isMove = true;
                _camera.GetComponent<ShakyCame>().isShaking = true;

            }
        }
    }
}
