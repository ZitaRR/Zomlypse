using System.Collections;
using UnityEngine;

namespace Zomlypse.Behaviours
{
    public class CardDeck : MonoBehaviour
    {
        [SerializeField]
        private RectTransform card;

        private RectTransform deck;
        private Vector2 origin;
        private float deckWidth;
        private float cardWidth;

        private void Awake()
        {
            deck = GetComponent<RectTransform>();
        }

        private void Start()
        {
            StartCoroutine(WaitForFrame());
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                AddCard();
            }
        }

        private IEnumerator WaitForFrame()
        {
            yield return new WaitForEndOfFrame();

            deckWidth = deck.rect.width;
            cardWidth = card.rect.width;

            card.sizeDelta = new Vector2(deck.rect.height * .75f, 0f);
            origin = new Vector2(deck.rect.min.x + cardWidth / 2, 0f);
        }

        private void AdjustCards()
        {
            int cards = transform.childCount;
            float spacing = (deckWidth - cardWidth) / (cards - 1);
        
            for (int i = 1; i < cards; i++)
            {
                RectTransform current = transform.GetChild(i).GetComponent<RectTransform>();
                current.anchoredPosition = new Vector2(origin.x + spacing * i, origin.y);
            }
        }

        public void AddCard()
        {
            Instantiate(card, deck);
            AdjustCards();
        } 
    }
}