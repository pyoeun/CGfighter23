using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroCredit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_skipInfoText;

    [SerializeField] private TextMeshPro m_storyText;
    [SerializeField] private float m_storySpeed;

    [SerializeField] private float m_standardTime;
    [SerializeField] private float m_exitTime;
    private float m_timer = 0f;

    [SerializeField] private Image m_fadeImg;
    [SerializeField] private float m_fadeTime;
    private Coroutine m_titleLoader;

    private void Update()
    {
        if(m_timer < m_exitTime)
        {
            m_timer += Time.deltaTime;
        }

        if(m_timer >= m_standardTime && !m_skipInfoText.gameObject.activeInHierarchy)
        {
            m_skipInfoText.gameObject.SetActive(true);
        }

        if (((Input.anyKeyDown && m_timer >= m_standardTime) || m_timer >= m_exitTime) && m_titleLoader == null)
        {
            m_titleLoader = StartCoroutine(LoadTitleScene());
        }

        StoryCredit();
    }

    private void StoryCredit()
    {
        m_storyText.transform.Translate(new Vector3(0, 1, 0.15f) * m_storySpeed * Time.deltaTime);
    }

    private IEnumerator LoadTitleScene()
    {
        Color color = m_fadeImg.color;
        while(m_fadeImg.color.a < 1)
        {
            color.a += m_fadeTime * Time.deltaTime;
            m_fadeImg.color = color;
            yield return null;
        }
        SceneManager.LoadScene("TitleScene");
        yield break;
    }
}
