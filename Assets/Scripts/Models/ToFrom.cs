using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class ToFrom
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int ToID { get; set; }
    public int FromID { get; set; }
    public string Direction { get; set; }
}

