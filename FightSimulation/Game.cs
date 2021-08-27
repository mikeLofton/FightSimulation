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
        public void Run()
        {
            //Monster 1
            Monster monster1;
            monster1.name = "Wompus";
            monster1.attack = 10.0f;
            monster1.defense = 5.0f;
            monster1.health = 20.0f;

            //Monster 2
            Monster monster2;
            monster2.name = "Thwompus";
            monster2.attack = 15.0f;
            monster2.defense = 10.0f;
            monster2.health = 15.0f;

            //Print monster 1 stats
            PrintStats(monster1);
            //Print monster 2 stats
            PrintStats(monster2);
            Console.ReadKey();

            //Monster 1 attacks monster 2
            float damageTaken = Fight(monster1, monster2);
            Console.WriteLine(monster2.name + " has taken " + damageTaken);

            //Monster 2 attacks monster 1
            damageTaken = Fight(monster2, monster1);            
            Console.WriteLine(monster1.name + " has taken " + damageTaken);

            Console.ReadKey();
            Console.Clear();

            //Print monster 1 stats
            PrintStats(monster1);
            //Print monster 2 stats
            PrintStats(monster2);
            Console.ReadKey();
        }

        void PrintStats(Monster monster)
        {
            Console.WriteLine("Name: " + monster.name);
            Console.WriteLine("Health: " + monster.health);
            Console.WriteLine("Attack: " + monster.attack);
            Console.WriteLine("Defense: " + monster.defense);
        }

        float Fight(Monster attacker, Monster defender)
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
