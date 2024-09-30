using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    [SerializeField] private float mapRadius;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip wall;
    private bool isWall = false;

    void Start()
    {
        
    }

    void Update()
    {
        ReturnToMap();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, mapRadius);
    }


    private void ReturnToMap()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, mapRadius);
        bool playerInCircle = false;

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                playerInCircle = true;
                break;
            }
        }

        if (!playerInCircle)
        {
            Collider playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
            Vector3 direction = transform.position - playerCollider.transform.position;
            if (!isWall)
            {
                audioSource.PlayOneShot(wall, 1f);
                StartCoroutine(CooldownSound());
            }
            direction = direction.normalized;

            Rigidbody playerRigidbody = playerCollider.gameObject.GetComponent<Rigidbody>();
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.AddForce(direction * 2f, ForceMode.Impulse);
            playerCollider.GetComponent<Player>().isMove = true;
        }
    }

    private IEnumerator CooldownSound() {
    yield return new WaitForSeconds(0.3f);
        isWall = false;
    }




}
