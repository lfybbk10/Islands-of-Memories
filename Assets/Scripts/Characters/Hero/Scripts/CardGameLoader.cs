using Infrastructure;
using UnityEngine;
using UnityEngine.UI;


public class CardGameLoader : MonoBehaviour
{
    [SerializeField] private Camera hero_camera;
    [SerializeField] private Camera card_camera;
    [SerializeField] private Camera Ui_camera;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            hero_camera.enabled = false;
            Ui_camera.enabled = false;
            card_camera.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Ui_camera.enabled = true;
            card_camera.enabled = false;
            hero_camera.enabled = true;
        }
    }
}