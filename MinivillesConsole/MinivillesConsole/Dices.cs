using System;
using System.Collections.Generic;
using System.Text;

namespace MiniVilles
{
    class Dices
    {
        public int NbFaces = 6;
        public List<int> faces = new List<int>() { };
        public int face;

        public Dices()
        {
            for (int i = 0; i < NbFaces; i++)
            {
                faces.Add(1 + i);
            }
            Lancer();

            //foreach (int i in faces) //afficher chaque faces du dé en ligne
            //    Console.WriteLine(i);
            //Console.ReadLine();
        }

        public Dices(int nbFaces)
        {
            NbFaces = nbFaces;

            for (int i = 0; i < NbFaces; i++)
            {
                faces.Add(1 + i);
            }

            //foreach (int i in faces)
            //    Console.WriteLine(i);
            //Console.ReadLine();
        }

        public void Lancer()
        {
            Random ran = new Random();
            face = ran.Next(1, NbFaces + 1);
        }

        public void afficherFace()
        {
            Console.WriteLine("La face actuelle : {0}", face);
        }
    }
}
