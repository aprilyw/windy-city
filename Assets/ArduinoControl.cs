using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoControl : MonoBehaviour
{
  SerialPort interfaceStream;
  public string interfacePath = "COM9";
  public int baudRate = 9600;

  public bool fanOn;

  void Start()
  {
    fanOn = false;
    Debug.Log("start");
    try
    {
      interfaceStream = new SerialPort(interfacePath, baudRate);
      interfaceStream.Open();
      interfaceStream.ReadTimeout = 1;
      Debug.Log("Opened.");
    }
    catch (System.IO.IOException e)
    {
      Debug.Log("Could not find the serial port named: " + interfacePath + " . Please check if you supplied the right one on the unity panel. Will shut down now.");
      Application.Quit();
    }
  }

  void Update () {
    if (interfaceStream.IsOpen) {
      if (Input.GetKey (KeyCode.RightArrow)) {
        try
        {
          Debug.Log("Turning right");
          WriteToArduino("0");
        }
        catch (TimeoutException e)
        {
          Debug.Log(e);
        }
      } else if (Input.GetKey (KeyCode.LeftArrow)) {
          try
          {
            Debug.Log("Turning left");
            WriteToArduino("1");
          }
          catch (TimeoutException e)
          {
            Debug.Log(e);
          }
      }
    }
  }

  void OnDisable()
  {
    interfaceStream.Close();
  }

  public void WriteToArduino(string m)
  {
    interfaceStream.WriteLine(m);
    interfaceStream.BaseStream.Flush();
  }
}
