using System;
using System.Collections;
using System.Linq;

namespace DeviceDice
{


    //TODO: Define 'business logic' in a separate class file (number of dice to play with, etc).

    //TODO: Version 2: Make it so we can play with multiple dice sides (e.g. diceboard with 3 d2 and 6 d12); we can take away and add dice of a specific type and roll them.

    //TODO: Unit tests: check all validation points for out-of-bounds and type matching input

    //TODO: added exception + exit everywhere when initializing the dice rolls just to experment - should remove them (?) and put while loops in. 
    //Worth keeping for int.parse? - would reject string values and silly -+32bit values

    // change to create a new branch

    class DeviceDice
    {
        static void Main(string[] args)

        {
            // welcome splash screen!

            // instantiate first set of dice

            Console.WriteLine("How many sides do you need on your dice? Acceptable values are 4, 6, 8, 10, 12, or 20.");

            int[] acceptableDiceSideValuesArray = { 4, 6, 8, 10, 12, 20 };

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

            // top-level loop for entire game

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

                int initialDiceValue = 0;

                Dice[] diceBoard = new Dice[numberOfPlayerDice];

                for (int i = 0; i < numberOfPlayerDice; i++)
                {
                    diceBoard[i] = new Dice(diceSides);

                    initialDiceValue += diceBoard[i].value;
                }

                Console.WriteLine("\r\nYou now have " + numberOfPlayerDice + " beautiful dice, all lined up in a row. You stare at them in awe:");

                for (int i = 0; i < diceBoard.Length; i++)
                {
                    Console.WriteLine("\r\nThe " + Ordinal.AddOrdinal((i + 1)) + " die has a value of " + diceBoard[i].value + ".");
                }

                Console.WriteLine("\r\nThe sum of all dice is " + initialDiceValue + ".");

                // while loop for rolling with modified number of dice

                // logic for rolling again

                Boolean playerWantsToPlayWithCurrentDiceNumber = true;
                
                while (playerWantsToPlayWithCurrentDiceNumber) { 

                    // roll loop logic. For simplicity's sake - modifier can be added here and not during the initial roll.

                    Boolean rollAgain = true;

                    while (rollAgain)
                    {
                    
                        Console.WriteLine("\r\nYou roll your beautiful dice.");

                        int allDiceValueForRoll = 0;

                        foreach (Dice specficDice in diceBoard)
                        {
                            specficDice.Roll();

                            allDiceValueForRoll += specficDice.value;
                        }

                        Console.WriteLine("\r\nYou stare at your marvelous dice again. Through the magic of physics, they've changed.");

                        for (int i = 0; i < diceBoard.Length; i++)
                        {
                            Console.WriteLine("\r\nThe " + Ordinal.AddOrdinal((i + 1)) + " die has a value of " + diceBoard[i].value + ".");
                        }

                        //print only if dice number > 1?

                        Console.WriteLine("\r\nThe sum of all dice is now " + allDiceValueForRoll + ".");

                        Console.WriteLine("\r\nRoll again? (y/n)");

                        String rollAgainConfirmation = Console.ReadLine();

                        while(!((rollAgainConfirmation == "y") || (rollAgainConfirmation == "n"))) {

                            Console.WriteLine("Invalid Input, please specify whether or not your want to roll again. (y/n)");

                            rollAgainConfirmation = Console.ReadLine();

                        }

                        if (rollAgainConfirmation == "n") { break; }

                    }

                    // add/subtract input validation

                    Console.WriteLine("\r\nSpecify whether to add or subtract dice, or exit. (add/subtract/exit)");

                    String[] addOrSubtractChoices = { "add", "subtract", "exit" };

                    String addOrSubtractConfirmation = "";

                    addOrSubtractConfirmation = Console.ReadLine();

                    while (!addOrSubtractChoices.Contains(addOrSubtractConfirmation))
                    {

                        Console.WriteLine("Invalid Input. Please specify whether to add or subtract dice, or exit. (add/subtract/exit)");

                        addOrSubtractConfirmation = Console.ReadLine();

                    }

                    int valueToModifyDiceBoard = 0;

                    if (addOrSubtractConfirmation == "add" || addOrSubtractConfirmation == "subtract")
                    {

                        Console.WriteLine("\r\nBy how much? You currently have " + diceBoard.Length + " dice.");

                        try
                        {
                            // need validation for specifying invalid numbers.
                            valueToModifyDiceBoard = int.Parse(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Invalid input." + e);

                            Environment.Exit(1);
                        }

                        // welcome to Off-By-One City: logic for actually modifying the diceBoard array

                        while ((diceBoard.Length + valueToModifyDiceBoard) > 10 || (diceBoard.Length - valueToModifyDiceBoard) < 0)
                        {
                            Console.WriteLine("You can only have between 1 and 10 dice. Please chose a proper value.");

                            try
                            {
                                valueToModifyDiceBoard = int.Parse(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Invalid input." + e);

                                Environment.Exit(1);
                            }
                        }

                        int originalDiceBoardLength = diceBoard.Length;

                        int newDiceValue = 0;

                        if (addOrSubtractConfirmation == "add")
                        {

                            Array.Resize(ref diceBoard, (diceBoard.Length + valueToModifyDiceBoard));

                            for (int i = originalDiceBoardLength; i < diceBoard.Length; i++)
                            {
                                diceBoard[i] = new Dice(diceSides);
                            }

                            foreach (Dice specificDice in diceBoard)
                            {
                                newDiceValue += specificDice.value;
                            }

                            Console.WriteLine("\r\nYou added " + valueToModifyDiceBoard + " and now have " + diceBoard.Length + " dice, with a total sum of " + newDiceValue + ".");

                        }

                        if (addOrSubtractConfirmation == "subtract")
                        {

                            Array.Resize(ref diceBoard, (diceBoard.Length - valueToModifyDiceBoard));

                            foreach (Dice specificDice in diceBoard)
                            {
                                newDiceValue += specificDice.value;
                            }

                            Console.WriteLine("\r\nYou subtracted " + valueToModifyDiceBoard + " and now have " + diceBoard.Length + " dice, with a total sum of " + newDiceValue + ".");

                        }


                    }

                    if (addOrSubtractConfirmation == "exit") { break; }

                }

                // exit loop and close game

                Console.WriteLine("\r\nPlay again soon!");

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
    //implements non-internationalized ordinals for english culture numbers
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