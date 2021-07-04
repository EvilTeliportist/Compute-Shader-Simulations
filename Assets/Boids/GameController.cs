using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private List<Boid> boids;
    public int numBoids = 10;
    public GameObject PREFAB_BOID;

    public float moveSpeed = 1f;
    public float turnSpeed = 1f;
    public float detectionDistance = 2f;
    public float viewAngle = 90f;

    // Start is called before the first frame update
    void Start()
    {

        boids = new List<Boid>();

        for (int i = 0; i < numBoids; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-8f, 8f), Random.Range(-4.5f, 4.5f), 0);
            float angle = Random.Range(0f, 360f);
            GameObject GO_Boid = Instantiate(PREFAB_BOID, pos, transform.rotation * Quaternion.Euler(0f, 0f, angle));
            Boid boid = GO_Boid.GetComponent<Boid>();

            boid.boids = boids;
            boid.moveSpeed = moveSpeed;
            boid.detectionDistance = detectionDistance;

            boids.Add(boid);
        }

        boids[0].selected = true;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Boid b in boids)
        {
            b.moveSpeed = moveSpeed;
            b.turnSpeed = turnSpeed;
            b.detectionDistance = detectionDistance;
            b.viewAngle = viewAngle;
        }
    }
}
