using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleNode
{
    [SerializeField]
    public string first_Name = "";
    [SerializeField]
    public string surname = "";
    [SerializeField]
    public string date = "";
    [SerializeField]
    public string spouse = "";

    [SerializeField]
    public List<string> ex_spouse = new List<string>();
    [SerializeField]
    public List<string> parents = new List<string>();
    [SerializeField]
    public List<string> siblings = new List<string>();
    [SerializeField]
    public List<string> children = new List<string>();

    [SerializeField]
    public string stringToSave = "";
}