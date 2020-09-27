using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node:MonoBehaviour
{
    public string first_Name;
    public string surname;
    public string date;
    public string spouse;

    public List<string> ex_spouse = new List<string>();
    public List<string> parents = new List<string>();
    public List<string> siblings = new List<string>();
    public List<string> children = new List<string>();

    public void AddName(InputField inputField)
    {
        first_Name = inputField.text;
    }

    public void AddSurname(InputField inputField)
    {
        surname = inputField.text;
    }

    public void AddDate(InputField inputField)
    {
        date = inputField.text;
    }

    public void AddSpouse(InputField inputField)
    {
        spouse = inputField.text;
    }

    public void AddExSpouse(InputField inputField)
    {
        ex_spouse.Add(inputField.text);
        
    }

    public void AddParent(InputField inputField)
    {
        parents.Add(inputField.text);

    }

    public void AddSibling(InputField inputField)
    {
        siblings.Add(inputField.text);

    }

    public void AddChild(InputField inputField)
    {
        children.Add(inputField.text);

    }

    public void DeleteSpouse()
    {
        spouse = "";

    }

    public void DeleteExSpouse()
    {
        ex_spouse.Remove(ex_spouse[ex_spouse.Count-1]);

    }

    public void DeleteParent()
    {
        parents.Remove(parents[parents.Count - 1]);

    }

    public void DeleteSibling()
    {
        siblings.Remove(siblings[siblings.Count - 1]);

    }

    public void DeleteChild()
    {
        children.Remove(children[children.Count - 1]);

    }
}
