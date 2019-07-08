using System;
using System.Collections;
using System.Linq;
using System.Drawing;
using Console = Colorful.Console;

namespace DeviceDice
{


    //TODO: Define 'business logic' in a separate classs file (number of dice to play with, etc).

    //TODO: Version 2: Make it so we can play with multiple dice sides (e.g. diceboard with 3 d2 and 6 d12); we can take away and add dice of a specific type and roll them.
    // can extend to multiple dice types by converting from array to dictionary

    //TODO: Unit tests: check all validation points for out-of-bounds and type matching input
    // create a few classes to do this.

    //TODO: added exception + exit everywhere when initializing the dice rolls just to experment - should remove them (?) and put while loops in. 
    //Worth keeping for int.parse? - would reject string values and silly -+32bit values

    // change to create a new branch

    class DeviceDice
    {
        static void Main(string[] args)

        {

            Boolean playerWantsToPlay = true;

            while (playerWantsToPlay == true)
            {

                //
                //
                //
                // instantiate first set of dice

            Console.WriteAscii("DeviceDice", Color.Red);

            Console.WriteLine("\r\nHow many sides do you need on your dice? Acceptable values are 4, 6, 8, 10, 12, or 20.");

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

                Console.WriteLine("Set a roll modifier? (y/n)");

                String useRollModifier = Console.ReadLine();

                while (!(useRollModifier == "y" || useRollModifier == "n"))
                {
                    Console.WriteLine("Please enter a proper value.");

                    useRollModifier = Console.ReadLine();
                }

                int rollModifier = 0;

                if (useRollModifier == "y")
                {
                    Console.WriteLine("Enter a modifier value greater than 0.");

                    try
                    {
                        rollModifier = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid input." + e);

                        Environment.Exit(1);
                    }

                    while (rollModifier <= 0)
                    {
                        Console.Write("Please enter a value greater than 0");

                        try
                        {
                            rollModifier = int.Parse(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Invalid input." + e);

                            Environment.Exit(1);
                        }

                    }

                }

                if (useRollModifier == "n") { Console.WriteLine("No modifier used."); }

                // Print initial dice values

                Console.WriteLine("\r\nYou now have " + numberOfPlayerDice + " beautiful dice, all lined up in a row. You stare at them in awe:");

                for (int i = 0; i < diceBoard.Length; i++)
                {
                    Console.WriteLine("\r\nThe " + Ordinal.AddOrdinal((i + 1)) + " die has a value of " + diceBoard[i].value + ".");
                }

                Console.WriteLine("\r\nThe sum of all dice values are " + initialDiceValue + ".");

                if (useRollModifier == "y")
                {
                    Console.WriteLine("\r\nWith a roll modfier of " + rollModifier + ", your dice roll has a value of " + (initialDiceValue + rollModifier) + ".");
                }

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

                        if (useRollModifier == "y")
                        {
                            Console.WriteLine("\r\nWith a roll modfier of " + rollModifier  + ", your dice roll has a value of " + (allDiceValueForRoll + rollModifier) + ".");
                        }

                        Console.WriteLine("\r\nRoll again? (y/n)");

                        String rollAgainConfirmation = Console.ReadLine();

                        while(!((rollAgainConfirmation == "y") || (rollAgainConfirmation == "n"))) {

                            Console.WriteLine("Invalid Input, please specify whether or not your want to roll again. (y/n)");

                            rollAgainConfirmation = Console.ReadLine();

                        }

                        if (rollAgainConfirmation == "n") { break; }

                    }

                    // add/subtract input validation

                    Console.WriteLine("\r\nSpecify whether to add or subtract dice, change the roll modifier, or exit. (add/subtract/modifier/exit)");

                    String[] addOrSubtractChoices = { "add", "subtract", "modifier", "exit" };

                    String addOrSubtractConfirmation = "";

                    addOrSubtractConfirmation = Console.ReadLine();

                    while (!addOrSubtractChoices.Contains(addOrSubtractConfirmation))
                    {

                        Console.WriteLine("Invalid Input. Please specify whether to add or subtract dice, or exit. (add/subtract/modifier/exit)");

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

                        if (valueToModifyDiceBoard < 0)
                        {

                            Console.WriteLine("A negative number, eh? Don't mind if I convert your input to an absolute number.");

                            valueToModifyDiceBoard = Math.Abs(valueToModifyDiceBoard);

                        }

                        // welcome to Off-By-One City: logic for actually modifying the diceBoard array

                        int originalDiceBoardLength = diceBoard.Length;

                        int newDiceValue = 0;

                        if (addOrSubtractConfirmation == "add")

                        {
                            while ((diceBoard.Length + valueToModifyDiceBoard) > 10)
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
                            while ((diceBoard.Length - valueToModifyDiceBoard) < 1)
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

                            Array.Resize(ref diceBoard, (diceBoard.Length - valueToModifyDiceBoard));

                            foreach (Dice specificDice in diceBoard)
                            {
                                newDiceValue += specificDice.value;
                            }

                            Console.WriteLine("\r\nYou subtracted " + valueToModifyDiceBoard + " and now have " + diceBoard.Length + " dice, with a total sum of " + newDiceValue + ".");

                        }

                    }

                    if (addOrSubtractConfirmation == "modifier")
                    {
                        Console.WriteLine("\r\nAdd a modifier, change your modifier, stop using a roll modifier, or exit (add/change/stop/exit)");

                        String modifierChoice = Console.ReadLine();

                        String[] modifyRollModifier = { "add", "change", "stop", "exit" };

                        while (!modifyRollModifier.Contains(modifierChoice))
                        {
                            Console.WriteLine("Please chose a valid input (change/stopmodifier/exit)");

                            modifierChoice = Console.ReadLine();
                        }

                        if (modifierChoice == "add")
                        {
                            Console.WriteLine("\r\nYou're now playing with a roll modifier.");

                            useRollModifier = "y";

                            modifierChoice = "change";
                        }

                        if (modifierChoice == "change")
                        {

                            Console.WriteLine("\r\nInput a value for the dice modifier greater than 0.");

                            try
                            {
                                rollModifier = int.Parse(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Invalid input." + e);

                                Environment.Exit(1);
                            }

                            while (rollModifier <= 0)
                            {
                                Console.Write("Please enter a value greater than 0.");

                                try
                                {
                                    rollModifier = int.Parse(Console.ReadLine());
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Invalid input." + e);

                                    Environment.Exit(1);
                                }

                            }

                        }

                        if (modifierChoice == "stop") { Console.WriteLine("\r\nYou stop using a dice modifier."); useRollModifier = "n"; rollModifier = 0; }

                    }

                    if (addOrSubtractConfirmation == "exit") { break; }

                }

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


//class DiceGame
//{
//    public Guid SessionId {get;}
//    public String[] CurrentDiceBoard;

//}