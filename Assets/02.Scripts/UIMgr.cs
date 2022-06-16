using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIMgr : MonoBehaviour
{
    // ��ư ���� ����
    public Button startButton;
    public Button optionButton;
    public Button exitButton;

    private UnityAction action;

    void Start()
    {
        // UnityAction�� ����ؼ� �̺�Ʈ ����
        action = () => OnStartClick();
        startButton.onClick.AddListener(action);

        // ���� �޼��� ����
        optionButton.onClick.AddListener(delegate { OnButtonClick(optionButton.name); } );

        // ���ٽ�
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
