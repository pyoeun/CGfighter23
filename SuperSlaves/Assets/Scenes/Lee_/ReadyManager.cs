using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class ReadyManager : MonoBehaviour
{
    //애니메이션용

    //오브젝트 받아오기
    [SerializeField] bool drowGizmo;                                //기즈모 드로우 유/무
    [SerializeField] GameObject[] character;                        //캐릭터 리스트
    [SerializeField] GameObject playerBox1;                         //플레이어 박스 1
    [SerializeField] GameObject playerBox2;                         //플레이어 박스 2
    [SerializeField] GameObject aniBox;                             //애니메이션용 박스
    [SerializeField] GameObject randomBox;                          //랜덤박스
    [SerializeField] UnityEngine.Color color1;
    [SerializeField] UnityEngine.Color color2;
    //값 받아오기
    [SerializeField] float selectPos;                               //선택창 위치
    [SerializeField] float gapX;
    [SerializeField] float gapY;                                    //캐릭터별 간극
    [SerializeField] Vector2 boxSize;                               //박스의 사이즈
    [SerializeField] float moveDelay;                               //움직임 딜레이
    [SerializeField] float changeDelay;                             //체인지 딜레이
    [SerializeField] float colorDelay;                              //컬러 딜레이
    //함수 내에서만 사용
     Vector3[] characterPos = new Vector3[10];                      //캐릭터 선택창 포지션
    float tempPos1, tempPos2;                                       //스폰의 시작점
    float time;                                                     //deltatime용 변수
    GameObject[] tempObj = new GameObject[12];
    short checkSpawn = 0;
    bool spawn = false;

    //키입력
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
                    
                    break; //가운데_위 + 아래
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
                    break; //첫번째 갈라짐
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
                        break; //두번째 갈라짐
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
                    break; //랜덤용 모이기
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
                    break; //색변화 1
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
                        break; //색변화 2
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
                    break; //색변화 3
                case 7:
                    time += Time.deltaTime; 
                    if (time > changeDelay * 1.4f)
                    {
                        tempObj[0].GetComponent<SpriteRenderer>().DOFade(0, colorDelay * 1.6f);
                        checkSpawn++;
                        time = 0.0f;
                    }
                    break; //투명색변화 1
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
                    break; //투명색변화 2
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
                    break; //투명색변화 3
                case 10:
                    for (int i = 0; i < characterPos.Length; i++)
                    {
                        if (i < 5)
                        {
                            if (i != 2)
                                Instantiate(character[i], characterPos[i], Quaternion.identity);
                        }
                        else
                        {
                            if (i != 7)
                                Instantiate(character[i], characterPos[i], Quaternion.identity);
                        }
                    }
                    Instantiate(randomBox, new Vector3(0, -selectPos), Quaternion.identity);;
                    checkSpawn++;
                    break; //선택창(캐릭터 프로필)생성 + 랜덤
                case 11:
                    time += Time.deltaTime;
                    if (time < 0.4f)
                    {
                        Pl_1 = Instantiate(playerBox1, characterPos[P1], Quaternion.identity);
                        Pl_2 = Instantiate(playerBox2, characterPos[P2], Quaternion.identity);
                        time = 0.0f;
                        checkSpawn++;
                    }
                        break; //선택용 박스 생성
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
                    break; //색변화용 오브젝트 삭제
            }
        } //
        else
        {
            //Player1
            {
                if (Input.GetKeyDown(KeyCode.W)) //위 
                {
                    if (P1 - 5 < 0)
                        StartCoroutine(ShakeP1_V());
                    else
                        P1_next = P1 - 5;
                }
                if (Input.GetKeyDown(KeyCode.S)) //아
                {
                    if (P1 + 5 >= 10)
                        StartCoroutine(ShakeP1_V());
                    else
                        P1_next = P1 + 5;
                }
                if (Input.GetKeyDown(KeyCode.A)) //왼
                {
                    if (P1 - 1 < 0)
                        P1_next = 9;
                    else
                        P1_next = P1 - 1;

                }
                if (Input.GetKeyDown(KeyCode.D)) //오
                {
                    P1_next = (P1 + 1) % 10;
                }

                if (P1 != P1_next)
                {
                    if (P1_next == 2 || P1_next == 7)
                    {
                        Pl_1.transform.DOMove(new Vector3(0, -selectPos), 0.2f);
                        Pl_1.transform.DOScale(new Vector3(1, boxSize.y + gapY + (gapY / 3) + 0.02f), 0.2f);
                    }
                    else
                    {
                        Pl_1.transform.DOMove(characterPos[P1_next], 0.2f);
                        Pl_1.transform.DOScale(new Vector3(1, 1), 0.2f);
                    }
                    P1 = P1_next;
                }
            }
            //Player2
            {
                if (Input.GetKeyDown(KeyCode.UpArrow)) //위 
                {
                    if (P2 - 5 < 0)
                        StartCoroutine(ShakeP2_V());
                    else
                        P2_next = P2 - 5;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow)) //아
                {
                    if (P2 + 5 >= 10)
                        StartCoroutine(ShakeP2_V());
                    else
                        P2_next = P2 + 5;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow)) //왼
                {
                    if (P2 - 1 < 0)
                        P2_next = 9;
                    else
                        P2_next = P2 - 1;

                }
                if (Input.GetKeyDown(KeyCode.RightArrow)) //오
                {
                    P2_next = (P2 + 1) % 10;
                }

                if (P2 != P2_next)
                {
                    if (P2_next == 2 || P2_next == 7)
                    {
                        Pl_2.transform.DOMove(new Vector3(0, -selectPos), 0.2f);
                        Pl_2.transform.DOScale(new Vector3(1, boxSize.y + gapY + (gapY / 3) + 0.02f), 0.2f);
                    }
                    else
                    {
                        Pl_2.transform.DOMove(characterPos[P2_next], 0.2f);
                        Pl_2.transform.DOScale(new Vector3(1, 1), 0.2f);
                    }
                    P2 = P2_next;
                }
            }
        }
        
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
