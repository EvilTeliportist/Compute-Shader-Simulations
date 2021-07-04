using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Boid : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float detectionDistance = 0f;
    public float viewAngle = 90f;
    public float turnSpeed = 2f;

    public bool selected = false;


    public List<Boid> boids;
    public Transform tf;
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D col;

    public void Start()
    {
        tf = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<PolygonCollider2D>();

        if (selected)
        {
            spriteRenderer.color = Color.red;
        }
    }

    public void Update()
    {
        CheckWorldBounds();
    
        List<Boid> seen = SeeBoids();
        Vector3 move = CalculateMove(seen);
        tf.rotation = new Quaternion(0, 0, Vector3.Angle(Vector3.up, move), 0);
        tf.position = tf.position - move * moveSpeed / 500f;

    }

    public void CheckWorldBounds()
    {
        if (tf.position.x > 9f)
        {
            tf.position = new Vector3(-9f, tf.position.y, 0);
        }

        if (tf.position.x < -9f)
        {
            tf.position = new Vector3(9f, tf.position.y, 0);
        }

        if (tf.position.y > 5.3f)
        {
            tf.position = new Vector3(tf.position.x, -5.3f, 0);
        }

        if (tf.position.y < -5.3f)
        {
            tf.position = new Vector3(tf.position.x, 5.3f, 0);
        }
    }


    public List<Boid> SeeBoids()
    {
        List<Boid> seen = new List<Boid>();

        foreach (Boid other in boids)
        {
            if (other == this) { continue; }

            if (Vector3.Distance(tf.position, other.tf.position) <= detectionDistance &&
                Vector3.Angle(tf.up, other.tf.position - tf.position) <= viewAngle)
            {
                seen.Add(other);
                other.spriteRenderer.color = Color.Lerp(other.spriteRenderer.color, Color.blue, Time.deltaTime * 5);
            }
            else
            {
                other.spriteRenderer.color = Color.Lerp(other.spriteRenderer.color, Color.white, Time.deltaTime * 5);
            }
        }

        return seen;
    }

    public Vector3 CalculateMove(List<Boid> seen)
    {
        Vector3 avoidanceResultant = Vector3.zero;
        foreach (Boid other in seen){
            Debug.DrawLine(tf.position, other.tf.position, Color.red);
            float dist_strength = Vector3.Distance(tf.position, other.tf.position) / detectionDistance;

            avoidanceResultant += (other.tf.position - tf.position) * dist_strength;
        }



        Vector3 resultant = avoidanceResultant;
        return resultant.normalized;
    }

}
