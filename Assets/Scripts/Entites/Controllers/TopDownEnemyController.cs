using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemyController : TopDownController
{
    protected Transform ClosestTarget {get; private set;}

    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        ClosestTarget = GameManager.Instance.player;
    }

    protected virtual void FixedUpdate(){

    }
    protected float DistanceToTarget(){
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }
    protected Vector2 DirectionToTarget(){
        return (ClosestTarget.position-transform.position).normalized;
    }
}
