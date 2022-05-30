using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Rogue : MonoBehaviour
{

    [SerializeField] private float maxDistanceToPlayer;
    [SerializeField] private float preferredDistanceToPlayer;
    [SerializeField] private float throwingRange;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject guard;
    [SerializeField] private LayerMask hidingObjectLayer;

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
        blackboard.SetValue<Transform>("target", player.transform);
        blackboard.SetValue<Transform>("guard", guard.transform);

        idleBehaviour = new BTSequence(new BTConditionalCheckDistanceTo(this.gameObject, player, maxDistanceToPlayer, 
            BTConditionalCheckDistanceTo.CheckType.LESS_OR_EQUAL), new BTPlayAnimation(animator, "Crouch Idle"));

        followBehaviour = new BTSequence(
            new BTConditionalCheckDistanceTo(this.gameObject, player, maxDistanceToPlayer, BTConditionalCheckDistanceTo.CheckType.GREATER_THAN),
            new BTPlayAnimation(animator, "Walk Crouch"), 
            new BTMoveTowards(blackboard, agent, preferredDistanceToPlayer, true));
        //new BTSeek(agent, player, 5, maxDistanceToPlayer));
        
        supportBehaviour = new BTSequence(new BTEvaluateBool("SeePlayer"),
            new BTGetClosestInLayer(blackboard, transform.position,
            throwingRange - 1, "obstacle", hidingObjectLayer),
            new BTDebug($"I got the closest in the layer! Which is: {blackboard.GetValue<Vector3>("obstacle")}"),
            new BTGetOppositeDirection(blackboard, "guard","obstacle", "hidingspot"),
            new BTDebug("I got the opposite direction!"),
            new BTMoveToPosition(blackboard, agent, 0.5f, "hidingspot"), 
            new BTDebug("I am now at the hiding spot"));

        rogueBehaviour = new BTSelector(true, supportBehaviour, new BTSequence(followBehaviour, idleBehaviour));
        //rogueBehaviour = new BTSequence(followBehaviour, idleBehaviour);
    }

    private void FixedUpdate()
    {
        rogueBehaviour?.Run();
    }

    private void OnDrawGizmos()
    {
    //    Gizmos.color = Color.yellow;
        Handles.color = Color.yellow;
    //    Vector3 endPointLeft = viewTransform.position + (Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
    //    Vector3 endPointRight = viewTransform.position + (Quaternion.Euler(0, ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
        Handles.DrawWireArc(transform.position, Vector3.up, transform.position, 360, maxDistanceToPlayer);
        Handles.color = Color.blue;
        Handles.DrawWireArc(transform.position, Vector3.up, transform.position, 360, preferredDistanceToPlayer);
    //    Handles.DrawWireArc(viewTransform.position, Vector3.up, Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward, ViewAngleInDegrees.Value * 2, SightRange.Value);
    //    Gizmos.DrawLine(viewTransform.position, endPointLeft);
    //    Gizmos.DrawLine(viewTransform.position, endPointRight);

    }
}
