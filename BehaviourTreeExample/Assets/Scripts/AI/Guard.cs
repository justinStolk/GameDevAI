using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Guard : MonoBehaviour
{
    private Blackboard Bb;

    private BTNode guardBehaviour;
    private BTNode patrolBehaviour;
    private BTNode attackBehaviour;

    //private BTNode observePlayer;
    private BTNode chasePlayer;
    private BTNode getWeapon;
    private BTNode checkPlayerPresence;

    private NavMeshAgent agent;
    private Animator animator;
    //private GameObject weapon = null;

    [SerializeField] private Text stateText;
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
        SharedBlackboard.SetValue("SeePlayer", false);
    }

    private void Start()
    {
        Bb = new Blackboard();
        Bb.SetValue<Transform>("target", waypointSystem.waypoints[0]);

        checkPlayerPresence = new BTSelector(false,
            new BTSequence(
               new BTEvaluateSharedBool("PlayerAlive"),
                        new BTCanSeeTarget(viewPoint, player, fieldOfView.viewAngle, fieldOfView.viewRadius, awarenessRadius),
                        //new BTSelector(false, 
                        //new BTInView(viewPoint, player, fieldOfView.viewAngle, fieldOfView.viewRadius, "Player"), new BTConditionalCheckDistanceTo(gameObject, player.gameObject, 5, BTConditionalCheckDistanceTo.CheckType.LESS_OR_EQUAL )),
                        new BTSetSharedBool("SeePlayer", true)
                        ),
                new BTInverter(new BTSetSharedBool("SeePlayer", false))
            );

        chasePlayer = new BTSelector(false, new BTInverter(new BTParallel(BTParallel.ParallelPolicy.ON_CHILD_FAIL, 
            checkPlayerPresence, 
            new BTSequence(
                        new BTDisplayText(stateText, "Chasing Enemy"),
                new BTSetTarget(Bb, player),
                new BTPlayAnimation(animator, "Rifle Walk"),
                new BTMoveTowards(Bb, agent, "target", attackRange, true),
                new BTPlayAnimation(animator, "Kick"),
                new BTAttack(this.gameObject, 10, player.GetComponent<IDamageable>()),
                new BTWait(1),
                new BTPlayAnimation(animator, "Idle"),
                new BTSetSharedBool("PlayerAlive", false)
                ))), new BTInverter(new BTSequence(
                new BTStopAgent(agent), 
                new BTPlayAnimation(animator, "Idle"))));

        //observePlayer = new BTSequence(
        //    checkPlayerPresence,
        //    new BTDisplayText(stateText, "Alerted"),
        //    chasePlayer
        //    );

        getWeapon = new BTSequence(
                        new BTDisplayText(stateText, "Getting Weapon"),
            new BTPlayAnimation(animator, "Rifle Walk"),
            new BTFindTaggedObjects(Bb, "Weapon", "Weapons"),
            new BTFindClosest(Bb, this.transform, "Weapons", "TargetWeapon"),
            new BTMoveTowards(Bb, agent, "TargetWeapon", 0.7f, false),
            new BTPlayAnimation(animator, "Idle"),
            new BTSetBool(Bb, "HasWeapon", true)
            );

        attackBehaviour = new BTSequence(
            checkPlayerPresence,
            new BTSelector(false, new BTCheckBool(Bb, "HasWeapon"), getWeapon), 
            chasePlayer
            );

        patrolBehaviour = new BTSequence(
            new BTParallel(BTParallel.ParallelPolicy.ON_ALL_CHILDREN_SUCCESS, 
            new BTInverter(checkPlayerPresence), new BTSequence(
            new BTDisplayText(stateText, "Patrolling"),
            //new BTCanSeeTarget(viewPoint, player, fieldOfView.viewAngle, fieldOfView.viewRadius, awarenessRadius),
                    //new BTInverter(new BTEvaluateSharedBool("PlayerAlive"))),
                    //new BTInverter(new BTCanSeeTarget(viewPoint, player, fieldOfView.viewAngle, fieldOfView.viewRadius, awarenessRadius)),
                    new BTSetWaypoint(Bb, waypointSystem, "waypointSystem"),
                    new BTPlayAnimation(animator, "Rifle Walk"),
                            new BTMoveTowards(Bb, agent, "target", 0, false),
                            new BTPlayAnimation(animator, "Idle"),
                                     new BTWait(patrolWaitTime)
                                        )));            
 
        guardBehaviour = new BTSelector(true, attackBehaviour, patrolBehaviour);
    }

    private void FixedUpdate()
    {
        guardBehaviour?.Run();
    }

    //private void OnDrawGizmos()
    //{

    ////    Gizmos.color = Color.yellow;
    //    Handles.color = Color.red;
    ////    Vector3 endPointLeft = viewTransform.position + (Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
    ////    Vector3 endPointRight = viewTransform.position + (Quaternion.Euler(0, ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;

    //   Handles.DrawWireArc(transform.position, Vector3.up, transform.position, 360, awarenessRadius, 2);
    ////    Gizmos.DrawLine(viewTransform.position, endPointLeft);
    ////    Gizmos.DrawLine(viewTransform.position, endPointRight);

    //}
}
