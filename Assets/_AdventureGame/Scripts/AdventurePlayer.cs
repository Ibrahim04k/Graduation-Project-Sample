using System;
using System.Collections.Generic;
using UnityEngine;

public class AdventurePlayer : MonoBehaviour, IDialogueCharacter {


    public static AdventurePlayer Instance { get; private set; }


    public event EventHandler<OnSelectedInteractionChangedEventArgs> OnSelectedInteractionChanged;
    public class OnSelectedInteractionChangedEventArgs : EventArgs {
        public IInteractable interactable;
    }


    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dialogueCameraGameObject;


    private float speedAnimatorParameter;
    private float targetSpeedAnimatorParameter;
    private Inventory inventory;
    private IInteractable selectedInteractable;


    private void Awake() {
        Instance = this;

        inventory = GetComponent<Inventory>();
    }

    private void Update() {
        if (DialogueSystem.Instance.IsDialogueActive()) {
            HandleDialogue();
        } else {
            HandleMovement();
            HandleInteractions();
        }
    }

    private void HandleDialogue() {
        if (Input.GetKeyDown(KeyCode.E)) {
            DialogueSystem.Instance.Next();
        }
    }

    private void HandleInteractions() {
        float interactDistance = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactDistance);
        IInteractable closestInteractable = null;
        foreach (Collider collider in colliderArray) {
            if (collider.TryGetComponent(out IInteractable interactable)) {
                if (closestInteractable == null) {
                    closestInteractable = interactable;
                } else {
                    if (Vector3.Distance(transform.position, interactable.GetTransform().position) <
                        Vector3.Distance(transform.position, closestInteractable.GetTransform().position)) {
                        // Closer
                        closestInteractable = interactable;
                    }
                }
            }
        }

        if (closestInteractable != selectedInteractable) {
            selectedInteractable = closestInteractable;
            OnSelectedInteractionChanged?.Invoke(this, new OnSelectedInteractionChangedEventArgs { interactable = selectedInteractable });
        }

        if (Input.GetKeyDown(KeyCode.E) && selectedInteractable != null) {
            selectedInteractable.Interact(this);

            if (DialogueSystem.Instance.IsDialogueActive()) {
                // Started dialogue
                selectedInteractable = null;
                OnSelectedInteractionChanged?.Invoke(this, new OnSelectedInteractionChangedEventArgs { interactable = selectedInteractable });
            }
        }
    }

    private void HandleMovement() {
        Vector3 inputVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            inputVector.z = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            inputVector.z = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            inputVector.x = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            inputVector.x = +1f;
        }

        if (inputVector == Vector3.zero) {
            targetSpeedAnimatorParameter = 0f;
        } else {
            targetSpeedAnimatorParameter = 6f;
        }

        float blendSpeed = 20f;
        speedAnimatorParameter = Mathf.Lerp(speedAnimatorParameter, targetSpeedAnimatorParameter, Time.deltaTime * blendSpeed);
        animator.SetFloat("Speed", speedAnimatorParameter);

        Transform cameraTransform = Camera.main.transform;
        Vector3 moveDir = cameraTransform.forward * inputVector.z + cameraTransform.right * inputVector.x;
        moveDir.y = 0f;
        moveDir.Normalize();
        float moveSpeed = 5f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float rotationSpeed = 12f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    public Inventory GetInventory() {
        return inventory;
    }

    public GameObject GetVirtualCameraGameObject() => dialogueCameraGameObject;

}