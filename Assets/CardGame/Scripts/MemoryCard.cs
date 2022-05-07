using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBlack;
    [SerializeField] private SceneController controller;
    private int _id;

    public int id
    {
        get { return _id; }
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    
    public void OnMouseDown()
    {
        if (cardBlack.activeSelf && controller.canReveal)
        {
            cardBlack.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        cardBlack.SetActive(true);
    }
}