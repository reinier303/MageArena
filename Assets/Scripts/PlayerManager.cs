using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerManager : MonoBehaviour {

    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
        if (FirstPerson)
        {
            Player = PlayerFirstPerson;
        }
        else
        {
            Player = PlayerTopDown;
        }
        Player.SetActive(true);
    }

    #endregion

    public GameObject Player;

    public GameObject PlayerTopDown;
    public GameObject PlayerFirstPerson;

    private MouseLook mouseLook;

    float LookSensitivity;

    public bool FirstPerson;

    private void Start()
    {
        if(FirstPerson)
        {
            mouseLook = Player.GetComponent<PlayerFirstPerson>().m_MouseLook;

            LookSensitivity = mouseLook.XSensitivity;
        }

    }

    public void CursorLocker()
    {
        Cursor.visible = !Cursor.visible;
        mouseLook.lockCursor = !mouseLook.lockCursor;

        if (!mouseLook.lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            mouseLook.XSensitivity = 0;
            mouseLook.YSensitivity = 0;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            mouseLook.XSensitivity = LookSensitivity;
            mouseLook.YSensitivity = LookSensitivity;
            Time.timeScale = 1;
        }
    }
}
