using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CountryFlags
{
    public class CountryFlags_Example : MonoBehaviour
    {
        public CountryFlag_UIEntry countryFlagUiEntryPrefab;
        public ScrollRect flagsScrollRect;

        public bool showCountriesWithNoNames;
        
        private List<CountryFlag_UIEntry> _uiEntries = new List<CountryFlag_UIEntry>();

        private void Start()
        {
            SpawnAllEntries();
        }

        public void SpawnAllEntries()
        {
            var countryEntries = CountryFlags.Flags;

            while (flagsScrollRect.content.childCount > 0)
            {
                DestroyImmediate(flagsScrollRect.content.GetChild(0).gameObject);
            }
            
            _uiEntries.Clear();

            for (int i = 0; i < countryEntries.Count; i++)
            {
                var entry = Instantiate(countryFlagUiEntryPrefab, flagsScrollRect.content);
                entry.Init(countryEntries[i]);

                _uiEntries.Add(entry);
            }
            
            SetEntriesVisible(_uiEntries);
        }

        void SetEntriesVisible(List<CountryFlag_UIEntry> entries)
        { 
            foreach (var countryFlagUiEntry in _uiEntries)
            {
                bool visibleIfNoName = showCountriesWithNoNames || countryFlagUiEntry.FlagEntry.HasName;
                countryFlagUiEntry.Visible = entries.Contains(countryFlagUiEntry) && visibleIfNoName;
            }
        }

        public void UI_Search(string query)
        {
            var searchResult = CountryFlags.Flags.Search(query);
            SetEntriesVisible(_uiEntries.FindAll(x => searchResult.Contains(x.FlagEntry)));

            flagsScrollRect.verticalNormalizedPosition = 1f;
        }
    }
}