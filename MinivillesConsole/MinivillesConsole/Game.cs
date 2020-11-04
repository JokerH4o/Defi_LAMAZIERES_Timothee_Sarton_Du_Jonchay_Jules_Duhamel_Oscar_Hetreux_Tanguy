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
        public string pName = "";
        public Piles piles;
        public List<Player> playerList = new List<Player>();

        Random rdm = new Random();


        public Game(int nbrJoueurs, int n)
        {
            Console.Write("Votre nom ? : ");
            string playerName = Console.ReadLine();
            this.nbCartes = n;
            for (int i = 1; i < nbrJoueurs + 1; i++)
            {
                if(i==1)
                    pName = playerName;
                else
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
                        DisplayPlayerInfo();
                        Console.WriteLine("\nVoici votre ville :");
                        foreach (Cards card in playerList[i].pVille)
                        {
                            Console.WriteLine("┌───");
                            Console.Write("│ ");
                            Console.ForegroundColor = card.Color;
                            Console.WriteLine("{0} - activation nbr : {1}", card.CardNameEffect, card.ActivationNbr);
                            Console.ResetColor();
                            Console.WriteLine("└───");
                            Console.WriteLine("");

                        }
                        Thread.Sleep(2000);
                        Console.ReadLine();
                        Console.Clear();


                        playerList[i].de.Lancer();// le joueur en question lance
                        playerList[i].Pioche(piles.cartes.Pop());//le joueur en question pioche une carte

                        DisplayLancerDice();//afficher le lancement de dé
                        Console.Clear();

                        foreach (Cards card in playerList[i].pVille)
                        {
                            if (card.ActivationNbr == playerList[i].de.face)//si les cartes de la ville du joueur en question correspondent a la face du dé
                            {
                                playerList[i].Effets(card.RefCards);
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
                                        playerList[i].EffetsRougeBleu(card.RefCards, Enemie);
                                    }
                                }
                                
                            }
                        }

                        

                        int tourAchatCount = 1;
                        while (tourAchatCount == 1)
                        {
                            int count = 0;
                            Console.Clear();
                            DisplayPlayerInfo();
                            Console.WriteLine("\nVoici votre main :");
                            foreach (Cards card in playerList[i].pMain)
                            {
                                count += 1;
                                Console.WriteLine("  ┌───");
                                Console.Write("{0} │ ", count);
                                Console.ForegroundColor = card.Color;
                                Console.WriteLine(" {0} - activation nbr :{2} - {1}$", card.CardNameEffect, card.Cost, card.ActivationNbr);
                                Console.ResetColor();
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

                        
                        foreach (Cards card in playerList[i].pVille)
                        {
                            if (card.ActivationNbr == playerList[i].de.face)//si les cartes de la ville du joueur en question correspondent a la face du dé
                            {
                                playerList[i].Effets(card.RefCards);
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
                                        playerList[i].EffetsRougeBleu(card.RefCards, Enemie);
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

        private void DisplayLancerDice()
        {
            DisplayPlayerInfo();
            Console.Write("\nVous lancez votre dé");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.WriteLine(".");
            DisplayDice();
        }

        private void DisplayDice()
        {
            Console.WriteLine("┌───┐");
            Console.WriteLine("│ {0} │", playerList[0].de.face);
            Console.WriteLine("└───┘");
            Thread.Sleep(1000);
            Console.ReadLine();
        }

        private void DisplayPlayerInfo()
        {
            if (playerList[0].pMoney >= 10)
            {
                Console.Write("                     │          │   ");
                foreach(Player player in playerList)
                {
                    if(player == playerList[0]) { }
                    else
                    {
                        Console.Write("j{0}:   ", player.pName);
                    }
                }
                Console.Write("\nyour money :{0}$      │          │    ", playerList[0].pMoney);
                foreach (Player player in playerList)
                {
                    if (player == playerList[0]) { }
                    else
                    {
                        Console.Write("{0}    ", player.pMoney);
                    }
                }
                Console.WriteLine("\n─────────────────────┘          └─────────────────────────────────────────────────────────────────────────────────────");
            }
            else
            {
                Console.Write("                       │          │   ");
                foreach (Player player in playerList)
                {
                    if (player == playerList[0]) { }
                    else
                    {
                        Console.Write("j{0}:   ", player.pName);
                    }
                }
                Console.Write("\nyour money :{0}$         │          │     ", playerList[0].pMoney);
                foreach (Player player in playerList)
                {
                    if (player == playerList[0]) { }
                    else
                    {
                        Console.Write("{0}    ", player.pMoney);
                    }
                }
                Console.WriteLine("\n───────────────────────┘          └─────────────────────────────────────────────────────────────────────────────────────");

            }


        }

    }
}
