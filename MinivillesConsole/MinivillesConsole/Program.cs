using System;
using System.Runtime.Serialization;

namespace MiniVilles
{

    class Program
    {

        static void Main(string[] args)
        {
            int nbPlayer;
            bool nextGame = true;

            do
            {
                Console.WriteLine("Bienvenue dans le jeu minivilles");
                Console.WriteLine("Contre combien de bots voulez-vous jouer ?");
                nbPlayer = int.Parse(Console.ReadLine()) + 1;
                Game g = new Game(nbPlayer, 6);
                Console.WriteLine("Vous êtes le joueur : {0}", g.playerList[0].pName);
                g.Tour();
                Console.Clear();
                Console.Write("Voici les vainqueurs : ");
                foreach (Player player in g.playerList)
                {
                    if (player.pMoney >= 20)
                    {
                        Console.Write(player.pName +", avec "+ player.pMoney+"$; ");
                    }
                }

                Console.WriteLine("\nVoulez-vous faire une autre partie ? 1 - Oui    2 - Non");
                if (Console.ReadLine() == ("2"))
                {
                    nextGame = false;
                }
            } while (nextGame);        
        }
    }
}