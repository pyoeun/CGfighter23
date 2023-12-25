using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Main_single : MonoBehaviour
{
    private static Main_single instance;

    //InGame
    [SerializeField] public static PlayerTypes player1;
    [SerializeField] public static PlayerTypes player2;
    public static short Win;
    // Win-0
    //Lose-1
    //Drow-2

    public static int Player1_;
    public static int Player2_;

    int sceneNum = 0;
    //0-Main
    //1-Ready
    //2-Ingame

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        switch(sceneNum)
        {
            //Ready
            case 1:
                {

                }
                break;
            //Fight
            case 2:
                break;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.name == "Main")
        {
            sceneNum = 0;
            Debug.Log("Main");
        }
        if (collision.collider.gameObject.name == "Ready")
        {
            sceneNum = 1;
            Debug.Log("Ready");
        }
        if (collision.collider.gameObject.name == "InGame")
        {
            sceneNum = 2;
            Debug.Log("InGame");
        }
    }

    public static void characterChoose_P1(int P1)   
    {
        Player1_ = P1;
        switch (P1)
        {
            case 0:
                player1 = PlayerTypes.P01; break;
            case 1:
                player1 = PlayerTypes.P03; break;
            case 2:
                Player1_ = RandomChar();
                player1 = SwichChar(Player1_); break;
            case 3:
                player1 = PlayerTypes.P06; break;
            case 4:
                player1 = PlayerTypes.P08; break;
            case 5:
                player1 = PlayerTypes.P10; break;
            case 6:
                player1 = PlayerTypes.P12; break;
            case 7:
                Player1_ = RandomChar();
                player1 = SwichChar(Player1_); break;
            case 8:
                player1 = PlayerTypes.P19; break;
            case 9:
                player1 = PlayerTypes.P51; break;
        }
        Ready_data.select1 = true;
    }
    public static void characterChoose_P2(int P2)
    {
        Player2_ = P2;
        switch (P2)
        {
            case 0:
                player2 = PlayerTypes.P01; break;
            case 1:
                player2 = PlayerTypes.P03; break;
            case 2:
                Player2_ = RandomChar();
                player2 = SwichChar(Player2_); break;
            case 3:
                player2 = PlayerTypes.P06; break;
            case 4:
                player2 = PlayerTypes.P08; break;
            case 5:
                player2 = PlayerTypes.P10; break;
            case 6:
                player2 = PlayerTypes.P12; break;
            case 7:
                Player2_ = RandomChar();
                player2 = SwichChar(Player2_); break;
            case 8:
                player2 = PlayerTypes.P19; break;
            case 9:
                player2 = PlayerTypes.P51; break;
        }
        Ready_data.select2 = true;
    }

    static int RandomChar()
    {
        int R = Random.Range(0, 9);
        if (R == 2)
            R++;
        if (R == 7)
            R--;
        return R;
    }
    private Main_single() { }

    public static Main_single getInstance()
    {
        if (instance == null)
        {
            instance = new Main_single();
        }
        return instance;
    }
    static PlayerTypes SwichChar(int R)
    {
        switch (R)
        {
            case 0:
                return PlayerTypes.P01;
            case 1:
                return PlayerTypes.P03;
            case 3:
                return PlayerTypes.P06;
            case 4:
                return PlayerTypes.P08;
            case 5:
                return PlayerTypes.P10;
            case 6:
                return PlayerTypes.P12;
            case 8:
                return PlayerTypes.P19;
            case 9:
                return PlayerTypes.P51;
            default:
                return PlayerTypes.P01;
        }
    }
}
