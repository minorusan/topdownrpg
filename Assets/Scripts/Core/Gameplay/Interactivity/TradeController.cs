using Core.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UI;

namespace Core.Gameplay.Interactivity
{
    public class TradeController : MonoBehaviour, IPointerClickHandler
    {
        private GameObject[] _playerItemBars;
        private GameObject[] _vendorItemBars;

        private List<TradeItemUI> _instantiatedItems = new List<GameObject>();

        [Header("Containers")]
        public GameObject PlayerItemsContainer;
        public GameObject VendorItemsContainer;

        public TradeItemUI TradeItemPrefab;

        private void Awake()
        {
            _playerItemBars = new GameObject[PlayerItemsContainer.transform.childCount];
            _vendorItemBars = new GameObject[VendorItemsContainer.transform.childCount];

            for (int i = 0; i < PlayerItemsContainer.transform.childCount; i++)
            {
                _playerItemBars[i] = PlayerItemsContainer.transform.GetChild(i).gameObject;
                _vendorItemBars[i] = VendorItemsContainer.transform.GetChild(i).gameObject;
            }

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            foreach (var item in _instantiatedItems)
            {
                Destroy(item);
            }
            _instantiatedItems.Clear();
            InitPlayerItems();
        }

        private void OnDisable()
        {
           
        }

        private void InitPlayerItems()
        {
            var playerItems = PlayerInventory.Instance.GetItems();

            int counter = 0;
            for (int i = 0; i < playerItems.Length; i++)
            {
                var newItem = Instantiate<TradeItemUI>(TradeItemPrefab);
                newItem.transform.SetParent(_playerItemBars[counter].transform);
                newItem.InitWithItem(playerItems[i]);
                newItem.transform.localScale = Vector2.one;
                newItem.transform.localPosition = Vector2.zero;

                _instantiatedItems.Add(newItem);
                if (i != 0 && i % 6 == 0)
                {
                    counter++;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            gameObject.SetActive(false);
        }
    }
}