using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public string first_Name = "";
    public string surname = "";
    public string date = "";
    public string spouse = "";

    public Text buttonText;
    public Text spouseText;
    public Text exSpouseText;
    public Text parentsText;
    public Text siblingsText;
    public Text childrenText;

    public List<string> ex_spouse = new List<string>();
    public List<string> parents = new List<string>();
    public List<string> siblings = new List<string>();
    public List<string> children = new List<string>();

    public SingleNode nodeToSave = new SingleNode();

    private void Awake()
    {
        FamilyTree.SaveInitiated += Save;
    }

    public void AddName(InputField inputField)
    {
        first_Name = inputField.text;
        UpdateButtonText();
    }

    public void AddSurname(InputField inputField)
    {
        surname = inputField.text;
        UpdateButtonText();
    }

    public void AddDate(InputField inputField)
    {
        date = inputField.text;
        UpdateButtonText();
    }

    public void UpdateButtonText()
    {
        if (first_Name == "" && surname == "" && date == "")
        {
            buttonText.fontSize = 36;
            buttonText.text = "Click To Open Panel";
        }
        else
        {
            buttonText.fontSize = 26;
            buttonText.text = first_Name + ' ' + surname + "\n" + date;
        }
    }

    public void AddSpouse(InputField inputField)
    {
        spouse = inputField.text;
        spouseText.text = spouse + " ";
    }

    public void AddExSpouse(InputField inputField)
    {
        if (inputField.text != "")
        {
            ex_spouse.Add(inputField.text);
            exSpouseText.text = "";

            if (ex_spouse.Count != 0)
            {
                foreach (string ex_spouse_el in ex_spouse)
                {
                    exSpouseText.text += ex_spouse_el + ", ";
                }
            }
        }
    }

    public void AddParent(InputField inputField)
    {
        if (inputField.text != "")
        {
            parents.Add(inputField.text);
            parentsText.text = "";
            if (parents.Count != 0)
            {
                foreach (string parents_el in parents)
                {
                    parentsText.text += parents_el + ", ";
                }
            }
        }
    }

    public void AddSibling(InputField inputField)
    {
        if (inputField.text != "")
        {
            siblings.Add(inputField.text);
            siblingsText.text = "";
            if (siblings.Count != 0)
            {
                foreach (string siblings_el in siblings)
                {
                    siblingsText.text += siblings_el + ", ";
                }
            }
        }
    }

    public void AddChild(InputField inputField)
    {
        if (inputField.text != "")
        {
            children.Add(inputField.text);
            childrenText.text = "";
            if (children.Count != 0)
            {
                foreach (string children_el in children)
                {
                    childrenText.text += children_el + ", ";
                }
            }
        }
    }

    public void DeleteSpouse()
    {
        spouse = "";
        spouseText.text = spouse;
    }

    public void DeleteExSpouse()
    {
        if (ex_spouse.Count != 0)
        {
            ex_spouse.Remove(ex_spouse[ex_spouse.Count - 1]);
            exSpouseText.text = "";

            foreach (string ex_spouse_el in ex_spouse)
            {
                exSpouseText.text += ex_spouse_el + ", ";
            }
        }
    }

    public void DeleteParent()
    {
        if (parents.Count != 0)
        {
            parents.Remove(parents[parents.Count - 1]);
            parentsText.text = "";

            foreach (string parents_el in parents)
            {
                parentsText.text += parents_el + ", ";
            }
        }
    }

    public void DeleteSibling()
    {
        if (siblings.Count != 0)
        {
            siblings.Remove(siblings[siblings.Count - 1]);
            siblingsText.text = "";

            foreach (string siblings_el in siblings)
            {
                siblingsText.text += siblings_el + ", ";
            }
        }
    }

    public void DeleteChild()
    {
        if (children.Count != 0)
        {
            children.Remove(children[children.Count - 1]);
            childrenText.text = "";

            foreach (string children_el in children)
            {
                childrenText.text += children_el + ", ";
            }
        }
    }

    public void AddItemsToList(List<string> strings)
    {
        foreach (string mystring in strings)
        {
            
        }
    }

    public void AddItemsToSave()
    {
        nodeToSave.first_Name = first_Name;
        nodeToSave.surname = surname;
        nodeToSave.date = date;
        nodeToSave.spouse = spouse;

        nodeToSave.parents = parents;
        nodeToSave.siblings = siblings;
        nodeToSave.ex_spouse = ex_spouse;
        nodeToSave.children = children;

        nodeToSave.stringToSave = "*Name: " + first_Name + " *Surname: " + surname + " *Date: " + date + " *Parents: " + string.Join(", ", parents) + " *Siblings: " + string.Join(",", siblings) + " *Spouse: " + spouse + " *Ex_spouse: " + string.Join(",", ex_spouse) + " *Children: " + string.Join(",", children);
    }

    public void Save()
    {
        AddItemsToSave();
        SaveLoad.Save<string>(nodeToSave.first_Name , "Name");
        SaveLoad.Save<string>(nodeToSave.surname , "Surname");
        SaveLoad.Save<string>(nodeToSave.date , "Date");
        SaveLoad.Save<List<string>>(nodeToSave.parents , "Parents");
        SaveLoad.Save<List<string>>(nodeToSave.siblings , "Sibling");
        SaveLoad.Save<string>(nodeToSave.spouse , "Spouse");
        SaveLoad.Save<List<string>>(nodeToSave.ex_spouse , "EXSpouse");
        SaveLoad.Save<List<string>>(nodeToSave.children , "Children");
        SaveLoad.Save<string>(nodeToSave.stringToSave, "LastProcessedTreeNode");
    }

    public void Load()
    {
        if (SaveLoad.SaveExists("Shapes"))
        {
            AddItemsToList(SaveLoad.Load<List<string>>("Nodes"));
        }
    }
}

