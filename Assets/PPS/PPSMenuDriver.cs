using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PPSMenuDriver : MonoBehaviour
{

    public PPS_Driver cst;

    public GameObject Menu;
    public Slider attraction;
    public Slider friction;
    public Slider sensingDistance;

    public Slider c1r;
    public Slider c1g;
    public Slider c1b;

    public Slider c2r;
    public Slider c2g;
    public Slider c2b;

    public Slider c3r;
    public Slider c3g;
    public Slider c3b;


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
        cst.attraction = attraction.value;
        cst.friction = friction.value;
        cst.sensingDistance = sensingDistance.value;

        cst.weights[0] = c1r.value;
        cst.weights[1] = c1g.value;
        cst.weights[2] = c1b.value;

        cst.weights[3] = c2r.value;
        cst.weights[4] = c2g.value;
        cst.weights[5] = c2b.value;

        cst.weights[6] = c3r.value;
        cst.weights[7] = c3g.value;
        cst.weights[8] = c3b.value;
    }
}
