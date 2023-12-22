using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;
using UnityEngine.InputSystem;

public class ReadyManager : MonoBehaviour
{
    //�ִϸ��̼ǿ�

    //������Ʈ �޾ƿ���
    [SerializeField] bool drowGizmo;                                //����� ��ο� ��/��
    [SerializeField] GameObject[] character;                        //ĳ���� ����Ʈ
    [SerializeField] GameObject playerBox1;                         //�÷��̾� �ڽ� 1
    [SerializeField] GameObject playerBox2;                         //�÷��̾� �ڽ� 2
    [SerializeField] GameObject aniBox;                             //�ִϸ��̼ǿ� �ڽ�
    [SerializeField] GameObject randomBox;                          //�����ڽ�
    [SerializeField] UnityEngine.Color color1;
    [SerializeField] UnityEngine.Color color2;
    //�� �޾ƿ���
    [SerializeField] float selectPos;                               //����â ��ġ
    [SerializeField] float gapX;
    [SerializeField] float gapY;                                    //ĳ���ͺ� ����
    [SerializeField] Vector2 boxSize;                               //�ڽ��� ������
    [SerializeField] float moveDelay;                               //������ �����
    [SerializeField] float changeDelay;                             //ü���� �����
    [SerializeField] float colorDelay;                              //�÷� �����
    //�Լ� �������� ���
     Vector3[] characterPos = new Vector3[10];                      //ĳ���� ����â ������
    float tempPos1, tempPos2;                                       //������ ������
    float time;                                                     //deltatime�� ����
    GameObject[] tempObj = new GameObject[12];
    short checkSpawn = 0;
    bool spawn = false;

    //Ű�Է�
    float size_1;
    float size_2;
    int p1_;
    int p1_n;
    int p2_;
    int p2_n;

    GameObject[] cha = new GameObject[10];
    GameObject Pl_1;
    GameObject Pl_2;
    public static int P1;
    public static int P2;
    int P1_next;
    int P2_next;

    [SerializeField] float shakeAmount;


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
            Gizmos.DrawCube(new Vector2(0, -selectPos), new Vector2(boxSize.x, boxSize.y + (gapY * 2)));
        }
    }

    void Start()
    {
        size_1 = 1.3f;
        size_2 = 1.5f;
        P1 = 0;
        P1_next = 0;
        P2 = 4;
        P2_next = 4;
        aniBox.GetComponent<SpriteRenderer>().color = color1;
        time = 0.0f;

        tempPos1 = -gapX * 2;
        tempPos2 = -gapX * 2;
        for (int i = 0; i < characterPos.Length; i++)
        {
            if (i < 5)
            {
                characterPos[i] = new Vector3(tempPos1, gapY - selectPos);
                tempPos1 += gapX;
            }
            else
            {
                characterPos[i] = new Vector3(tempPos2, -gapY - selectPos);
                tempPos2 += gapX;
            }
        }
    }


    
    void Update()
    {
        if (!spawn)
        {
            switch (checkSpawn)
            {
                case 0:
                    time += Time.deltaTime;
                    if(time > 0.5f)
                    {
                        //Top
                        {
                            tempObj[0] = Instantiate(aniBox, new Vector3(0, -selectPos), Quaternion.identity);
                            tempObj[0].transform.DOMoveY(gapY - selectPos, moveDelay);
                        }
                        //Bottom
                        {
                            tempObj[1] = Instantiate(aniBox, new Vector3(0, -selectPos), Quaternion.identity);
                            tempObj[1].transform.DOMoveY(-gapY - selectPos, moveDelay);
                        }
                        checkSpawn++;
                        time = 0.0f;
                    }
                    
                    break; //���_�� + �Ʒ�
                case 1:
                    time += Time.deltaTime;
                    if(time> changeDelay)
                    {
                        //Top
                        {
                            tempObj[2] = Instantiate(aniBox, new Vector3(0, gapY - selectPos), Quaternion.identity);
                            tempObj[2].transform.DOMoveX(characterPos[1].x, moveDelay);
                            
                            tempObj[3] = Instantiate(aniBox, new Vector3(0, gapY - selectPos), Quaternion.identity);
                            tempObj[3].transform.DOMoveX(characterPos[3].x, moveDelay);
                        }
                        //Bottom
                        {
                            tempObj[4] = Instantiate(aniBox, new Vector3(0, -gapY - selectPos), Quaternion.identity);
                            tempObj[4].transform.DOMoveX(characterPos[6].x, moveDelay);

                            tempObj[5] = Instantiate(aniBox, new Vector3(0, -gapY - selectPos), Quaternion.identity);
                            tempObj[5].transform.DOMoveX(characterPos[8].x, moveDelay);
                        }
                        checkSpawn++;
                        time = 0.0f;
                    }
                    break; //ù��° ������
                case 2:
                        time += Time.deltaTime;
                        if (time > changeDelay * 0.9f)
                        {
                            //Top
                            {
                                tempObj[6] = Instantiate(aniBox, new Vector3(characterPos[1].x, gapY - selectPos), Quaternion.identity);
                                tempObj[6].transform.DOMoveX(characterPos[0].x, moveDelay);

                                tempObj[7] = Instantiate(aniBox, new Vector3(characterPos[3].x, gapY - selectPos), Quaternion.identity);
                                tempObj[7].transform.DOMoveX(characterPos[4].x, moveDelay);
                            }
                            //Bottom
                            {
                                tempObj[8] = Instantiate(aniBox, new Vector3(characterPos[6].x, -gapY - selectPos), Quaternion.identity);
                                tempObj[8].transform.DOMoveX(characterPos[5].x, moveDelay);

                                tempObj[9] = Instantiate(aniBox, new Vector3(characterPos[8].x, -gapY - selectPos), Quaternion.identity);
                                tempObj[9].transform.DOMoveX(characterPos[9].x, moveDelay);
                            }
                            checkSpawn++;
                            time = 0.0f;
                        }
                        break; //�ι�° ������
                case 3:
                    time += Time.deltaTime;
                    if (time > changeDelay)
                    {
                        //Top
                        {
                            tempObj[10] = Instantiate(aniBox, new Vector2(0,gapY - selectPos), Quaternion.identity);
                            tempObj[10].transform.DOMoveY(-selectPos, moveDelay * 25);
                        }
                        //Bottom
                        {
                            tempObj[11] = Instantiate(aniBox, new Vector2(0, -gapY - selectPos), Quaternion.identity);
                            tempObj[11].transform.DOMoveY(-selectPos, moveDelay * 25);
                        }
                        checkSpawn++;
                        time = 0.0f;
                    }
                    break; //������ ���̱�
                case 4:
                    time += Time.deltaTime;
                    if (time > changeDelay)
                    {
                        Destroy(tempObj[0]);
                        Destroy(tempObj[1]);
                        Destroy(tempObj[10]);
                        Destroy(tempObj[11]);
                        tempObj[0] = Instantiate(aniBox, new Vector3(0, -selectPos), Quaternion.identity);
                        tempObj[0].transform.localScale = new Vector3(boxSize.x, boxSize.y + (gapY * 2), 1);
                        tempObj[0].GetComponent<SpriteRenderer>().DOColor(color2, colorDelay);
                        checkSpawn++;
                        time = 0.0f;
                    }
                    break; //����ȭ 1
                case 5:
                    time += Time.deltaTime;
                    if (time > changeDelay * 1.2f)
                    {
                        tempObj[2].GetComponent<SpriteRenderer>().DOColor(color2, colorDelay);
                        tempObj[3].GetComponent<SpriteRenderer>().DOColor(color2, colorDelay);
                        tempObj[4].GetComponent<SpriteRenderer>().DOColor(color2, colorDelay);
                        tempObj[5].GetComponent<SpriteRenderer>().DOColor(color2, colorDelay);
                        checkSpawn++;
                        time = 0.0f;
                    }
                        break; //����ȭ 2
                case 6:
                    time += Time.deltaTime;
                    if (time > changeDelay * 1.2f)
                    {
                        tempObj[6].GetComponent<SpriteRenderer>().DOColor(color2, colorDelay);
                        tempObj[7].GetComponent<SpriteRenderer>().DOColor(color2, colorDelay);
                        tempObj[8].GetComponent<SpriteRenderer>().DOColor(color2, colorDelay);
                        tempObj[9].GetComponent<SpriteRenderer>().DOColor(color2, colorDelay);
                        checkSpawn++;
                        time = 0.0f;
                    }
                    break; //����ȭ 3
                case 7:
                    time += Time.deltaTime; 
                    if (time > changeDelay * 1.4f)
                    {
                        tempObj[0].GetComponent<SpriteRenderer>().DOFade(0, colorDelay * 1.6f);
                        checkSpawn++;
                        time = 0.0f;
                    }
                    break; //�������ȭ 1
                case 8:
                    time += Time.deltaTime;
                    if (time > changeDelay * 1.2f)
                    {
                        tempObj[2].GetComponent<SpriteRenderer>().DOFade(0, colorDelay * 1.6f);
                        tempObj[3].GetComponent<SpriteRenderer>().DOFade(0, colorDelay * 1.6f);
                        tempObj[4].GetComponent<SpriteRenderer>().DOFade(0, colorDelay * 1.6f);
                        tempObj[5].GetComponent<SpriteRenderer>().DOFade(0, colorDelay * 1.6f);
                        checkSpawn++;
                        time = 0.0f;
                    }
                    break; //�������ȭ 2
                case 9:
                    time += Time.deltaTime;
                    if (time > changeDelay * 1.8f)
                    {
                        tempObj[6].GetComponent<SpriteRenderer>().DOFade(0, colorDelay * 1.6f);
                        tempObj[7].GetComponent<SpriteRenderer>().DOFade(0, colorDelay * 1.6f);
                        tempObj[8].GetComponent<SpriteRenderer>().DOFade(0, colorDelay * 1.6f);
                        tempObj[9].GetComponent<SpriteRenderer>().DOFade(0, colorDelay * 1.6f);
                        checkSpawn++;
                        time = 0.0f;
                    }
                    break; //�������ȭ 3
                case 10:
                    for (int i = 0; i < characterPos.Length; i++)
                    {
                        if (i < 5)
                        {
                            if (i != 2)
                            {
                                cha[i] = Instantiate(character[i], characterPos[i], Quaternion.identity);
                                cha[i].transform.localScale = new Vector3(size_1, size_1);
                            }
                        }
                        else
                        {
                            if (i != 7)
                            {
                                cha[i] = Instantiate(character[i], characterPos[i], Quaternion.identity);
                                cha[i].transform.localScale = new Vector3(size_1, size_1);
                            }
                        }
                    }
                    cha[0].transform.localScale = new Vector3(size_2,size_2);
                    cha[4].transform.localScale = new Vector3(size_2, size_2);
                    Instantiate(randomBox, new Vector3(0, -selectPos), Quaternion.identity);;
                    checkSpawn++;
                    break; //����â(ĳ���� ������)���� + ����
                case 11:
                    time += Time.deltaTime;
                    if (time < 0.4f)
                    {
                        Pl_1 = Instantiate(playerBox1, characterPos[P1], Quaternion.identity);
                        Pl_2 = Instantiate(playerBox2, characterPos[P2], Quaternion.identity);
                        time = 0.0f;
                        checkSpawn++;
                    }
                        break; //���ÿ� �ڽ� ����
                case 12:
                    time += Time.deltaTime;
                    if (time > 0.6f)
                    {
                        for (int i = 0; i < tempObj.Length; i++)
                        {
                            Destroy(tempObj[i]);
                        }
                        spawn = true;
                    }
                    break; //����ȭ�� ������Ʈ ����
            }
        } //
        else
        {
            //Player1
                {
        //        if (Input.GetKeyDown(KeyCode.W)) //�� 
        //        {
        //            P1_InputUp();
        //        }
        //        if (Input.GetKeyDown(KeyCode.S)) //��
        //        {
        //            P1_InputDown();
        //        }
        //        if (Input.GetKeyDown(KeyCode.A)) //��
        //        {
        //            P1_InputLeft();
        //        }
        //        if (Input.GetKeyDown(KeyCode.D)) //��
        //        {
        //            P1_InputRight();
        //        }

                if (P1 != P1_next)
                {
                    if (P1_next == 2 || P1_next == 7)
                    {
                        Pl_1.transform.DOMove(new Vector3(0, -selectPos), 0.2f);
                        Pl_1.transform.DOScale(new Vector3(1, boxSize.y + gapY + (gapY / 3) + 0.02f), 0.2f);
                        cha[P1].transform.DOScale(new Vector3(size_1, size_1), 0.2f);
                    }
                    else
                    {
                        Pl_1.transform.DOMove(characterPos[P1_next], 0.2f);
                        Pl_1.transform.DOScale(new Vector3(1, 1), 0.2f);
                        if(P1 != 2 && P1 != 7)
                            cha[P1].transform.DOScale(new Vector3(size_1, size_1), 0.2f);
                        cha[P1_next].transform.DOScale(new Vector3(size_2, size_2), 0.2f);
                    }
                    P1 = P1_next;
                }
            }
            //Player2
            {
                //if (Input.GetKeyDown(KeyCode.UpArrow)) //�� 
                //{
                //    P2_InputUp();
                //}
                //if (Input.GetKeyDown(KeyCode.DownArrow)) //��
                //{
                //    P2_InputDown();
                //}
                //if (Input.GetKeyDown(KeyCode.LeftArrow)) //��
                //{
                //    P2_InputLeft();
                //}
                //if (Input.GetKeyDown(KeyCode.RightArrow)) //��
                //{
                //    P2_InputRight();
                //}

                if (P2 != P2_next)
                {
                    if (P2_next == 2 || P2_next == 7)
                    {
                        Pl_2.transform.DOMove(new Vector3(0, -selectPos), 0.2f);
                        Pl_2.transform.DOScale(new Vector3(1, boxSize.y + gapY + (gapY / 3) + 0.02f), 0.2f);
                        cha[P2].transform.DOScale(new Vector3(size_1, size_1), 0.2f);
                    }
                    else
                    {
                        Pl_2.transform.DOMove(characterPos[P2_next], 0.2f);
                        Pl_2.transform.DOScale(new Vector3(1, 1), 0.2f);
                        if (P2 != 2 && P2 != 7)
                            cha[P2].transform.DOScale(new Vector3(size_1, size_1), 0.2f);
                        cha[P2_next].transform.DOScale(new Vector3(size_2, size_2), 0.2f);
                    }
                    P2 = P2_next;
                }
            }
        }
        
    }
    private void OnMoveP1(InputValue value)
    {
        Vector2 inputVec = value.Get<Vector2>();

        //Left
        if (inputVec.x < 0)
        {
            P1_InputLeft();
        }
        //Right
        if (inputVec.x > 0)
        {
            P1_InputRight();
        }
        //Up
        if (inputVec.y > 0)
        {
            P1_InputUp();
        }
        //Down
        if (inputVec.y < 0)
        {
            P1_InputDown();
        }
    }
    private void OnMoveP2(InputValue value)
    {
        Vector2 inputVec = value.Get<Vector2>();

        //Left
        if (inputVec.x < 0)
        {
            P2_InputLeft();
        }
        //Right
        if (inputVec.x > 0)
        {
            P2_InputRight();
        }
        //Up
        if (inputVec.y > 0)
        {
            P2_InputUp();
        }
        //Down
        if (inputVec.y < 0)
        {
            P2_InputDown();
        }
    }

    private void OnPunchP1()
    {
       
    }
    void P1_InputUp()
    {
        if (P1 - 5 < 0)
            StartCoroutine(ShakeP1_V());
        else
            P1_next = P1 - 5;
    }
    void P1_InputDown()
    {
        if (P1 + 5 >= 10)
            StartCoroutine(ShakeP1_V());
        else
            P1_next = P1 + 5;
    }
    void P1_InputLeft()
    {
        if (P1 - 1 < 0)
            P1_next = 9;
        else
            P1_next = P1 - 1;
    }
    void P1_InputRight()
    {
        P1_next = (P1 + 1) % 10;
    }
    void P2_InputUp()
    {
        if (P2 - 5 < 0)
            StartCoroutine(ShakeP2_V());
        else
            P2_next = P2 - 5;
    }
    void P2_InputDown()
    {
        if (P2 + 5 >= 10)
            StartCoroutine(ShakeP2_V());
        else
            P2_next = P2 + 5;
    }
    void P2_InputLeft()
    {
        if (P2 - 1 < 0)
            P2_next = 9;
        else
            P2_next = P2 - 1;
    }
    void P2_InputRight()
    {
        P2_next = (P2 + 1) % 10;
    }
    IEnumerator ShakeP1_V()
    {
        float shakeTime = 0.0f;
        float localPos = Pl_1.transform.position.y;
        while (shakeTime < 0.2f)
        {
            float random = Random.Range(-shakeAmount, shakeAmount);
            Pl_1.transform.position = new Vector3(Pl_1.transform.position.x, localPos + random);
            yield return null;
            shakeTime += Time.deltaTime;
        }
        Pl_1.transform.position = new Vector3(Pl_1.transform.position.x, localPos);
    }
    IEnumerator ShakeP2_V()
    {
        float shakeTime = 0.0f;
        float localPos = Pl_2.transform.position.y;
        while (shakeTime < 0.2f)
        {
            float random = Random.Range(-shakeAmount, shakeAmount);
            Pl_2.transform.position = new Vector3(Pl_2.transform.position.x, localPos + random);
            yield return null;
            shakeTime += Time.deltaTime;
        }
        Pl_2.transform.position = new Vector3(Pl_2.transform.position.x, localPos);
    }
}
