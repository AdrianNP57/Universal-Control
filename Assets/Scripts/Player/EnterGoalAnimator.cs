using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGoalAnimator : MonoBehaviour
{
    private GameObject goal = null;

    public Vector3 centerOffset;

    public float initialAngularVelocity;
    public float maxAngularVelocity;
    public float angularAcceleration;

    public float scaleDownVelocity;

    public bool scaleAfterRotate;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = Mathf.Infinity;
    }

    void Update()
    {
        if(goal != null)
        {
            transform.position = goal.transform.position + centerOffset;
            rb.angularVelocity += angularAcceleration * Time.deltaTime * Vector3.back;

            bool maxAngularVelocityReached = Mathf.Abs(rb.angularVelocity.z) >= maxAngularVelocity;

            if (maxAngularVelocityReached)
            {
                rb.angularVelocity = maxAngularVelocity * Vector3.back;
            }  

            if(maxAngularVelocityReached || !scaleAfterRotate)
            {
                transform.localScale -= scaleDownVelocity * Time.deltaTime * Vector3.one;

                if (transform.localScale.x <= 0)
                {
                    transform.localScale = Vector3.zero;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BlackHole"))
        {
            goal = collision.gameObject;

            foreach(SphereCollider collider in collision.gameObject.GetComponents<SphereCollider>())
            {
                collider.enabled = false;
                rb.angularVelocity = initialAngularVelocity * Vector3.back;
            }
        }
    }
}
