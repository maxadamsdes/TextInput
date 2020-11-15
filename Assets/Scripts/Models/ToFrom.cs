using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class ToFrom
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string ToID { get; set; }
    public string FromID { get; set; }
    public string Direction { get; set; }
}

