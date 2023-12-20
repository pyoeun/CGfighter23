using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Main_single : MonoBehaviour
{
    private static Main_single instance;

    private Main_single() { }

    public static Main_single getInstance()
    {
        if (instance == null)
        {
            instance = new Main_single();
        }
        return instance;
    }

}
