using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void OpenPanel(GameObject panel)
    {
        Time.timeScale = 0;
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }
}
