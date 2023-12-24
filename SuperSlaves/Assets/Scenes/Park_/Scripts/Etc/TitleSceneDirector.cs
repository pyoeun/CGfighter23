using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneDirector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_infoText;
    [SerializeField] private float m_blickTime;
    private float m_timer = 0;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainScene");
        }
        m_timer += Time.deltaTime;
        if(m_timer >= m_blickTime)
        {
            m_infoText.gameObject.SetActive(!m_infoText.gameObject.activeInHierarchy);
            m_timer = 0;
        }
    }
}
