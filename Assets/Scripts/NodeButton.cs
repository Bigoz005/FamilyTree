﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeButton : MonoBehaviour
{
    public void OpenPanel()
    {
        transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
    }
}
