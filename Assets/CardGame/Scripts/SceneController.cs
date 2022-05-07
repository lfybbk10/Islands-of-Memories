using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class SceneController : MonoBehaviour
{
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;

    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;


    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;


    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            bool amogus = false;
        }
        else
        {
            yield return new WaitForSeconds(0.7f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        (_firstRevealed, _secondRevealed) = (null, null);
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = UnityEngine.Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    private void Start()
    {
        Vector3 startPos = originalCard.transform.position;
        int[] numbers = new[] {0, 0, 1, 1, 2, 2, 3, 3,};
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if (j == 0 && i == 0) card = originalCard;
                else
                {
                    card = Instantiate(originalCard);
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    } 
    
}