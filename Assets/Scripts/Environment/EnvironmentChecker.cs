using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvironmentChecker : MonoBehaviour
{
    protected List<BaseEnvironment> environments;

    protected BaseEnvironment ClosestEnvironment
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

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.GetComponent<BaseEnvironment>() != null)
            environments.Add(collisionObject.GetComponent<BaseEnvironment>());
    }
    protected void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        environments.Remove(collisionObject.GetComponent<BaseEnvironment>());
    }
}
