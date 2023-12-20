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

    [SerializeField] string[] Name_list = new string[10];
    [SerializeField] string[] Skill_list = new string[10];

    void Update()
    {
        if (ReadyManager.P1 != null && ReadyManager.P2 != null)
        {
            P1_num = ReadyManager.P1;
            P2_num = ReadyManager.P2;
            if(P1_num == 2 ||  P1_num == 7)
                P1_name.text = "Random";
            else
                P1_name.text = Name_list[P1_num];
            if (P2_num == 2 || P2_num == 7)
                P2_name.text = "Random";
            else
                P2_name.text = Name_list[P2_num];
            P1_skill.text = Skill_list[P1_num];
            P2_skill.text = Skill_list[P2_num];
        }
    }
}
