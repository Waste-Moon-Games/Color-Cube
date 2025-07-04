﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Player
{
    public static class PlayerWallet
    {
        private static int _wallet;
        private static int _increaceRate;

        private static int _minIncreaceRate = 1;
        private static int _maxIncreaceRate = 5;

        public static int Wallet
        {
            get {  return _wallet; }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Wallet), "Wallet cannot be negative");

                if(_wallet != value)
                    _wallet = value;
            }
        }
        public static int IncreaceRate
        {
            get
            {
                return _increaceRate;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(IncreaceRate),"Increase Rate cannot be negative");

                _increaceRate = value;
            }
        }

        public static event Action<int, int> WalletChanged;

        public static void Add()
        {
            IncreaceRate = Random.Range(_minIncreaceRate, _maxIncreaceRate);

            Wallet += IncreaceRate;

            WalletChanged?.Invoke(Wallet, IncreaceRate);
        }

        public static void Spend(int value)
        {
            if(value < 0)
                return;

            Wallet -= value;

            WalletChanged?.Invoke(Wallet, -value);
        }

        public static void SaveWallet()
        {
            PlayerPrefs.SetInt(PlayerPrefsConsts.WALLET, Wallet);
            PlayerPrefs.Save();
        }

        public static void LoadWallet()
        {
            Wallet = PlayerPrefs.GetInt(PlayerPrefsConsts.WALLET);
        }

        public static int GetWallet()
        {
            return Wallet;
        }
    }
}