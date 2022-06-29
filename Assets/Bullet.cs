using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    public float Speed = 1;
    public float lifetime = 0;
    public float frequency = 20f;
    public float magnitude = 0.5f;

    Rigidbody2D rb;

    private Vector2 pos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifetime = Random.Range(0, 2 * Mathf.PI);
    }

    private void FixedUpdate()
    {
        
        lifetime += Time.fixedDeltaTime;
        rb.velocity = GetProjectileVelocity(transform.up, Speed, lifetime, frequency, magnitude);
        


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    private Vector2 GetProjectileVelocity(Vector2 _forward, float _speed, float _time, float _frequency, float _amplitude)
    {
        Vector2 up = new Vector2(-_forward.y, _forward.x);
        float up_speed = Mathf.Cos(_time * _frequency) * _amplitude * _frequency;
        return up * up_speed + _forward * _speed;
    }
}
