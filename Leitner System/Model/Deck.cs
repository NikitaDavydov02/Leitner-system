﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Leitner_System.Model
{
    /// <summary>
    /// Keep cards and supportive information
    /// </summary>
    [DataContract]
    public class Deck
    {
        //--------------------------------------------------------------------------------
        //------------------------------------- DATA MEMBERS ---------------------------------
        //--------------------------------------------------------------------------------
        [DataMember]
        public List<Card> Cards { get; private set; }
        [DataMember]
        public string Name { get; private set; }
        //--------------------------------------------------------------------------------
        //------------------------------------- METHODS ---------------------------------
        //--------------------------------------------------------------------------------
        /// <summary>
        /// Create new deck with defined name and empty cards
        /// </summary>
        /// <param name="name">Name of this deck</param>
        public Deck(string name)
        {
            Cards = new List<Card>();
            Name = name;
        }
        /// <summary>
        /// Return string with name of this deck
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
        /// <summary>
        /// Set this deck as parent to every child card
        /// </summary>
        public void SetParentDeckToCards()
        {
            foreach (Card card in Cards)
                card.SetPerentDeck(this);
        }
        /// <summary>
        /// Change name of this deck
        /// </summary>
        /// <param name="newName">New name of this deck</param>
        public void Rename(string newName)
        {
            if (!FileManager.UpdateNameOfDeckDeckFileAndDeckFolder(newName, Name))
                return;
            Name = newName;
            FileManager.SaveDeckOrUpdateDeckFile(this);
        }
        /// <summary>
        /// Create new default card in this deck and return it
        /// </summary>
        public Card CreateNewCard()
        {
            Card newCard = new Card(this, "Question?", "Answer");
            Cards.Add(newCard);
            FileManager.SaveDeckOrUpdateDeckFile(this);
            return newCard;
        }
        /// <summary>
        /// Delete determined cards from list from this deck and save deck after this
        /// </summary>
        /// <param name="card">Card to delete</param>
        public void DeleteSelectedCard(List<int> indexesOfCardToDelete)
        {
            List<Card> cardsToRemove = new List<Card>();
            foreach (int i in indexesOfCardToDelete)
                cardsToRemove.Add(Cards[i]);
            int removingIndex;
            foreach (Card card in cardsToRemove)
            {
                removingIndex = Cards.IndexOf(card);
                Cards.RemoveAt(removingIndex);
            }
            FileManager.SaveDeckOrUpdateDeckFile(this);
        }
        //public void DeleteSelectedCards(List<Card> CardsToDelete)
        //{
        //    foreach (Card card in CardsToDelete)
        //    {
        //        int removingIndex = Cards.IndexOf(card);
        //        Cards.RemoveAt(removingIndex);
        //    }
        //    FileManager.SaveDeckOrUpdateDeckFile(this);
        //}
    }
}
