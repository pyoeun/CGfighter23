using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ready_data : MonoBehaviour
{
    [SerializeField] GameObject P1_img;
    [SerializeField] GameObject P2_img;
    [SerializeField] SpriteRenderer[] img_P1_list;
    [SerializeField] SpriteRenderer[] img_P2_list;
    [SerializeField] SpriteRenderer[] img_Skills;
    public Text P1_name;
    public Text P2_name;

    public Text P1_skill;
    public Text P2_skill;

    int P1_num;
    int P2_num;

    public static bool select1;
    public static bool select2;

    [SerializeField] string[] Name_list = new string[10];
    [SerializeField] string[] Skill_list = new string[10];

    private void Start()
    {
        select1 = false;
        select2 = false;
    }
    void Update()
    {
        if (select1)
        {
            Debug.Log(Main_single.Player1_);
            P1_num = Main_single.Player1_;
            P1_name.text = Name_list[P1_num];
            P1_skill.text = Skill_list[P1_num];
        }
        else
        {
            P1_num = ReadyManager.P1;
            if (P1_num == 2 || P1_num == 7)
                P1_name.text = "Random";
            else
                P1_name.text = Name_list[P1_num];
            P1_skill.text = Skill_list[P1_num];
        }
        if(select2)
        {
            P2_num = Main_single.Player2_;
            P2_name.text = Name_list[P2_num];
            P2_skill.text = Skill_list[P2_num];
        }
        else
        {
            
            P2_num = ReadyManager.P2;
            if (P2_num == 2 || P2_num == 7)
                P2_name.text = "Random";
            else
                P2_name.text = Name_list[P2_num];
            P2_skill.text = Skill_list[P2_num];
        }
    }
}
