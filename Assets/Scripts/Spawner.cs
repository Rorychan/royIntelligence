using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] int agentsCount;
    [SerializeField] GameObject agent;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < agentsCount; i++)
        {
           GameObject _agent =  Instantiate(agent, new Vector3(Random.Range(-100,100),Random.Range(-50,50),0),  transform.rotation);
            _agent.name = ""+i+"";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
