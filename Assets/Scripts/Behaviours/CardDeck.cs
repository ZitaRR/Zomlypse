using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zomlypse.Behaviours
{
    public class CardDeck : MonoBehaviour
    {
        [SerializeField]
        private RectTransform cardRect;

        private RectTransform deck;
        private List<RectTransform> cards = new List<RectTransform>();
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

        private IEnumerator WaitForFrame()
        {
            yield return new WaitForEndOfFrame();

            deckWidth = deck.rect.width;
            cardWidth = cardRect.rect.width;

            cardRect.sizeDelta = new Vector2(deck.rect.height * .75f, 0f);
            origin = new Vector2(deck.rect.min.x + cardWidth / 2, 0f);
        }

        private void AdjustCards()
        {
            int cards = this.cards.Count;
            float spacing = (deckWidth - cardWidth) / (cards - 1);
        
            for (int i = 1; i < cards; i++)
            {
                RectTransform current = this.cards[i];
                current.anchoredPosition = new Vector2(origin.x + spacing * i, origin.y);
            }
        }

        public void AddCard(Entity entity)
        {
            RectTransform rect = Instantiate(cardRect, deck);
            rect.GetComponent<Card>().Apply(entity.Appearance);
            cards.Add(rect);
            AdjustCards();
        } 
    }
}