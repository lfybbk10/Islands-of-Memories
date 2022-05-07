using Infrastructure;
using UnityEngine;
using UnityEngine.UI;


public class CardGameLoader : MonoBehaviour
{
    [SerializeField] private GameObject card_camera;
    [SerializeField] private GameObject Ui_camera;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
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

/*
using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
public Camera[] playercam;
int i=0;
int dlina;
        void Update () {
                if (Input.GetKeyDown("c")) {
                                i=i+1;
                                        if (i >= dlina){i=0;}}
                dlina=playercam.Length;
                playercam[i].enabled = true;
                playercam[i-1].enabled = false;
                        if (i==0)
                        playercam[dlina].enabled = false;
        }
}
*/