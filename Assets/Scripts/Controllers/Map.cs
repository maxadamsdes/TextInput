﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public Dropdown dropdown;

    public void Start()
    {
        List<int> LocationList = new List<int>() { 0, 1, 2, 3, 4, 5, 6};
        GameModel.ds.jsnGetPlayerLocations(LocationList);
    }
    public void jsnPlayerLocationListReceiverDel(List<Player> pReceivedList)
    {
        List<string> names = new List<string>();
        Player p = pReceivedList[0];
        foreach(Player i in pReceivedList)
        {
            names.Add(i.PlayerName);
        }
        
        switch(p.LocationId)
        {
            case 0:
                dropdown = GameObject.Find("DropdownForest").GetComponent<Dropdown>();
                break;
            case 1:
                dropdown = GameObject.Find("DropdownCave").GetComponent<Dropdown>();
                break;
            case 2:
                dropdown = GameObject.Find("DropdownBeach").GetComponent<Dropdown>();
                break;
            case 3:
                dropdown = GameObject.Find("DropdownHighway").GetComponent<Dropdown>();
                break;
            case 4:
                dropdown = GameObject.Find("DropdownCave2").GetComponent<Dropdown>();
                break;
            case 5:
                dropdown = GameObject.Find("DropdownRiver").GetComponent<Dropdown>();
                break;
            case 6:
                dropdown = GameObject.Find("DropdownOcean").GetComponent<Dropdown>();
                break;
            default:
                break;
        }
        dropdown.AddOptions(names);
    }
    public void populateDropdown(Player pPlayer)
    {
        
    }
}