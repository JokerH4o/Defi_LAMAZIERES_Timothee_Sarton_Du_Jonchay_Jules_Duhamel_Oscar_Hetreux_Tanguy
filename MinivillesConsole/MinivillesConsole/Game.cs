using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Text;
using System.Threading;

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
            Console.WriteLine("La partie va commencer !");
            Console.ReadLine();
            Console.Clear();

            bool gagnant = false;
            while (gagnant == false)
            {
                for (int i = 0; i < playerList.Count; i++)//chaque joueurs font leurs tours à la suite en lancant un dé (joueur en question = joueur lancant le dé)
                {
                    if (i == 0)
                    {
                        Console.Clear();
                        DisplayPlayerInfo(playerList[i]);
                        Console.WriteLine("\nVoici votre ville :");
                        foreach (Cards card in playerList[i].pVille)
                        {
                            Console.WriteLine("┌───");
                            Console.WriteLine("│ {0} - activation nbr : {1}", card.CardNameEffect, card.ActivationNbr);
                            Console.WriteLine("└───");
                            Console.WriteLine("");

                        }
                        Thread.Sleep(2000);
                        Console.ReadLine();
                        Console.Clear();


                        playerList[i].de.Lancer();// le joueur en question lance
                        playerList[i].Pioche(piles.cartes.Pop());//le joueur en question pioche une carte

                        DisplayLancerDice(playerList[i]);//afficher le lancement de dé
                        Console.Clear();

                        foreach (Cards card in playerList[i].pVille)
                        {
                            if (card.ActivationNbr == playerList[i].de.face)//si les cartes de la ville du joueur en question correspondent a la face du dé
                            {
                                playerList[i].Effets(card.ActivationNbr);
                            }
                        }
                        
                        foreach (Player Enemie in playerList)
                        {
                            if (Enemie == playerList[i]) { }
                            else//Les joueurs autres que celui en question
                            {
                                
                                foreach (Cards card in Enemie.pVille)
                                {
                                    if (card.ActivationNbr == Enemie.de.face)
                                    {
                                        playerList[i].EffetsRougeBleu(card.ActivationNbr, Enemie);
                                    }
                                }
                                
                            }
                        }

                        

                        int tourAchatCount = 1;
                        while (tourAchatCount == 1)
                        {
                            int count = 0;
                            Console.Clear();
                            DisplayPlayerInfo(playerList[i]);
                            Console.WriteLine("\nVoici votre main :");
                            foreach (Cards card in playerList[i].pMain)
                            {
                                count += 1;
                                Console.WriteLine("  ┌───");
                                Console.WriteLine("{2} │ {0} - {1}$", card.CardNameEffect, card.Cost,count);
                                Console.WriteLine("  └───");
                                Console.WriteLine("");

                            }
                            Console.WriteLine("");
                            int choixJoueur;
                            List<Cards> cartesConstruire = new List<Cards>();
                            while (true)
                            {
                                Console.WriteLine("Qu'elle carte voulez-vous poser ? Vous devrez la payer. (1, 2, 3, ...; -1 pour passer)");
                                choixJoueur = int.Parse(Console.ReadLine());
                                if (choixJoueur > 0 && choixJoueur <= playerList[i].pMain.Count)
                                {
                                    if (playerList[i].pMoney >= playerList[i].pMain[choixJoueur - 1].Cost)
                                        break;
                                }
                                else if (choixJoueur == -1) 
                                    break;
                                Console.WriteLine("Vous devez en choisir une valable.");
                            }
                            if (choixJoueur == -1)
                                break;
                            cartesConstruire.Add(playerList[i].pMain[choixJoueur - 1]);
                            playerList[i].pMain.RemoveAt(choixJoueur - 1);

                            foreach (Cards card in cartesConstruire)
                            {
                                playerList[i].Construire(card);
                            }


                            if (playerList[i].pMain.Count >= 0)
                            {
                                Console.WriteLine("Voulez vous poser une autre cartes ? \n 1 - oui  2 - non");
                                tourAchatCount = int.Parse(Console.ReadLine());
                            }
                            else
                            {
                                tourAchatCount = 2;
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
                            int choixIa=0;
                            List<Cards> cartesConstruire = new List<Cards>();

                            while (true)
                            {
                                if(playerList[i].pMain.Count == 0)
                                    break;
                                choixIa = rdm.Next(1, playerList[i].pMain.Count + 1);
                                if (playerList[i].pMoney >= playerList[i].pMain[choixIa - 1].Cost)
                                {
                                    cartesConstruire.Add(playerList[i].pMain[choixIa - 1]);
                                    playerList[i].pMain.RemoveAt(choixIa - 1);
                                    break;
                                }
                                else
                                    break;
                            }

                            if (playerList[i].pMain.Count == 0)
                                break;
                            
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

        private void DisplayLancerDice(Player player)
        {
            DisplayPlayerInfo(player);
            Console.Write("\nVous lancez votre dé");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.WriteLine(".");
            DisplayDice(player);
        }

        private void DisplayDice(Player player)
        {
            Console.WriteLine("┌───┐");
            Console.WriteLine("│ {0} │", player.de.face);
            Console.WriteLine("└───┘");
            Thread.Sleep(1000);
            Console.ReadLine();
        }

        private void DisplayPlayerInfo(Player player)
        {
            if (player.pMoney >= 10)
            {
                Console.WriteLine("                     │");
                Console.WriteLine("your money :{0}$       │",player.pMoney);
                Console.WriteLine("─────────────────────┘");
            }
            else
            {
                Console.WriteLine("                       │");
                Console.WriteLine("your money :{0}$         │", player.pMoney);
                Console.WriteLine("───────────────────────┘");

            }

        }

    }
}
