using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Slime_Agent
{
    public Vector2 position;
    public float direction;
    public Color color;
};


public class ComputeShaderTest : MonoBehaviour
{

    public ComputeShader computerShader;
    public RenderTexture renderTexture;
    public RenderTexture fadedRenderTexture;
    public RenderTexture final;
    public int numAgents = 100;
    public int width = 100;
    public int height = 100;

    public float evaporateSpeed = 1f;
    public float diffuseSpeed = 1f;

    [Range(0, Mathf.PI)]
    public float sensorAngleSpacing = 1;
    public float moveSpeed = 10;
    public float turnSpeed = 1f;
    public float sensorSize = 5;
    public float sensorOffsetDist = 5;

    public int radius = 100;
    public bool inwards;
    public bool staggered;
    public bool spin;

    public Color color1;
    public Color color2;
    public Color color3;

    private Slime_Agent[] data;
    private ComputeBuffer buffer;


    public void Start()
    {
        data = new Slime_Agent[numAgents];

        for (int i = 0; i < numAgents; i++)
        {
            Slime_Agent a = new Slime_Agent();
            float angle = ((2 * Mathf.PI / numAgents) * i);

            Vector2 pos = new Vector2(radius * Mathf.Cos(angle),
                                      radius * Mathf.Sin(angle));

            if (staggered)
            {
                pos *= Random.value;
            }

            pos += new Vector2(width / 2, height / 2);
            a.position = pos;

            if (inwards)
            {
                a.direction = (angle + Mathf.PI) % (Mathf.PI * 2);
            }
            else
            {
                a.direction = angle;
            }


            if (spin)
            {
                a.direction += Mathf.PI / 2;
            }

            a.color = new Color(1, 1, 1, 0);


            data[i] = a;
        }

        int bufferSize = sizeof(float) * 7;
        buffer = new ComputeBuffer(numAgents, bufferSize);
        buffer.SetData(data);
        Debug.Log("Buffer set");
    }


    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(width, height, 24);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();

            fadedRenderTexture = new RenderTexture(width, height, 24);
            fadedRenderTexture.enableRandomWrite = true;
            fadedRenderTexture.Create();

            final = new RenderTexture(width, height, 24);
            final.enableRandomWrite = true;
            final.Create();

            Debug.Log("RenderTextures created.");
        }

        computerShader.SetTexture(0, "Texture", renderTexture);
        computerShader.SetTexture(1, "Texture", renderTexture);
        computerShader.SetTexture(0, "FadedTexture", fadedRenderTexture);
        computerShader.SetTexture(1, "FadedTexture", fadedRenderTexture);

        computerShader.SetBuffer(0, "agents", buffer);

        computerShader.SetInt("width", width);
        computerShader.SetInt("height", height);
        computerShader.SetInt("radius", radius);

        computerShader.SetInt("numAgents", numAgents);
        computerShader.SetFloat("deltaTime", Time.deltaTime);
        computerShader.SetFloat("moveSpeed", moveSpeed);
        computerShader.SetFloat("PI", Mathf.PI);

        computerShader.SetFloat("evaporateSpeed", evaporateSpeed / 100f);
        computerShader.SetFloat("diffuseSpeed", diffuseSpeed);

        computerShader.SetFloat("turnSpeed", turnSpeed);
        computerShader.SetFloat("sensorAngleSpacing", sensorAngleSpacing);
        computerShader.SetFloat("sensorOffsetDist", sensorOffsetDist);
        computerShader.SetFloat("sensorSize", sensorSize);

        computerShader.SetVector("color1", color1);
        computerShader.SetVector("color2", color2);
        computerShader.SetVector("color3", color3);

        computerShader.Dispatch(0, width / 8, height / 8, 1);
        computerShader.Dispatch(1, width / 8, height / 8, 1);
        Graphics.Blit(fadedRenderTexture, renderTexture);
        Graphics.Blit(renderTexture, dest);
    }
}
