using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPELoginView : MonoBehaviour
{
    private InputField m_Username = null;
    private InputField m_Password = null;
    private Button m_BtnLogin = null;

    private void Start()
    {
        m_Username = transform.Find("Content/Inputs/Username")?.GetComponent<InputField>();
        m_Password = transform.Find("Content/Inputs/Password")?.GetComponent<InputField>();
        m_BtnLogin = transform.Find("Content/Inputs/BtnLogin")?.GetComponent<Button>();

        m_BtnLogin.onClick.AddListener(OnLoginClicked);
    }

    private void OnLoginClicked()
    {
        if (!string.IsNullOrEmpty(m_Username.text) && !string.IsNullOrEmpty(m_Password.text))
        {
            Debug.Log($"<color=yellow> Username: {m_Username.text} Password: {m_Password.text}</color>");

            if (CPELoginControl.Api != null)
                CPELoginControl.Api.Login(m_Username.text, m_Password.text);
        }
    }

    private void OnDestroy()
    {
        m_BtnLogin.onClick.RemoveAllListeners();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CPELoginControl.Api.Login("tester", "Aa123123!@#456");
        }
    }
#endif
}
