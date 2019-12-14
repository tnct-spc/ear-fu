using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSerial : MonoBehaviour
{
    public SerialHandler serial;
    public float timeOut;
    public Text text;
    private float timeTrigger;

    // Start is called before the first frame update
    void Start()
    {
        serial.OnDataReceived += OnDataReceived;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeTrigger)
        {
            // Do anything
            serial.Write("unti");

            timeTrigger = Time.time + timeOut;
        }
        
    }

    void OnDataReceived(string message)
    {

        try
        {
            Debug.Log(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
