using UnityEngine;

public class AdventureNPCWalking : MonoBehaviour {


    [SerializeField] private Animator animator;
    [SerializeField] private Transform waypointContainer;


    private int activeWaypoint;


    private void Update() {
        Vector3 targetPosition = waypointContainer.GetChild(activeWaypoint).position;
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        moveDir.y = 0f;
        float moveSpeed = 1.7f;
        transform.position += moveDir * Time.deltaTime * moveSpeed;

        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

        animator.SetFloat("Speed", 2f);

        if (Vector3.Distance(transform.position, targetPosition) < .5f) {
            activeWaypoint = (activeWaypoint + 1) % waypointContainer.childCount;
        }
    }


}