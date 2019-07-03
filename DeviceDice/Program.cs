using System;
using System.Collections;
using System.Linq;

namespace DeviceDice
{


    //TODO: Define 'business logic' in a separate class (number of dice to play with, etc).

    //TODO: Version 2: Make it so we can play with differing numbers of dice (e.g. diceboard with 3 d2 and 6 d12); we can take away and add dice of a specific type. Probably necessitates complete redesign.

    //TODO: Unit tests.

    class DeviceDice
    {
        static void Main(string[] args)
        {

            Console.WriteLine("How many sides do you need on your dice? Acceptable values are 2, 4, 6, 8, 10, 12, or 20.");

            int[] acceptableDiceSideValuesArray = { 2, 4, 6, 8, 10, 12, 20 };

            int diceSides = 0;

            try
            {
                diceSides = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input." + e);

                Environment.Exit(1);
            }

            while (!acceptableDiceSideValuesArray.Contains(diceSides))
            {

                Console.WriteLine("Must chose between dice with 4, 6, 8, 10, 12, or 20 sides.");

                try
                {
                    diceSides = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input." + e);

                    Environment.Exit(1);
                }

            }

            Boolean playerWantsToPlay = true;

            while (playerWantsToPlay == true)
            {

                Console.WriteLine("How many dice do you want to roll? Acceptable values are 1-10.");

                int numberOfPlayerDice = 0;

                try
                {
                    numberOfPlayerDice = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input." + e);

                    Environment.Exit(1);
                }

                while (!(numberOfPlayerDice < 11 || numberOfPlayerDice <= 0))
                {
                    Console.WriteLine("Must chose between one and ten dice.");

                    try
                    {
                        numberOfPlayerDice = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid input." + e);

                        Environment.Exit(1);
                    }
                }

                Dice[] diceBoard = new Dice[numberOfPlayerDice];

                for (int i = 0; i < numberOfPlayerDice; i++)
                {
                    diceBoard[i] = new Dice(diceSides);
                }

                Console.WriteLine("\r\nYou now have " + numberOfPlayerDice + " beautiful dice, all lined up in a row. You stare at them in awe:");

                for (int i = 0; i < diceBoard.Length; i++)
                {
                    Console.WriteLine("\r\nThe " + Ordinal.AddOrdinal((i + 1)) + " die has a value of " + diceBoard[i].value + ".");
                }

                Boolean RollAgain = true;

                while (RollAgain)
                {

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

                    Console.WriteLine("Roll again? (y/n)");

                    String rollAgainConfirmation = Console.ReadLine();

                    while(!((rollAgainConfirmation == "y") || (rollAgainConfirmation == "n"))) {

                        Console.WriteLine("Invalid Input, please specify whether or not your want to roll again (y/n)");

                        rollAgainConfirmation = Console.ReadLine();

                    }

                    if (rollAgainConfirmation == "n") { break; }

                }

                //Use Array.Resize ability to add & subtract number of dice in array.

                // exit loop and close game

                Console.WriteLine("Play again soon!");

                playerWantsToPlay = false;

            }

        }

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