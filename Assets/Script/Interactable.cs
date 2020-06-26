using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3;

    bool isFocus = false;
    Transform player;

    public Transform interactionTransform;

    bool hasInteracted = false;

    public virtual void Interact()
    {

    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance < radius)
            {
                Interact(); //What u wanna do

                hasInteracted = true;
            }
        }
    }

    public void OnFocus(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocus()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
