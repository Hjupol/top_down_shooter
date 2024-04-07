using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller_Enemy : MonoBehaviour
{
    public static int numPatroler;
    internal GameObject player;
    internal NavMeshAgent agent;
    internal Renderer render;
    internal Vector3 destination;
    public float patrolDistance = 5;
    public float destinationTime = 4;
    public float enemySpeed;

    void Start()
    {
        render = GetComponent<Renderer>();
        Restart._Restart.OnRestart += Reset;
        destination = new Vector3(UnityEngine.Random.Range(-10, 12), 1, UnityEngine.Random.Range(-12, 9));
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    public void Reset()
    {
        Destroy(this.gameObject);
    }

    internal virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            Controller_Hud.points++;
        }
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            Destroy(this.gameObject);
            Controller_Hud.points++;
        }
        if (collision.gameObject.CompareTag("Bumeran"))
        {
            Destroy(this.gameObject);
            Controller_Hud.points++;
        }
    }

    private void OnDestroy()
    {
        Instantiator.enemies.Remove(this);
    }

    private void OnDisable()
    {
        Restart._Restart.OnRestart -= Reset;
    }
}
