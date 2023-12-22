using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Main_single : MonoBehaviour
{
    private static Main_single instance;

    //인게임
    public static PlayerTypes player1;
    public static PlayerTypes player2;
    short Win;


    int sceneNum = 0;
    //0-Main
    //1-Ready
    //2-Ingame

    private void Update()
    {
        switch(sceneNum)
        {
            case 1:
                {
                    
    
                }
                break;
            case 2:
                break;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Main")
            sceneNum = 0;
        if (collision.collider.tag == "Ready")
            sceneNum = 1;
        if (collision.collider.tag == "InGame")
            sceneNum = 2;
    }

    public static void characterChoose_P1(int P1)
    {
        switch (P1)
        {
            case 0:
                player1 = PlayerTypes.P01; break;
            case 1:
                player1 = PlayerTypes.P03; break;
            case 2:
                player1 = RandomChar(); break;
            case 3:
                player1 = PlayerTypes.P06; break;
            case 4:
                player1 = PlayerTypes.P08; break;
            case 5:
                player1 = PlayerTypes.P10; break;
            case 6:
                player1 = PlayerTypes.P12; break;
            case 7:
                player1 = RandomChar(); break;
            case 8:
                player1 = PlayerTypes.P19; break;
            case 9:
                player1 = PlayerTypes.P51; break;

        }
    }
    public static void characterChoose_P2(int P2)
    {
        switch (P2)
        {
            case 0:
                player2 = PlayerTypes.P01; break;
            case 1:
                player2 = PlayerTypes.P03; break;
            case 2:
                player2 = RandomChar(); break;
            case 3:
                player2 = PlayerTypes.P06; break;
            case 4:
                player2 = PlayerTypes.P08; break;
            case 5:
                player2 = PlayerTypes.P10; break;
            case 6:
                player2 = PlayerTypes.P12; break;
            case 7:
                player2 = RandomChar(); break;
            case 8:
                player2 = PlayerTypes.P19; break;
            case 9:
                player2 = PlayerTypes.P51; break;
        }
    }

    static PlayerTypes RandomChar()
    {
        int R = UnityEngine.Random.Range(0, 9);
        if (R == 2)
            R++;
        if (R == 7)
            R--;

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
