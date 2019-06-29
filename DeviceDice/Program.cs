using System;
using System.Collections;
using System.Linq;

namespace DeviceDice
{
    class DeviceDice
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many sides do you need on your dice?");

            //TODO: input validation for: 4, 6, 8, 10, 12, 20

            int diceSides = int.Parse(Console.ReadLine());

            Console.WriteLine("How many dice do you want to roll?");

            //TODO: input validation on upper number of dice, possibly 6 (and at least one);       

            int numberOfPlayerDice = int.Parse(Console.ReadLine());

            //TODO: store it in a hash table!

            Dice[] diceBoard = new Dice[numberOfPlayerDice];

            for (int i = 0; i < numberOfPlayerDice; i++)
            {
                diceBoard[i] = new Dice(diceSides);
            }

            Console.WriteLine("\r\nYou create " + numberOfPlayerDice + " beautiful dice, all lined up in a row. You stare at them in awe:");

            for (int i = 0; i < diceBoard.Length; i++)
            {
                Console.WriteLine("\r\nThe " + Ordinal.AddOrdinal((i + 1)) + " die has a value of " + diceBoard[i].value + ".");
            }

            // TODO: add loop to reroll as needed.

            Console.WriteLine("\r\nYou roll your beautiful dice.");

            foreach (Dice specficDice in diceBoard)
            {
                specficDice.Roll();
            }

            Console.WriteLine("\r\nYou stare at your marvelous dice again. Through the magic of physics, they've changed.");

            for (int i = 0; i < diceBoard.Length; i++)
            {
                Console.WriteLine("\r\nThe " + Ordinal.AddOrdinal((i + 1)) + " die has a value of " + diceBoard[i].value + ".");
            }

            // TODO: add or subtract dice and reroll.

            // TODO: create a UI for it in Xamarin

        }
    }
    public class Dice
    {
        public int value;
        public int sides;
        public Guid id;

        public Dice(int _sides)
        {
            sides = Math.Abs(_sides);
            id = Guid.NewGuid();

            Random rnd = new Random();

            value = rnd.Next(1, _sides);
        }

        public void Roll()
        {
            Random rnd = new Random();

            value = rnd.Next(1, sides);

        }

    }

    //shamelessly stolen from StackOverflow 🙏 
    //implements non-internationalized ordinals for english culture numbers (TODO: Add .net libraries for culture-based ordinals)
    //source: user samjudson on Aug 21 '08 -- https://stackoverflow.com/questions/20156/is-there-an-easy-way-to-create-ordinals-in-c
    class Ordinal
    {
        public static string AddOrdinal(int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }

        }

    }

}
