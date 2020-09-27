using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    string name;
    string surname;
    string date;
    string spouse;
    string ex_spouse;

    List<Node> parents;
    List<Node> siblings;
    List<Node> kids;
}
