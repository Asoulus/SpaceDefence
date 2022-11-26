using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    protected GameObject _hover;

    [SerializeField]
    protected GameObject _selector;

    protected bool isSelected = false;

    public void Hovering()
    {
        if (_hover != null)
        {
            _hover.SetActive(true);
        }
    }

    public void Leaving()
    {
        if (_hover != null)
        {
            _hover.SetActive(false);
        }
    }

    public void Return()
    {
        //MenuEventHandler.instance.MainMenuButtonPressed(-1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public async void Pressed()
    {
        if (!_selector)
        {
            Debug.Log("HUH?");
            return;
        }

        isSelected = !isSelected;
        _selector.SetActive(!_selector.activeSelf);

        await Task.Delay(100);

        isSelected = !isSelected;
        _selector.SetActive(!_selector.activeSelf);     
    }
}
