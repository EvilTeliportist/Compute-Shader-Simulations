using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PPS_Agent
{
    public Vector2 position;
    public Vector2 velocity;
    public int color;
};


public class PPS_Driver : MonoBehaviour
{

    public ComputeShader computerShader;
    public RenderTexture renderTexture;

    // Initializing variables
    public int numAgents = 1000;
    public int width = 100;
    public int height = 100;

    // Mutable Parameters
    public float sensingDistance = 10f;
    public float attraction = 1f;

    [Range(.8f, 1f)]
    public float friction = .999f;

    private PPS_Agent[] agents;
    private ComputeBuffer buffer;
    private ComputeBuffer colorBuffer;
    ComputeBuffer weightBuffer;

    public Color[] available_colors = { Color.red, Color.green, Color.blue };
    public float[] weights = { 1, 1, 1, 1, 1, 1, 1, 1, 1 };

    public GameObject prefab;
    private GameObject[] dots;



    public void Start()
    {

        // Initialize all agents
        agents = new PPS_Agent[numAgents];
        for (int i = 0; i < numAgents; i++)
        {
            PPS_Agent agent = new PPS_Agent();
            agent.position = new Vector2(Random.value * width, Random.value * height);

            agent.color = Random.Range(0, available_colors.Length);
            agent.velocity = new Vector2(0, 0);
            agents[i] = agent;
        }

        // Make all gameobjects
        dots = new GameObject[numAgents];
        for (int i = 0; i < numAgents; i++)
        {
            PPS_Agent agent = agents[i];
            GameObject go = Instantiate(prefab, new Vector2(10 * agent.position.x / width, 10 * agent.position.y / height), Quaternion.identity);
            go.GetComponent<SpriteRenderer>().color = available_colors[agent.color];
            dots[i] = go;
        }

        Debug.Log("Agents initialized");

        // Make buffer
        int bufferSize = sizeof(float) * 4 + sizeof(int);
        buffer = new ComputeBuffer(numAgents, bufferSize);
        buffer.SetData(agents);


        // Set colors buffer
        int colorBufferSize = sizeof(float) * 4 * available_colors.Length;
        colorBuffer = new ComputeBuffer(available_colors.Length, colorBufferSize);
        colorBuffer.SetData(available_colors);

        weightBuffer = new ComputeBuffer(weights.Length, sizeof(float) * weights.Length);
        weightBuffer.SetData(weights);

    }


    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {

        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(width, height, 24);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();
        }

        computerShader.SetTexture(0, "Texture", renderTexture);

        computerShader.SetBuffer(0, "agents", buffer);
        computerShader.SetBuffer(0, "weights", weightBuffer);
        computerShader.SetBuffer(0, "colors", colorBuffer);

        weightBuffer.SetData(weights);

        computerShader.SetInt("width", width);
        computerShader.SetInt("height", height);
        computerShader.SetInt("numAgents", numAgents);
        computerShader.SetFloat("deltaTime", Time.deltaTime);

        computerShader.SetFloat("attraction", attraction);
        computerShader.SetFloat("friction", friction);
        computerShader.SetFloat("sensingDistance", sensingDistance);

        computerShader.Dispatch(0, width / 8, height / 8, 1);
        Graphics.Blit(src, dest);

        buffer.GetData(agents);

        for (int i = 0; i < numAgents; i++)
        {
            PPS_Agent agent = agents[i];
            GameObject go = dots[i];
            go.transform.position = new Vector3((21 * agent.position.x / width) - 10.5f, (agent.position.y * 12 / height) - 6, 0);
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        weightBuffer.Dispose();
        buffer.Dispose();
        colorBuffer.Dispose();
    }
}
