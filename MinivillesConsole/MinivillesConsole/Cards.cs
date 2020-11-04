using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVilles
{
    class Cards
    {
        public int Cost;
        public string CardNameEffect;
        public int ActivationNbr;
        public System.ConsoleColor Color;
        public int RefCards;
        

        public Cards(int refcard)
        {
            RefCards = refcard;
            switch(refcard)
            {
                case 1:
                    ChampsDeBlé();
                    break;
                case 2:
                    Ferme();
                    break;
                case 3:
                    Boulangerie();
                    break;
                case 4:
                    Café();
                    break;
                case 5:
                    Superette();
                    break;
                case 6:
                    Forêt();
                    break;
                case 7:
                    Restaurant();
                    break;
                case 8:
                    Stade();
                    break;
            }
        }

        public void ChampsDeBlé()
        {
            ActivationNbr = 1;
            Color = ConsoleColor.Blue;
            CardNameEffect = "Champs de blé : Recevez 1 pièce";
            Cost = 1;

        }
        public void Ferme()
        {
            ActivationNbr = 1;
            Color = ConsoleColor.Blue;
            CardNameEffect = "Ferme : Recevez 1 pièce";
            Cost = 2;
        }
        public void Boulangerie()
        {
            ActivationNbr = 2;
            Color = ConsoleColor.Green;
            CardNameEffect = "Boulangerie : Recevez 2 pièces";
            Cost = 1;
        }
        public void Café()
        {
            ActivationNbr = 3;
            Color = ConsoleColor.Red;
            CardNameEffect = "Café : Recevez 1 pièce du joueur qui a lancé le dé";
            Cost = 2;
        }
        public void Superette()
        {
            ActivationNbr = 4;
            Color = ConsoleColor.Green;
            CardNameEffect = "Superette : Recevez 3 pièces";
            Cost = 2;
        }
        public void Forêt()
        {
            ActivationNbr = 5;
            Color = ConsoleColor.Blue;
            CardNameEffect = "Forêt : Recevez 1 pièce";
            Cost = 2;
        }
        public void Restaurant()
        {
            ActivationNbr = 5;
            Color = ConsoleColor.Red;
            CardNameEffect = "Restaurant : Recevez 2 pièces du joueur qui a lancé le dé ";
            Cost = 4;
        }
        public void Stade()
        {
            ActivationNbr = 6;
            Color = ConsoleColor.Blue;
            CardNameEffect = "Stade : Recevez 4 pièce";
            Cost = 6;
        }
        
    }
}
