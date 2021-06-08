using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TbWay : MonoBehaviour
{
    public Button _button;
    public void ActivateButton()
    {
        _button.onClick.Invoke();
    }
}
