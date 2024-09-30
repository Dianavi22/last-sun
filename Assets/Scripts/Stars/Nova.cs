using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nova : MonoBehaviour
{
    private bool isCharging;
    [SerializeField] private float explosionRadius;
    [SerializeField] private GameObject _targetPlayer;
    private float _rotateY;
    [SerializeField] GameManager _gameManager;
    private Camera _camera;
    [SerializeField] private GameObject _sprite;
    [SerializeField] ParticleSystem _explosePart;
    [SerializeField] ParticleSystem _rotatePart;

    private bool isDie = false;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _camera = FindObjectOfType<Camera>();

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
                    explosionRadius += 2;
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

    private IEnumerator DestroyStar()
    {
        if (!isDie)
        {
            isDie = true;
            this._sprite.SetActive(false);
            this.GetComponent<MeshRenderer>().enabled = false;
            _rotatePart.Stop();
            _explosePart.Play();
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }

    }

    void ExplodeStar()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                Vector3 direction = collider.transform.position - transform.position;

                direction = direction.normalized;

                Rigidbody playerRigidbody = collider.gameObject.GetComponent<Rigidbody>();
                playerRigidbody.AddForce(direction * 15, ForceMode.Impulse);
                collider.GetComponent<Player>().isMove = true;
                _camera.GetComponent<ShakyCame>().isShaking = true;


            }
        }
    }
}
