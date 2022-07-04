using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using UnityEngine.UI;

public class Rogue : MonoBehaviour
{

    [SerializeField] private float maxDistanceToPlayer;
    [SerializeField] private float preferredDistanceToPlayer;
    [SerializeField] private float throwingRange;
    [SerializeField] private float throwingForce;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject guard;
    //[SerializeField] private LayerMask hidingObjectLayer;
    [SerializeField] private Rigidbody smokeBomb;
    [SerializeField] private Transform throwingPoint;
    [SerializeField] private Text stateText;

    private Blackboard blackboard;
    private BTNode rogueBehaviour;
    private BTNode idleBehaviour;
    private BTNode followBehaviour;
    private BTNode supportBehaviour;
    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        blackboard = new Blackboard();
        blackboard.SetValue<Transform>("player", player.transform);
        blackboard.SetValue<Transform>("guard", guard.transform);
        blackboard.SetValue<Transform>("throwpoint", throwingPoint.transform);
        //blackboard.SetValue<GameObject[]>("hidingspots", GameObject.FindGameObjectsWithTag("HidingSpot"));

        idleBehaviour = new BTSequence(new BTConditionalCheckDistanceTo(this.gameObject, player, maxDistanceToPlayer, 
            BTConditionalCheckDistanceTo.CheckType.LESS_OR_EQUAL), 
            new BTPlayAnimation(animator, "Crouch Idle"),
            new BTDisplayText(stateText, "Idling"));

        followBehaviour = new BTSequence(
            new BTConditionalCheckDistanceTo(this.gameObject, player, maxDistanceToPlayer, BTConditionalCheckDistanceTo.CheckType.GREATER_THAN),
            new BTDisplayText(stateText, "Following"),
            new BTPlayAnimation(animator, "Walk Crouch"), 
            new BTMoveTowards(blackboard, agent, "player", preferredDistanceToPlayer, true));
        //new BTSeek(agent, player, 5, maxDistanceToPlayer));

        supportBehaviour = new BTSequence(new BTEvaluateSharedBool("SeePlayer"),
            new BTEvaluateSharedBool("PlayerAlive"),
            new BTDisplayText(stateText, "Supporting"),
            new BTFindTaggedObjects(blackboard, "HidingSpot", "hidingspots"),
            new BTFindClosest(blackboard, this.transform, "hidingspots", "hidingspot"),
            new BTPlayAnimation(animator, "Walk Crouch"),
            new BTMoveTowards(blackboard, agent, "hidingspot", 0.5f, false),
            new BTPlayAnimation(animator, "Crouch Idle"),
            new BTConditionalCheckDistanceTo(this.gameObject, guard, throwingRange, BTConditionalCheckDistanceTo.CheckType.LESS_OR_EQUAL),
            new BTTimerEvaluator(blackboard, "SmokeCooldown", 5),
            new BTSpawnAtTransform(blackboard, "player", smokeBomb.gameObject, false)
            // new BTThrowObject(blackboard ,smokeBomb, "throwpoint", "guard", throwingForce, 5)
            );

        rogueBehaviour = new BTParallel(BTParallel.ParallelPolicy.ON_CHILD_SUCCESS, new BTTimer(blackboard, "SmokeCooldown"), new BTSelector(true, supportBehaviour, new BTSequence(followBehaviour, idleBehaviour)));
        //rogueBehaviour = new BTSequence(followBehaviour, idleBehaviour);
    }

    private void FixedUpdate()
    {
        rogueBehaviour?.Run();
    }

    //private void OnDrawGizmos()
    //{

    ////    Gizmos.color = Color.yellow;
    //    Handles.color = Color.yellow;
    ////    Vector3 endPointLeft = viewTransform.position + (Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
    ////    Vector3 endPointRight = viewTransform.position + (Quaternion.Euler(0, ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
    //    Handles.DrawWireArc(transform.position, Vector3.up, transform.position, 360, maxDistanceToPlayer);
    //    Handles.color = Color.blue;
    //    Handles.DrawWireArc(transform.position, Vector3.up, transform.position, 360, preferredDistanceToPlayer);
    ////    Handles.DrawWireArc(viewTransform.position, Vector3.up, Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward, ViewAngleInDegrees.Value * 2, SightRange.Value);
    ////    Gizmos.DrawLine(viewTransform.position, endPointLeft);
    ////    Gizmos.DrawLine(viewTransform.position, endPointRight);

    //}
}
