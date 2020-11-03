using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace MiniVilles
{
    class Piles
    {
        public int NbdePossibilitésdansPile = 6;
        public int NbrCartePile;
        Random rnd = new Random();
        public Stack<Cards> cartes = new Stack<Cards>();

        public Piles(int nbrCartePile)
        {
            NbrCartePile = nbrCartePile;

            for (int i = 0; i < NbrCartePile; i++)
            {
                cartes.Push(new Cards(1));
                cartes.Push(new Cards(2));
                cartes.Push(new Cards(3));
                cartes.Push(new Cards(4));
                cartes.Push(new Cards(5));

            }

        }

        public void Shuffle()
        {
            var shuffled = cartes.OrderBy(n => Guid.NewGuid());
        }

        public Cards PrendreCartePioche()
        {
            Cards peeked = cartes.Peek();
            //Console.WriteLine("(Peek)\t\t{0}", peeked);
            return peeked;
        }
    }

}
