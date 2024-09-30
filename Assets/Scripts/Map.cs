using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    [SerializeField] private float mapRadius;

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

            direction = direction.normalized;

            Rigidbody playerRigidbody = playerCollider.gameObject.GetComponent<Rigidbody>();
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.AddForce(direction * 2f, ForceMode.Impulse);
            playerCollider.GetComponent<Player>().isMove = true;
        }
    }



}
