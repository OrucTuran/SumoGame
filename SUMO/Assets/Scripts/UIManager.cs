using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PauseUI PauseUI;

    public void ShowPauseUI(bool value)
    {
        if (value) PauseUI.Show();
        else PauseUI.Hide();
    }
}
