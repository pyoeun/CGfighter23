using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loding_Tip : MonoBehaviour
{
    [SerializeField] string[] Tmi;
    string t = "TMI : ";
    int rand;
    private void Start()
    {
        rand = Random.Range(0,Tmi.Length);
        this.GetComponent<Text>().text = t + Tmi[rand];
    }
}
