using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    public JoyStick JoyStick;
    Rigidbody rb;
    [SerializeField] private int speed = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ps.Stop();
    }

    void Update()
    {
        Vector3 vec = new Vector3(JoyStick.InputDirection.x, 0, JoyStick.InputDirection.y);
        rb.AddForce(vec * (speed * Time.deltaTime));
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
}
