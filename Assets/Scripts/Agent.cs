using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Agent : MonoBehaviour
{
    [SerializeField]private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private Renderer renderer;
    [SerializeField] private GameObject[] targets;
    [SerializeField] private float[] distances;
    [SerializeField] private GameObject target;
    [SerializeField] private Color[] colors;
    private float trueSpeed;

    private void Awake()
    {
        GameObject[] targetArray = GameObject.FindGameObjectsWithTag("Resource");
        renderer = GetComponent<Renderer>();
        targets = targetArray;
        colors = new Color[2];
        colors[0] = Color.green;
        colors[1] = Color.red;
        trueSpeed = speed + UnityEngine.Random.Range(-4f, 4f);
           
        direction = new Vector3(UnityEngine.Random.Range(-1,1f), UnityEngine.Random.Range(-1,1), 0).normalized;
        distances = new float[2];
        distances[0] = 0;
        distances[1] = 0;
        target = targets[0];
        renderer.material.SetColor("_Color", colors[0]);


    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate(direction *Time.deltaTime*trueSpeed);
        for (int i = 0; i < 2; i++)
        {
            distances[i] += Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Resource"))
        {
            int index = Array.IndexOf(targets, other.gameObject);
            distances[index] = 0;
            if (other.gameObject == target)
            {
                
                if (Array.IndexOf(targets, target) == 0)
                {
                    target = targets[1];
                    renderer.material.SetColor("_Color", colors[1]);
                }
                else
                {
                    target = targets[0];
                    renderer.material.SetColor("_Color", colors[0]);
                }
                direction = -direction;
            }
            
        }
        if (other.gameObject.CompareTag("Sound"))
        {
            Agent otherAgent = other.gameObject.GetComponentInParent<Agent>();
            for (int i = 0; i < 2; i++)
            {
                if (distances[i] > otherAgent.distances[i])
                {
                    Debug.DrawLine(otherAgent.transform.position, transform.position, colors[i]);
                    distances[i] = otherAgent.distances[i]+15f;
                    if (i == Array.IndexOf(targets, target))
                    {
                        
                        direction = (otherAgent.transform.position - transform.position).normalized;
                    }
                }
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            
            direction = Vector3.Reflect(direction, collision.contacts[0].normal);
        }
    }


}