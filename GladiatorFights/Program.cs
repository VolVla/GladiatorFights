using System;
using System.Collections.Generic;

namespace GladiatorFights
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fighter fighter = new Fighter();
            List<Fighter> list = new List<Fighter>();
            list.Add(new Warrior("Jon", 200, 20, 40));
            list.Add(new Archer("Lyk", 160, 15, 30, 80));
            list.Add(new Paladin("Boris", 300, 50, 25));
            list.Add(new PlagueDoctor("Sanek",250,40,30));
            list.Add(new Barbarian("Dem",140,10,120));
            fighter.Battle(list);
        }
    }

    class Fighter : ICloneable
    {
        public int Health 
        { 
            get 
            { 
                return _health; 
            } 
        }
        public int Damage 
        { 
            get 
            { 
                return _damage; 
            } 
        }
        public string Name 
        { 
            get 
            { 
                return _name; 
            } 
        }
        
        private const float _critChange = 1.5f;
        private protected int responseChange = 5;
        private protected int _health;
        private protected int _armor;
        private protected string _name;
        private protected int _damage;
        private int _criticalChange = 6;
        public bool IsTrueCriticalChange = false;
 
        public Fighter(string name,int health, int armor, int damage)
        {
            _name = name;
            _health = health;
            _armor = armor;
            _damage = damage;
            
        }
        public Fighter()
        {

        }

        public void Battle(List<Fighter> list)
        {
            bool battel = true;

            ShowList(list);
            Console.WriteLine("Выберите первого бойца:");
            bool firth = int.TryParse(Console.ReadLine(), out int indexSecondFighter);
            Fighter secondFighter = list[indexSecondFighter];
            Console.WriteLine("Выберите второго бойца:");
            bool firths = int.TryParse(Console.ReadLine(), out int indexFirstFighter);
            Fighter firstFighter = list[indexFirstFighter];

            if (secondFighter == firstFighter)
            {
                Fighter fighter2 = new Fighter();

                fighter2 = (Fighter)secondFighter.Clone();
                fighter2._name = "Shadow " + secondFighter.Name;
                firstFighter = fighter2;
            }
            while (battel == true)
            {
                secondFighter.TakeDamage(firstFighter.Damage);
                firstFighter.TakeDamage(secondFighter.Damage);
                firstFighter.ShowStats();
                secondFighter.ShowStats();
                
                if (secondFighter.Health <= 0 && firstFighter.Health <= 0)
                {
                    battel = false;
                    Console.WriteLine($"Поздравляю бойцы убили друг друга, никто не победил и все проиграли ");
                }
                else if (secondFighter.Health <= 0)
                {
                    battel = false;
                    Console.WriteLine($"Победил {firstFighter.Name} ");
                }
                else if (firstFighter.Health <= 0)
                {
                    battel = false;
                    Console.WriteLine($"Победил {secondFighter.Name} ");
                }
                
            }
        }

        public void PickSoldier()
        {

        }

        public void ShowList(List<Fighter> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(i + " ");
                list[i].ShowStats();
            }
        }

        public object Clone()
        {
            return new Fighter(_name, _health, _armor, _damage);
        }

        public void ResponseCriticalChange()
        {
            Random random = new Random();
            int criticalChange = random.Next(0, responseChange);

            if (criticalChange == responseChange)
            {
               IsTrueCriticalChange = true;
            }
        } 
        public void TakeDamage(int damage)
        {
           ResponseCriticalChange();

            if (IsTrueCriticalChange == true)
            {
                _health -= Convert.ToInt32(_damage * _critChange) - _armor;
            }
            else if (IsTrueCriticalChange == false)
            {
                _health -= _damage - _armor;
            }
        }

 
        public void ShowStats()
        {
           Console.WriteLine($"Имя {_name} HP - {_health}  Damage - {_damage} Armor {_armor} CriticalChange {_critChange}");
        }
    
        

        public void ShowInfo()
        {
            Console.WriteLine(Health);
        }
    }

    class Warrior : Fighter
    {
        public Warrior(string name,int health, int armor, int damage) : base(name,health, armor, damage) { }

        public void BuffAttack()
        {
            _damage += 10;
            _armor -= 5;
        }
    }

    class Archer : Fighter
    {
        public Archer(string name,int health, int armor, int damage, int attackSpeed) :base(name,health, armor, (damage + attackSpeed) ){ }

        private int _damageShoot = 20;
        
        public void Shoot()
        {
            Console.WriteLine("");
            TakeDamage(_damageShoot);
        }
    }

    class Paladin : Fighter
    {
        public Paladin(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }
    }

    class PlagueDoctor : Fighter
    {
        public PlagueDoctor(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }
    }

    class Barbarian : Fighter
    {
        public Barbarian(string name, int health, int armor, int damage) : base(name, health, armor, damage) { }
    }
}
