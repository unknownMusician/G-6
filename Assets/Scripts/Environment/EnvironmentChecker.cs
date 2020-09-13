using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvironmentChecker : MonoBehaviour
{
    protected List<BaseEnvironment> environments;

    public BaseEnvironment ClosestEnvironment
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
        environments = new List<BaseEnvironment>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.GetComponent<BaseEnvironment>() != null)
        {
            environments.Add(collisionObject.GetComponent<BaseEnvironment>());
            Debug.DrawLine(this.transform.position, collisionObject.transform.position);
        }
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        environments.Remove(collisionObject.GetComponent<BaseEnvironment>());
    }
}
