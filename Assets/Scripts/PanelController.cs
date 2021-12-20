using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public void Open(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void Close(GameObject panel)
    {
        panel.SetActive(false);
    }
}
