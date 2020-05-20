using UnityEngine;

namespace CountryFlags
{
    [System.Serializable]
    public class CountryFlagEntry
    {
        public string code;
        public string name;
        public string nameRU;
        public Sprite flag;

        public bool HasName => !string.IsNullOrEmpty(name);
    }
}
