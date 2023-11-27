using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ready_Select : MonoBehaviour
{
    [SerializeField] bool drowGizmo;                //����� ��ο� ��/��
    [SerializeField] GameObject[] player;           //�÷��̾� ����Ʈ
    [SerializeField] GameObject aniBox;             //�ִϸ��̼ǿ� �ڽ�
    [SerializeField] GameObject randomBox;          //�����ڽ�(�� ū��)
    [SerializeField] Vector2 boxSize;               //�ڽ��� ������
    [SerializeField] float moveDelay;               //������ ������
    [SerializeField] float changeDelay;             //ü���� ������
    [SerializeField] float colorDelay;              //�÷� ������

    Vector3[] characterPos = new Vector3[10];       //ĳ���� ����â ������
    float selectPos;                                //����â ��ġ
    float gapX, gapY;                               //ĳ���ͺ� ����
}
