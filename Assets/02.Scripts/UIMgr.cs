using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIMgr : MonoBehaviour
{
    // 버튼 연결 변수
    public Button startButton;
    public Button optionButton;
    public Button exitButton;

    private UnityAction action;

    void Start()
    {
        // UnityAction을 사용해서 이벤트 연결
        action = () => OnStartClick();
        startButton.onClick.AddListener(action);

        // 무명 메서드 연결
        optionButton.onClick.AddListener(delegate { OnButtonClick(optionButton.name); } );

        // 람다식
        exitButton.onClick.AddListener(() => OnButtonClick(exitButton.name));
    }

    void OnStartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void OnButtonClick(string str)
    {
        Debug.Log($"Click Button : {str}");
    }
}
