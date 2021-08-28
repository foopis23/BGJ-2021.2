using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardHudController : MonoBehaviour
{
    // editor fields
    public int numCards;
    public float cardOffset;
    public float cardSelectTimeout;
    public float cardSlideSpeed;
    public float cardSlidePos;
    public Player player;
    public GameObject heldCardsObject;
    public GameObject newCard;
    public GameObject cardPrefab;

    // private fields
    private CardInventory cardInventory;
    private RawImage[] heldCards;
    private int selectedCard;
    private float selectTime;

    private static Dictionary<KeyCode, int> KeyCodeCardIndices = new Dictionary<KeyCode, int>() {
        { KeyCode.Alpha1, 0 },
        { KeyCode.Alpha2, 1 },
        { KeyCode.Alpha3, 2 },
        { KeyCode.Alpha4, 3 },
        { KeyCode.Alpha5, 4 },
    };

    void Start()
    {
        cardInventory = player.Inventory;
        heldCards = new RawImage[numCards];
        selectedCard = -1;
        newCard.SetActive(false);

        for(int i = 0; i < numCards; i++)
        {
            var obj = Instantiate(cardPrefab, heldCardsObject.transform);
            var rawImage = obj.GetComponent<RawImage>();
            rawImage.rectTransform.anchoredPosition = new Vector3(cardOffset * i, cardSlidePos, 0f);
            heldCards[i] = rawImage;
        }
    }

    void Update()
    {
        foreach(KeyCode keyCode in KeyCodeCardIndices.Keys)
        {
            if(Input.GetKeyDown(keyCode))
            {
                selectedCard = KeyCodeCardIndices[keyCode];
                selectTime = Time.time;
            }
        }

        if(Time.time - selectTime > cardSelectTimeout)
        {
            selectedCard = -1;
        }

        for(int i = 0; i < heldCards.Length; i++)
        {
            var heldCard = heldCards[i];
            if(cardInventory.Cards[i] == null)
            {
                heldCard.gameObject.SetActive(false);
                continue;
            }
            else
            {
                var cardData = cardInventory.Cards[i];
                heldCard.gameObject.SetActive(true);
                heldCard.gameObject.transform.Find("Icon").GetComponent<RawImage>().texture = cardData.Icon;
                heldCard.gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = cardData.Name;
                heldCard.gameObject.transform.Find("Info Text").GetComponent<TextMeshProUGUI>().text = "poopie stinkyyy"; // TODO: set info text
                heldCard.gameObject.transform.Find("Chaos Percent").GetComponent<TextMeshProUGUI>().text = ((int) (cardData.ChaosLevel * 100)).ToString() + "%";
            }

            int slideDir = i == selectedCard ? 1 : -1;
            float currentY = heldCard.rectTransform.anchoredPosition.y;
            if(currentY > -0.001f && slideDir == 1)
            {
                currentY = 0;
            }
            else if(currentY < cardSlidePos + 0.001f && slideDir == -1)
            {
                currentY = cardSlidePos;
            }
            else
            {
                currentY += slideDir * cardSlideSpeed * Time.deltaTime;
                if(currentY > -0.001f)
                {
                    currentY = 0;
                }
                else if(currentY < cardSlidePos + 0.001f)
                {
                    currentY = cardSlidePos;
                }

                heldCard.rectTransform.anchoredPosition = new Vector3(heldCard.rectTransform.anchoredPosition.x, currentY, 0f);
            }

        }
    }
}
