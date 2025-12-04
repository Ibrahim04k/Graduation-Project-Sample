using UnityEngine;

public interface IInteractable {


    public void Interact(AdventurePlayer adventurePlayer);

    public string GetInteractText();

    public Transform GetTransform();


}