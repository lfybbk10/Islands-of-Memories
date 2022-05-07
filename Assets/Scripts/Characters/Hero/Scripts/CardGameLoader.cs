using Infrastructure;
using UnityEngine;
using UnityEngine.UI;


public class CardGameLoader : MonoBehaviour
{
    [SerializeField] private GameObject card_camera;
    [SerializeField] private GameObject Ui_camera;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Ui_camera.SetActive(false);
            card_camera.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {   
            Ui_camera.SetActive(true);
            card_camera.SetActive(false);
        }
    }
}

