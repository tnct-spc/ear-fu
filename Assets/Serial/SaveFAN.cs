using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveFAN : MonoBehaviour
{
    public string filename;
    private string sensorPath = "";
    private StreamWriter writer;

    public SerialHandler serial;
    public float timeOut;
    private float timeTrigger;

    private bool trigger = false;

    // Start is called before the first frame update
    void Start()
    {
        serial.OnDataReceived += OnDataReceived;

        Encoding enc = Encoding.GetEncoding("UTF-8");
#if UNITY_EDITOR
        while (sensorPath == "") sensorPath = EditorUtility.SaveFilePanel("select sensor file", "Assets", "data", "txt");
#else
        sensorPath = Application.dataPath + "/Resources/" + filename;
#endif
        writer = new StreamWriter(sensorPath, false, enc);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeTrigger)
        {
            // Do anything
            try
            {
                if (trigger) writer.WriteLine("0");
                trigger = true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }


            timeTrigger = Time.time + timeOut;
        }
    }

    void OnDestroy()
    {
        Close();
    }

    void OnDataReceived(string message)
    {
        try
        {
            Debug.Log(message);
            if (trigger)
            {
                trigger = false;
                writer.WriteLine(message);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    private void Close()
    {
        writer.Close();
        Debug.Log("close file");

    }
}
