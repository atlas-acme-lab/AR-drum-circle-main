﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelMovement : MonoBehaviour
{
    public GameObject MenuOrigPos;
    public GameObject MenuActivePos;
    public GameObject MenuPanel;
    public GameObject menuIcon;
    public GameObject loopPanel;

    public bool Move_Menu_Panel;
    public bool Move_Menu_Panel_Back;

    public float moveSpeed;
    public float tempMenuPos;
    void Start()
    {
        MenuPanel.transform.position = MenuOrigPos.transform.position;
    }

    void Update()
    {
        if(Move_Menu_Panel)
        {
            MenuPanel.transform.position = Vector3.Lerp(MenuPanel.transform.position, MenuActivePos.transform.position, moveSpeed * Time.deltaTime);

            if(MenuPanel.transform.localPosition.x == tempMenuPos)
            {
                Move_Menu_Panel = false;
                MenuPanel.transform.position = MenuActivePos.transform.position;
                tempMenuPos = -99999999f;
            }
            if(Move_Menu_Panel)
            {
                tempMenuPos = MenuPanel.transform.position.x;
            }
        }

        if(Move_Menu_Panel_Back)
        {
            MenuPanel.transform.position = Vector3.Lerp(MenuPanel.transform.position, MenuOrigPos.transform.position, moveSpeed * Time.deltaTime);

            if (MenuPanel.transform.localPosition.x == tempMenuPos)
            {
                Move_Menu_Panel_Back = false;
                MenuPanel.transform.position = MenuOrigPos.transform.position;
                tempMenuPos = -99999999f;
            }
            if (Move_Menu_Panel_Back)
            {
                tempMenuPos = MenuPanel.transform.position.x;
            }
        }
    }

    public void MovePanel()
    {
        menuIcon.SetActive(false);
        Move_Menu_Panel_Back = false;
        Move_Menu_Panel = true;
    }

    public void MovePanelBack()
    {
        menuIcon.SetActive(true);
        Move_Menu_Panel_Back = true;
        Move_Menu_Panel = false;
    }

    public void UnhideUI()
    {
        //loopPanel.SetActive(true);
        menuIcon.SetActive(true);
    }
}

