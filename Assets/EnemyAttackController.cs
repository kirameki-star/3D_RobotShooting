using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public Collider attack;
    // Start is called before the first frame update
    void Start()
    {
        //attack = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
		attack.isTrigger = false;
    }

}
