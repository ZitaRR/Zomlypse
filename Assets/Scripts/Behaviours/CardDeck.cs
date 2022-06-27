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
        private bool initialized;

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
                StartCoroutine(AddCard(GameManager.Instance.Player));
            }
        }

        private IEnumerator WaitForFrame()
        {
            yield return new WaitForEndOfFrame();

            deckWidth = deck.rect.width;
            cardWidth = cardRect.rect.width;

            cardRect.sizeDelta = new Vector2(deck.rect.height * .75f, 0f);
            origin = new Vector2(deck.rect.min.x + cardWidth / 2, 0f);

            initialized = true;
        }

        private void AdjustCards()
        {
            int cards = this.cards.Count;
            float spacing = (deckWidth - cardWidth) / (Mathf.Clamp(cards - 1, 1, float.MaxValue));
        
            for (int i = 0; i < cards; i++)
            {
                RectTransform current = this.cards[i];
                current.anchoredPosition = new Vector2(origin.x + spacing * i, origin.y);
            }
        }

        private IEnumerator EnsureInitialization()
        {
            if (initialized)
            {
                yield return null;
            }

            yield return new WaitUntil(() => initialized == true);
        }

        public IEnumerator AddCard(params Entity[] entities)
        {
            yield return StartCoroutine(EnsureInitialization());

            for (int i = 0; i < entities.Length; i++)
            {
                RectTransform rect = Instantiate(cardRect, deck);
                rect.GetComponent<Card>().Apply(entities[i].Appearance);
                cards.Add(rect);
                AdjustCards();
            }
        } 
    }
}