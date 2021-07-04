using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDriver : MonoBehaviour
{

    public ComputeShaderTest cst;

    public GameObject Menu;
    public Slider evapSpeed;
    public Slider diffSpeed;
    public InputField moveSpeed;
    public InputField turnSpeed;
    public Slider sensorAngle;
    public Slider sensorSize;
    public InputField sensorDist;


    public Slider c1r;
    public Slider c1g;
    public Slider c1b;
    public Slider c2r;
    public Slider c2g;
    public Slider c2b;
    public Slider c3r;
    public Slider c3g;
    public Slider c3b;

    public Image c1;
    public Image c2;
    public Image c3;

    public InputField numAgents;
    public InputField radius;
    public Toggle staggered;
    public Toggle inwards;
    public Toggle spin;


    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            Menu.SetActive(!Menu.activeSelf);
        }

        if (Input.GetKeyDown("space"))
        {
            Time.timeScale = (Time.timeScale == 1) ? 0 : 1;
        }
    }




    public void UpdateMenu()
    {
        cst.evaporateSpeed = evapSpeed.value;
        cst.diffuseSpeed = diffSpeed.value;
        cst.moveSpeed = int.Parse(moveSpeed.text);
        cst.turnSpeed = int.Parse(turnSpeed.text);
        cst.sensorAngleSpacing = sensorAngle.value;
        cst.sensorSize = sensorSize.value;
        cst.sensorOffsetDist = int.Parse(sensorDist.text);

        Color color1 = new Color(c1r.value, c1g.value, c1b.value);
        Color color2 = new Color(c2r.value, c2g.value, c2b.value);
        Color color3 = new Color(c3r.value, c3g.value, c3b.value);

        cst.color1 = color1;
        cst.color2 = color2;
        cst.color3 = color3;

        c1.color = color1;
        c2.color = color2;
        c3.color = color3;
    }


    public void Restart()
    {
        cst.numAgents = int.Parse(numAgents.text);
        cst.radius = int.Parse(radius.text);
        cst.staggered = staggered.isOn;
        cst.inwards = inwards.isOn;
        cst.spin = spin.isOn;
        cst.renderTexture = null;
        cst.Start();
    }
}
