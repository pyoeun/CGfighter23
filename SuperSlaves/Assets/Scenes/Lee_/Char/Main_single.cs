using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Main_single : MonoBehaviour
{
    private static Main_single instance = new Main_single();

    // private constructor to avoid client applications to use constructor
    private Main_single() { }

    public static Main_single getInstance()
    {
        return instance;
    }
}
