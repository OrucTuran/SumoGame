using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    GameObject target;
    bool isTargetSelected;
    [SerializeField] private ParticleSystem ps;

    [SerializeField] float maxDistanceToTarget = 10f;
    [SerializeField] float selectionRadius = 9f;
    [SerializeField] LayerMask layerMask;

    [SerializeField] float speed = 2;
    [SerializeField] float maxSpeed = 10;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ps.Stop();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ps.Play();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        ps.Stop();
    }

    void Update()
    {
        if (isTargetSelected == false)
        {
            var colliders = Physics.OverlapSphere(transform.position, selectionRadius, layerMask);
            var closest = GetClosest(transform.position, colliders, out var distance, GetComponent<Collider>());
            if (closest != null)
            {
                isTargetSelected = true;
                target = closest.transform.gameObject;
            }
        }

        if (isTargetSelected == false) return;

        if (Vector3.Distance(transform.position, target.transform.position) > maxDistanceToTarget)
        {
            isTargetSelected = false;
            return;
        }

        var direction = target.transform.position - transform.position;
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(direction.normalized * (speed * Time.deltaTime));
        }
    }

    void OnDrawGizmos()
    {
        const float defaultAlpha = 0.5f;

        if (isTargetSelected)
        {
            Gizmos.color = GetColor(Color.magenta, 0.8f);
            Gizmos.DrawLine(transform.position, target.transform.position);

            Gizmos.color = GetColor(Color.red, defaultAlpha);
        }
        else
        {
            Gizmos.color = GetColor(Color.blue, defaultAlpha);
        }

        Gizmos.DrawSphere(transform.position, selectionRadius);
    }

    Color GetColor(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    public static T GetClosest<T>(Vector3 currentPosition, T[] searchArray, out float distance, params T[] excludeArray) where T : Component
    {
        distance = float.MaxValue;

        T selected = default;
        for (int i = 0; i < searchArray.Length; i++)
        {
            var current = searchArray[i];
            var dist = Vector3.Distance(currentPosition, current.transform.position);
            if (dist < distance && Contains(current, excludeArray) == false)
            {
                distance = dist;
                selected = searchArray[i];
            }
        }

        return selected;
    }

    public static bool Contains<T>(T item, T[] array)
    {
        int length = array.Length;
        for (int i = 0; i < length; i++)
        {
            if (item.Equals(array[i])) return true;
        }

        return false;
    }
}
