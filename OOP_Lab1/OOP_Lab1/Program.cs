using System;

namespace OOP_Lab1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var artem = new GameAccount("Artem",90);
            var andrey = new GameAccount("Andrey", 90);
            var nikita = new GameAccount("Nikita", 90);
            var game = new Game();
            var rnd = new Random();
            
            for (int i = 0; i < 3; i++)
            {
                int key = rnd.Next(1, 3);
                int rating = rnd.Next(1, 21);
                game.Play(artem, andrey, rating, key);
                game.Play(artem,nikita,rating,key);
                game.Play(andrey, nikita, rating, key);
            }

            artem.OverAllInfo();
            andrey.OverAllInfo();
            nikita.OverAllInfo();
            
            Console.WriteLine();
            Console.Write("Choose id of Artem's game which history you want to watch: ");
            Console.WriteLine(artem.games.GetGame(Convert.ToInt32(Console.ReadLine())));
        }
    }
}