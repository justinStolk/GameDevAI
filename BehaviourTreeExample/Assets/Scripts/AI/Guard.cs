using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    private Blackboard Bb;
    private BTNode guardBehaviour;
    private BTNode patrolBehaviour;
    private BTNode attackBehaviour;
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject weapon;
    [SerializeField] private Transform player;
    [SerializeField] private Transform viewPoint;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private WaypointSystem waypointSystem;
    [SerializeField] private float awarenessRadius = 2;
    [SerializeField] private float attackRange = 1;
    [SerializeField] private float patrolWaitTime;
    private Vector3 lastPosition;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Bb = new Blackboard();
        Bb.SetValue<Transform>("target", waypointSystem.waypoints[0]);
        attackBehaviour = new BTSequence(
            //new BTSenseTarget(Bb, this.transform, fieldOfView),
            new BTCanSeeTarget(viewPoint, player, fieldOfView.viewAngle, fieldOfView.viewRadius, awarenessRadius),
            new BTSetTarget(Bb, player),
            new BTPlayAnimation(animator, "Rifle Walk"),
            //  new BTSelector(true, new BTSequence(
            // new BTHasObject(Bb, "weapon"), 
            new BTMoveTowards(Bb, agent, attackRange, false),
            new BTPlayAnimation(animator, "Kick"),
            new BTAttack(this.gameObject, 10, player.GetComponent<IDamageable>()),
            new BTWait(3)
            // ), new BTSequence()
            ,
        new BTDebug("Executed attack behaviour successfully")) ;
        patrolBehaviour = new BTSequence(
                  new BTInverter(new BTCanSeeTarget(viewPoint, player, fieldOfView.viewAngle, fieldOfView.viewRadius, awarenessRadius)),
                    new BTSetWaypoint(Bb, waypointSystem, "waypointSystem"),
                    new BTPlayAnimation(animator, "Rifle Walk"),
                            new BTMoveTowards(Bb, agent, 0, false),
                            new BTPlayAnimation(animator, "Idle"),
                                     new BTWait(patrolWaitTime)
                                        );            
        guardBehaviour = new BTSelector(false, attackBehaviour, patrolBehaviour);
    }

    private void FixedUpdate()
    {
        guardBehaviour?.Run();
    }

    private void OnDrawGizmos()
    {
    //    Gizmos.color = Color.yellow;
        Handles.color = Color.red;
    //    Vector3 endPointLeft = viewTransform.position + (Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
    //    Vector3 endPointRight = viewTransform.position + (Quaternion.Euler(0, ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;

       Handles.DrawWireArc(transform.position, Vector3.up, transform.position, 360, awarenessRadius, 2);
    //    Gizmos.DrawLine(viewTransform.position, endPointLeft);
    //    Gizmos.DrawLine(viewTransform.position, endPointRight);

    }
}
