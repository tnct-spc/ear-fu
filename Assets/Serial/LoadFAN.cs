using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LoadFAN : MonoBehaviour
{
    public string filename;
    private string sensorPath = "";
    private List<string> data;

    public SerialHandler serial;
    public float timeOut;
    private float timeTrigger;

    private int i = 0;
    private bool trigger = false;

    // Start is called before the first frame update
    void Start()
    {        
        serial.OnDataReceived += OnDataReceived;
        data = new List<string>();

#if UNITY_EDITOR
        while (sensorPath == "") sensorPath = EditorUtility.OpenFilePanel("select sensor file", "you must select sensor data file", "txt");
#else
        string fileName = PlayerPrefs.GetString("selectSensorData");
        sensorPath = Application.dataPath + "/Resources/" + fileName;
#endif
        Debug.Log("loadSensorData :" + sensorPath);

        getData();

    }

    public void Play()
    {
        trigger = true;
    }

    public void Stop() {
        trigger = false;
        string sendmsg = "fan " + 0 + "\r\n";
        serial.Write(sendmsg);
        Debug.Log("sendmsg : " + sendmsg);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeTrigger)
        {
            if (trigger)
            {
                try
                {
                    string sendmsg = "fan " + data[i] + "\r\n";
                    serial.Write(sendmsg);
                    Debug.Log("sendmsg : " + sendmsg);
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning(e.Message);
                }
                i++;

                if (i == data.Count) i = 0;
            }

            timeTrigger = Time.time + timeOut;
        }
    }

    public void getData()
    {

        if (!File.Exists(@sensorPath))
        {
            Debug.LogWarning("Error");
        }
        else
        {
            StreamReader reader = new StreamReader(sensorPath); // Chage Input File
            while (!reader.EndOfStream)
            {
                try
                {
                    data.Add(reader.ReadLine());
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning(e.Message);
                }
            }
            reader.Close();
            Debug.Log("data_length :" + data.Count);
            Debug.Log("success");
        }
    }

    void OnDataReceived(string message)
    {
        try
        {
            Debug.Log("getmsg : " + message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
