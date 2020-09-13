using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractableChecker : MonoBehaviour
{
    protected List<InteractableBase> environments;

    public InteractableBase ClosestEnvironment
    {
        get
        {
            if (environments.Count != 0)
                return environments
                    .OrderBy(
                        p => (p.transform.position - this.transform.position).magnitude
                        )
                    .First();
            return null;
        }
    }

    void Start()
    {
        environments = new List<InteractableBase>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.GetComponent<InteractableBase>() != null)
        {
            environments.Add(collisionObject.GetComponent<InteractableBase>());
            Debug.DrawLine(this.transform.position, collisionObject.transform.position);
        }
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        environments.Remove(collisionObject.GetComponent<InteractableBase>());
    }
}
