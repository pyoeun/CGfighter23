using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ReadyManager : MonoBehaviour
{
    public bool drowGizmo;                              //����� �׸�����
    [SerializeField] GameObject redBox;                 //�ִϸ��̼ǿ� �ڽ�
    public GameObject[] character;                      //ĳ���� ����
    public Vector3[] characterPos = new Vector3[10];    //ĳ���� ����â ������
    public float selectPos;                             //����â ��ġ
    public float gapX, gapY;                            //ĳ���ͺ� ����
    public Vector2 boxSize;                             //�ڽ��� ������
    public float delay;                                 //�ִϸ��̼� ������

    float tempPos1,tempPos2;                            //������ ������
    float time;                                         //deltatime�� ����

    private void OnDrawGizmos()
    {
        if (drowGizmo)
        {
            tempPos1 = -gapX * 2;
            tempPos2 = -gapX * 2;
            for (int i = 0; i < characterPos.Length; i++)
            {
                if (i < 5)
                {
                    if (i != 2)
                    {
                        characterPos[i] = new Vector3(tempPos1, gapY - selectPos);
                        tempPos1 += gapX;
                        Gizmos.color = new UnityEngine.Color(1, 0, 0, 0.5f);
                        Gizmos.DrawCube(characterPos[i], boxSize);
                    }
                    else
                        tempPos1 += gapX;
                }
                else
                {
                    if (i != 7)
                    {
                        characterPos[i] = new Vector3(tempPos2, -gapY - selectPos);
                        tempPos2 += gapX;
                        Gizmos.color = new UnityEngine.Color(1, 0, 0, 0.5f);
                        Gizmos.DrawCube(characterPos[i], boxSize);
                    }
                    else
                        tempPos2 += gapX;
                }
            }
            Gizmos.color = new UnityEngine.Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector2(0,-selectPos), new Vector2(boxSize.x, boxSize.y + (gapY*2)));
        }
    }

    void Start()
    {
        time = 0.0f;



        tempPos1 = -gapX * 2;
        tempPos2 = -gapX * 2;
        for (int i = 0; i < characterPos.Length; i++)
        {
            if (i < 5)
            {
                if (i != 2)
                {
                    characterPos[i] = new Vector3(tempPos1, gapY - selectPos);
                    tempPos1 += gapX;
                    Instantiate(character[0], characterPos[i], Quaternion.identity);
                }
                else
                    tempPos1 += gapX;
            }
            else
            {
                if (i != 7)
                {
                    characterPos[i] = new Vector3(tempPos2, -gapY - selectPos);
                    tempPos2 += gapX;
                    Instantiate(character[0], characterPos[i], Quaternion.identity);
                }
                else
                    tempPos2 += gapX;
            }
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        
    }
}
