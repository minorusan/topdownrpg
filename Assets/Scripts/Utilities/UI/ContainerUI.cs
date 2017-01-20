﻿using Core.Characters.Player;
using Core.Gameplay.Interactivity;
using Core.Inventory;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class ContainerUI : MonoBehaviour
    {
        private static ContainerUI _instance;
        private Container _current;
        private Transform _bar;

        public ContainerItem Prefab;

        private void Awake()
        {
            _instance = this;
            _bar = transform.GetChild(0);
            gameObject.SetActive(false);
        }

        public static void ShowForContainer(Container cont)
        {
            var items = cont.Items;
            _instance.gameObject.SetActive(true);
            _instance.ShowForItems(cont);
            _instance.transform.position = cont.transform.position;
        }

        private void ShowForItems(Container cont)
        {
            _current = cont;
            var items = cont.Items;
            for (int i = 0; i < _bar.transform.childCount; i++)
            {
                Destroy(_bar.transform.GetChild(i).gameObject);
            }

            foreach (var id in items)
            {
                var newItem = Instantiate(Prefab, _bar);
                newItem.Item = id;
                newItem.OnItemSelected += OnItemSelected;
            }
        }

        private void Update()
        {
            if (Vector3.Distance(PlayerBehaviour.CurrentPlayer.transform.position, transform.position) > 10)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnItemSelected(Core.Inventory.AItemBase arg1, ContainerItem arg2)
        {
            PlayerInventory.Instance.TryAddItemToInventory(arg1);
            var templist = _current.Items.ToList();
            templist.Remove(arg1.ItemID);
            _current.Items = templist.ToArray();
            Destroy(arg2.gameObject);
        }
    }

}
