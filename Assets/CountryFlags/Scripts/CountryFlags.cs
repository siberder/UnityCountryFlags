using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CountryFlags
{
    public static class CountryFlags
    {
        const string SpriteResourcesPath = "CountryFlags/flags";
        const string DataResourcesPath = "CountryFlags/flagsData";

        private static List<CountryFlagEntry> countryFlagEntries;
        private static List<Sprite> flagsSprites = new List<Sprite>();

        public static List<CountryFlagEntry> Flags
        {
            get
            {
                if (countryFlagEntries == null)
                {
                    ParseCountryEntries();
                }
                
                return countryFlagEntries;
            }
            set => countryFlagEntries = value;
        }

        static void LoadSprites()
        {
            flagsSprites = Resources.LoadAll<Sprite>(SpriteResourcesPath).ToList();
            flagsSprites.Sort((x, y) => x.name.GetLastNum() - y.name.GetLastNum());
        }

        static void ParseCountryEntries()
        {
            LoadSprites();

            var flagsToCountries = Resources.Load<TextAsset>(DataResourcesPath);
            Flags = JsonHelper.FromJson<CountryFlagEntry>(flagsToCountries.text).ToList();
            for (var i = 0; i < Flags.Count; i++)
            {
                Flags[i].flag = flagsSprites[i];
            }
        }
    }
}