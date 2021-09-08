using System;
using System.Collections.Generic;
using System.Text;

namespace FightSimulation
{
    struct Monster
    {
        public string name;
        public float health;
        public float attack;
        public float defense;        
    }
    class Game
    {
        bool gameOver = false;
        Monster currentMonster1;
        Monster currentMonster2;
        int currentMonsterIndex = 0;
        int currentScene = 0;
        //Monsters
        Monster wompus;
        Monster thwompus;
        Monster backupWompus;
        Monster unclePhil;

        public void Run()
        {
            Start();
           
            while (!gameOver)
            {
                Update();
            }
        }

        void Start()
        {
            //Initialize monsters

            wompus.name = "Wompus";
            wompus.attack = 15.0f;
            wompus.defense = 5.0f;
            wompus.health = 20.0f;



            thwompus.name = "Thwompus";
            thwompus.attack = 15.0f;
            thwompus.defense = 10.0f;
            thwompus.health = 15.0f;


            backupWompus.name = "Backup Wompus";
            backupWompus.attack = 25.6f;
            backupWompus.defense = 5.0f;
            backupWompus.health = 3.0f;


            unclePhil.name = "Uncle Phil";
            unclePhil.attack = 100000000000f;
            unclePhil.defense = 0;
            unclePhil.health = 1.0f;

            ResetCurrentMonsters();
          
        }

        void ResetCurrentMonsters()
        {
            currentMonsterIndex = 0;

            //Set starting fighters
            currentMonster1 = GetMonster(currentMonsterIndex);
            currentMonsterIndex++;
            currentMonster2 = GetMonster(currentMonsterIndex);
        }

        void UpdateCurrentScene()
        {
            if (currentScene == 0)
            {
                DisplayStartMenu();
            }
            if (currentScene == 1)
            {
                Battle();
                UpdateCurrentMonsters();
                Console.ReadKey(true);              
            }
            if (currentScene == 2)
            {
                DisplayRestartMenu();
            }
        }

        /// <summary>
        /// Gets an input from the player based on some decision.
        /// </summary>
        /// <param name="description">The context for the decision</param>
        /// <param name="option1">The first choice the player has</param>
        /// <param name="option2">The second choice the player has</param>
        /// <param name="pauseInvalid">If true, the player must press a key to continue after inputting
        /// an incorrect value</param>
        /// <returns>A number representing which of the two options was chosen. Returns 0 if an invalid input.
        /// Return 1 if player picked choice 1. Reurn 2 if player picked choice 2.</returns>
        int GetInput(string description, string option1, string option2, bool pauseInvalid = false)
        {
            Console.WriteLine(description);
            Console.WriteLine("1. " + option1);
            Console.WriteLine("2. " + option2);

            //Get player input
            string input = Console.ReadLine();
            int choice = 0;

            //If the player typed 1 set the return variable to be 1
            if (input == "1")
            {
                choice = 1;
            }
            //If the player typed 2 set the return variable to be 2
            else if (input == "2")
            {
                choice = 2;
            }
            //If the player did not type 1 or 2 let them know input was invalid
            else
            {
                Console.WriteLine("Invalid Input");
                //If we want tp pause when an invalid input is recieved 
                if (pauseInvalid)
                {
                    Console.ReadKey(true);
                }
            }

            return choice;
        }

        /// <summary>
        /// Displays the starting menu. Gives the player the option to start or exit the sumulation.
        /// </summary>
        void DisplayStartMenu()
        {
            //Get player choice
            int choice = GetInput("Welcome to Monster Fight Simulator And Uncle Phil", "Start Simulation", "Quit Application");

            //If they choose to start the simulation start the battle scene.
            if (choice == 1)
            {
                currentScene = 1;
            }
            //Otherwise if they choose exit, end the game.
            else if (choice == 2)
            {
                gameOver = true;
            }
        }

        /// <summary>
        /// Displays the restart menu. Gives the player the option to restart or exit the program
        /// </summary>
        void DisplayRestartMenu()
        {
            //Get the player choice
            int choice = GetInput("Simulation Over. Would you like to play again?", "Yes", "No");

            //If the player chose to restart set the current scene to zero.
            if (choice == 1)
            {
                ResetCurrentMonsters();
                currentScene = 0;
            }
            //Otherwise end the game.
            else if (choice == 2)
            {
                gameOver = true;
            }
        }

        /// <summary>
        /// Called every game loop
        /// </summary>
        void Update()
        {
            UpdateCurrentScene();
            Console.Clear();
        }


        Monster GetMonster(int monsterIndex)
        {
            Monster monster;
            monster.name = "None";
            monster.attack = 1;
            monster.defense = 1;
            monster.health = 1;

            if (monsterIndex == 0)
            {
                monster = unclePhil;
            }
            else if (monsterIndex == 1)
            {
                monster = backupWompus;
            }
            else if (monsterIndex == 2)
            {
                monster = wompus;
            }
            else if (monsterIndex == 3)
            {
                monster = thwompus;
            }

            return monster;
        }

        /// <summary>
        /// Simulates one turn in the current monster fight.
        /// </summary>
        void Battle()
        {
            //Print monster 1 stats
            PrintStats(currentMonster1);
            //Print monster 2 stats
            PrintStats(currentMonster2);
            Console.ReadKey();

            //Monster 1 attacks monster 2
            float damageTaken = Fight(currentMonster1, ref currentMonster2);
            Console.WriteLine(currentMonster2.name + " has taken " + damageTaken);

            //Monster 2 attacks monster 1
            damageTaken = Fight(currentMonster2, ref currentMonster1);
            Console.WriteLine(currentMonster1.name + " has taken " + damageTaken);
        }

        /// <summary>
        /// Changes on of the current fighters to be the next in the list if it died.
        /// Ends the game if all fighters in the list have been used.
        /// </summary>
        void UpdateCurrentMonsters()
        {
            //If monster 1 has died increment the current monster index and swap out the monster
            if (currentMonster1.health <= 0)
            {
                currentMonsterIndex++;
                currentMonster1 = GetMonster(currentMonsterIndex);
            }
            //If monster 2 has died increment the current monster index and swap out the monster
            if (currentMonster2.health <= 0)
            {
                currentMonsterIndex++;
                currentMonster2 = GetMonster(currentMonsterIndex);
            }
            //If either monster is set to "None" and the last monster has been set go to the restart menu
            if (currentMonster2.name == "None" || currentMonster1.name == "None" && currentMonsterIndex >= 4)
            {             
                currentScene = 2;
            }
        }

        void PrintStats(Monster monster)
        {
            Console.WriteLine("Name: " + monster.name);
            Console.WriteLine("Health: " + monster.health);
            Console.WriteLine("Attack: " + monster.attack);
            Console.WriteLine("Defense: " + monster.defense);
        }


        string StartBattle(ref Monster monster1, ref Monster monster2)
        {
            string matchResult = "No Contest";

            while (monster1.health > 0 && monster2.health > 0)
            {
                //Print monster 1 stats
                PrintStats(monster1);
                //Print monster 2 stats
                PrintStats(monster2);

                //Monster 1 attacks monster 2
                float damageTaken = Fight(monster1, ref monster2);
                Console.WriteLine(monster2.name + " has taken " + damageTaken);

                //Monster 2 attacks monster 1
                damageTaken = Fight(monster2, ref monster1);
                Console.WriteLine(monster1.name + " has taken " + damageTaken);

                Console.ReadKey(true);
                Console.Clear();

            }
            if (monster1.health <= 0 && monster2.health <= 0)
            {
                matchResult = "Draw";
            }
            else if (monster1.health > 0)
            {
                matchResult = monster1.name;
            }
            else if (monster2.health > 0)
            {
                matchResult = monster2.name;
            }
         

            return matchResult;
        }

        float Fight(Monster attacker, ref Monster defender)
        {
            float damageTaken = CalculateDamage(attacker, defender);
            defender.health -= damageTaken;
            return damageTaken;
        }
        float CalculateDamage(float attack, float defense)
        {
            float damage = attack - defense;
            if (damage <= 0)
            {
                damage = 0;
            }

            return damage;
        }

        float CalculateDamage(Monster attacker, Monster defender)
        {
            return attacker.attack - defender.defense;
        }
    }
}
