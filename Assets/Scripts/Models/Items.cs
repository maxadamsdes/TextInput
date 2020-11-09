using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class Items
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public int LocationID { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }

}
