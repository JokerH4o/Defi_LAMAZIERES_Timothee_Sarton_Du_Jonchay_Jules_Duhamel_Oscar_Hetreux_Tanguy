using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Text;

namespace MiniVilles
{
    class Game
    {
        private int nbCartes;
        public string pName = "joris";
        public Piles piles;
        public List<Player> playerList = new List<Player>();

        Random rdm = new Random();


        public Game(int nbrJoueurs, int n)
        {
            this.nbCartes = n;
            for (int i = 1; i < nbrJoueurs + 1; i++)
            {
                pName = (Convert.ToString(i));
                playerList.Add(new Player(pName));
            }
            piles = new Piles(nbCartes);
        }

        public void Tour()
        {
            bool gagnant = false;
            while (gagnant == false)
            {
                for (int i = 0; i < playerList.Count; i++)//chaque joueurs font leurs tours à la suite en lancant un dé (joueur en question = joueur lancant le dé)
                {
                    if (i == 0)
                    {
                        playerList[i].de.Lancer();// le joueur en question lance
                        playerList[i].Pioche(piles.cartes.Pop());//le joueur en question pioche une carte

                        if (playerList[i].pVille.Contains(new Cards(playerList[i].de.face)))
                        {
                            foreach (Cards card in playerList[i].pVille)
                            {
                                if (card.RefCards == playerList[i].de.face)//si les cartes de la ville du joueur en question correspondent a la face du dé
                                {
                                    playerList[i].Effets(card.RefCards);
                                }
                            }
                        }
                        foreach (Player Enemie in playerList)
                        {
                            if (Enemie == playerList[i]) { }
                            else//Les joueurs autres que celui en question
                            {
                                if (Enemie.pVille.Contains(new Cards(playerList[i].de.face)))
                                {
                                    foreach (Cards card in Enemie.pVille)
                                    {
                                        if (card.RefCards == Enemie.de.face)
                                        {
                                            playerList[i].EffetsRougeBleu(card.RefCards, Enemie);
                                        }
                                    }
                                }
                            }
                        }

                        int tourAchatCount = 1;
                        while (tourAchatCount == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Voici votre main :");
                            foreach (Cards card in playerList[i].pMain)
                            {
                                Console.Write(card.CardNameEffect);
                                Console.Write(" - ");
                            }

                            int choixJoueur;
                            List<Cards> cartesConstruire = new List<Cards>();
                            while (true)
                            {
                                Console.WriteLine("Qu'elle carte voulez-vous poser ? (1, 2, 3, ...)");
                                choixJoueur = int.Parse(Console.ReadLine());
                                if (playerList[i].pMoney >= playerList[i].pMain[choixJoueur - 1].Cost)
                                    break;
                                Console.WriteLine("choisir une valable.");
                            }
                            cartesConstruire.Add(playerList[i].pMain[choixJoueur - 1]);
                            playerList[i].pMain.RemoveAt(choixJoueur - 1);

                            Console.WriteLine("Voulez vous posé une autre cartes ? \n 1 - oui  2 - non");
                            tourAchatCount = int.Parse(Console.ReadLine());

                            foreach (Cards card in cartesConstruire)
                            {
                                playerList[i].Construire(card);
                            }
                        }
                    }
                    else
                    {
                        playerList[i].de.Lancer();// le joueur en question lance
                        playerList[i].Pioche(piles.cartes.Pop());//le joueur en question pioche une carte

                        //[appliquer le lancement du dé]

                        if (playerList[i].pVille.Contains(new Cards(playerList[i].de.face)))
                        {
                            foreach (Cards card in playerList[i].pVille)
                            {
                                if (card.RefCards == playerList[i].de.face)//si les cartes de la ville du joueur en question correspondent a la face du dé
                                {
                                    playerList[i].Effets(card.RefCards);
                                }
                            }
                        }
                        foreach (Player Enemie in playerList)
                        {
                            if (Enemie == playerList[i]) { }
                            else//Les joueurs autres que celui en question
                            {
                                if (Enemie.pVille.Contains(new Cards(playerList[i].de.face)))
                                {
                                    foreach (Cards card in Enemie.pVille)
                                    {
                                        if (card.RefCards == Enemie.de.face)
                                        {
                                            playerList[i].EffetsRougeBleu(card.RefCards, Enemie);
                                        }
                                    }
                                }
                            }
                        }

                        int tourAchatCount = 1;
                        while (tourAchatCount == 1)
                        {
                            int choixIa;
                            List<Cards> cartesConstruire = new List<Cards>();

                            while (true)
                            {
                                choixIa = rdm.Next(1, playerList[i].pMain.Count + 1);
                                if (playerList[i].pMoney >= playerList[i].pMain[choixIa - 1].Cost)
                                    break;
                            }
                            cartesConstruire.Add(playerList[i].pMain[choixIa - 1]);
                            playerList[i].pMain.RemoveAt(choixIa - 1);

                            tourAchatCount = rdm.Next(1, 3);

                            foreach (Cards card in cartesConstruire)
                            {
                                playerList[i].Construire(card);
                            }
                        }
                    }

                }
                foreach (Player player in playerList)
                {
                    if (player.pMoney >= 20)
                    {
                        gagnant = true;
                    }
                }

            }
        }
    }
}
