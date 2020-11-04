using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVilles
{
    class Player
    {
        public string pName;
        public int pMoney = 3;
        public Dices de = new Dices();

        public List<Cards> pVille = new List<Cards>() { new Cards(1), new Cards(3) };
        public List<Cards> pMain = new List<Cards>() { };



        public Player(string name) 
        {
            pName = name;
        }
        public void Pioche(Cards pioché)
        {
            pMain.Add(pioché);
        }
        public void Construire(Cards carte)
        {
            pVille.Add(carte);
            pMain.Remove(carte);
            pMoney -= carte.Cost;
        }
        public void Effets(int refCard)//les effets activés sur la ville du joueur lancant le dé
        {
            switch (refCard)
            {
                case 1:
                    pMoney++;
                    break;
                case 2:
                    pMoney+=2;
                    break;
                case 3:
                    pMoney += 2;
                    break;
                case 4:
                    //rien ici car les cartes rouge ne s'activent pas sur notre tour.
                    break;
                case 5:
                    pMoney += 3;
                    break;
                case 6:
                    pMoney++;
                    break;
                case 7://rien ici car les cartes rouge ne s'activent pas sur notre tour.
                    break;
                case 8:
                    pMoney += 4;
                    break;
            }
        }
        public void EffetsRougeBleu(int refCard, Player playerEnnemi)
        {
            switch (refCard)
            {
                case 1:
                    playerEnnemi.pMoney++;
                    break;
                case 2:
                    playerEnnemi.pMoney++;
                    break;
                case 3:
                    pMoney += 2;
                    break;
                case 4:
                    playerEnnemi.pMoney++;
                    pMoney--;
                    break;
                case 6:
                    playerEnnemi.pMoney++;
                    break;
                case 7:
                    playerEnnemi.pMoney += 2;
                    pMoney-=2;
                    break;
                case 8:
                    playerEnnemi.pMoney += 4;
                    break;
            }
        }


    }
}
