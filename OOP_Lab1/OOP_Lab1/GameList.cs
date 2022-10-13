using System;

namespace OOP_Lab1
{
    public class GameList<TValue> 
    {
        private Node First;
        private Node Last;
        public int Count { get; set; } = 0;
        
        private class Node
        {
            public int Key;
            public TValue Val;
            public Node Next;
        }

        public Boolean IsEmpty()
        {
            return First == null;
        }

        public void AddGame(int key, TValue item)
        {
            if (First!=null)
            {

                Node oldlast=Last;
                Last = new Node();
                Last.Val=item;
                Last.Key = key;
                oldlast.Next=Last;
            }

            else
            {
                Last = new Node();
                Last.Val=item;
                Last.Key = key;
                Last.Next=null;
                First=Last;
            }
            Count++;
        }

        public TValue GetGame(int key)
        {
            TValue item = default;
            Node x = First;
            while( x != null)
            {
                if (key == x.Key)  
                {
                    item = x.Val;
                    break;
                }
                x = First.Next;
            }

            if (x == null) throw new NoSuchKeyException("There is no such id of game");
            return item;
            
        }

        public void AllStatistic()
        {
            for (Node x = First; x != null; x = x.Next)
            {
                Console.WriteLine(x.Val);
            }
        }

        public void Clear()
        {
            First = null;
        }
        
    }
}
