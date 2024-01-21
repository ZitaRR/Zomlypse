using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

namespace Zomlypse.Behaviours
{
    public class CardDeck : MonoBehaviour
    {
        [SerializeField]
        private RectTransform cardRect;

        private RectTransform deck;
        private List<RectTransform> cards = new List<RectTransform>();
        private float deckWidth;
        private float cardWidth;
        private bool initialized;
        private HorizontalLayoutGroup horizontalLayout;

        private void Awake()
        {
            deck = GetComponent<RectTransform>();
            horizontalLayout = GetComponent<HorizontalLayoutGroup>();
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

            initialized = true;
        }

        private void AdjustCards()
        {
            int cards = this.cards.Count;
            float width = cardWidth * cards;
            if (width <= deckWidth)
            {
                horizontalLayout.spacing = 0;
                return;
            }

            float diff = width - deckWidth;
            float spacing = diff / (cards - 1);
            horizontalLayout.spacing = -spacing;
        }

        private IEnumerator EnsureInitialization()
        {
            if (initialized)
            {
                yield return null;
            }

            yield return new WaitUntil(() => initialized == true);
        }

        private IEnumerator AddCards(params Entity[] entities)
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

        public void Add(params Entity[] entites)
        {
            StartCoroutine(AddCards(entites));
        }
    }
}