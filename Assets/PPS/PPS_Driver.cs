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
    public int numAgents = 100;
    public int width = 100;
    public int height = 100;

    // Mutable Parameters
    public float sensingDistance = 10f;
    public float attraction = 1f;

    [Range(.8f, 1f)]
    public float friction = .999f;

    private PPS_Agent[] agents;
    private float[] weights;
    private ComputeBuffer buffer;
    private ComputeBuffer weightBuffer;
    private ComputeBuffer colorBuffer;

    public Color[] available_colors = { Color.red, Color.green, Color.blue };


    public void Start()
    {

        // Create color weight array
        weights = new float[available_colors.Length * available_colors.Length];
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.value + 1 * Mathf.Sign(Random.value - .5f);
        }


        for (int i = 0; i < available_colors.Length; i++)
        {
            weights[3 * i + i] = 0f;
        }

        Debug.Log(string.Join(" ", weights));


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

        Debug.Log("Agents initialized");

        // Make buffer
        int bufferSize = sizeof(float) * 4 + sizeof(int);
        buffer = new ComputeBuffer(numAgents, bufferSize);
        buffer.SetData(agents);

        // Set weights buffer
        int weightBufferSize = sizeof(float) * weights.Length;
        weightBuffer = new ComputeBuffer(weights.Length, weightBufferSize);
        weightBuffer.SetData(weights);

        // Set colors buffer
        int colorBufferSize = sizeof(float) * 4 * available_colors.Length;
        colorBuffer = new ComputeBuffer(available_colors.Length, colorBufferSize);
        colorBuffer.SetData(available_colors);

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

        computerShader.SetInt("width", width);
        computerShader.SetInt("height", height);
        computerShader.SetInt("numAgents", numAgents);
        computerShader.SetFloat("deltaTime", Time.deltaTime);

        computerShader.SetFloat("attraction", attraction);
        computerShader.SetFloat("friction", friction);
        computerShader.SetFloat("sensingDistance", sensingDistance);

        computerShader.Dispatch(0, width / 8, height / 8, 1);
        Graphics.Blit(renderTexture, dest);

    }
}
