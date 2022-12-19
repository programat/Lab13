using System;
using System.Collections;

namespace Lab13
{
    public abstract class Animal
    {
        public int number;
        public int age;
        public string name;

        public Animal() {}

        public abstract void Change_number(int number);
        public abstract void Change_age(int age);
        public abstract void Change_name(string name);
        public abstract void Change_param(int param);
        public abstract void Change_param2(int param);
        public abstract void Print_info();
    }

    public class Mammals : Animal, IComparable<Mammals>, IComparer<Mammals>, ICloneable
    {
        public bool horns;
        public bool claws; //когти

        public int CompareTo(Mammals? p)
        {
            if (p is null)
                throw new ArgumentException("Incorrect parameter value");
            return name.CompareTo(p.name);
        }

        
        public int Compare(Mammals? p1, Mammals? p2)
        {
            if (p1 is null || p2 is null)
                throw new ArgumentException("Incorrect parameter value");
            return p1.name.CompareTo(p2.name);
        }

        public object Clone() => MemberwiseClone();

        public object DeepClone()
        {
            Mammals other = new Mammals(number, age, name);
            if (horns) other.Change_param(1);
            else other.Change_param(-1);
            if (claws) other.Change_param2(1);
            else other.Change_param2(-1);
            return other;
        }

        public Mammals()
        {
            number = -1;
            age = -1;
            name = "null";
            horns = false;
            claws = false;
        }

        public Mammals(int number, int age, string name)
        {
            try {
                this.number = number;
                this.age = age;
                this.name = name;
                this.horns = false;
                this.claws = false;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public Mammals(int number, int age, string name, int horns, int claws)
        {
            try {
                this.number = number;
                this.age = age;
                this.name = name;
                if (horns == 0)
                    this.horns = false;
                else
                    this.horns = true;
                if (claws == 0)
                    this.claws = false;
                else
                    this.claws = true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        
        public override void Change_number(int number)
        {
            this.number = number;
        }

        public override void Change_age(int age)
        {
            this.age = age;
        }

        public override void Change_name(string name)
        {
            this.name = name;
        }

        public override void Change_param(int flag)
        {
            if (flag <= 0)
                this.horns = false;
            else
                this.horns = true;
        }

        public override void Change_param2(int flag)
        {
            if (flag <= 0)
                this.claws = false;
            else
                this.claws = true;
        }

        public override void Print_info()
        {
            Console.WriteLine($"N: {number}.  age: {age}, name: {name}, horns is {horns}, claws is {claws};");
        }
    }

    public class Zoo
    {
        private List<Animal> animals;

        public List<Animal> GetAnimals
        {
            get {
                return animals;
            }
        }

        public Zoo()
        {
            animals = new List<Animal>();
        }

        public void Add_elem(Animal m)
        {
            animals.Add(m);
        }

        public void Edit_elem(int i, int value)
        {
            try {
                if (value == 1) {
                    Console.Write("Enter the number: ");
                    animals[i].Change_number(Convert.ToInt32(Console.ReadLine()));
                }

                if (value == 2) {
                    Console.Write("Enter the age: ");
                    animals[i].Change_age(Convert.ToInt32(Console.ReadLine()));
                }

                if (value == 3) {
                    Console.Write("Enter the name: ");
                    animals[i].Change_name(Console.ReadLine());
                }

                if (value == 4) {
                    Console.Write("Enter int value >0 if animal has horns: ");
                    if (Convert.ToInt32(Console.ReadLine()) == 0)
                        animals[i].Change_param(0);
                    else
                        animals[i].Change_param(1);
                }

                if (value == 5) {
                    Console.Write("Enter int value >0 if animal has claws: ");
                    if (Convert.ToInt32(Console.ReadLine()) == 0)
                        animals[i].Change_param2(0);
                    else
                        animals[i].Change_param2(1);
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void Sort(int h)
        {
            try {
                if (h == 1) animals.Sort(delegate(Animal e1, Animal e2) { return e1.number.CompareTo(e2.number);});
                if (h == 2) animals.Sort(delegate(Animal e1, Animal e2) { return e1.age.CompareTo(e2.age);});
                if (h == 3) animals.Sort(delegate(Animal e1, Animal e2) { return e1.name.CompareTo(e2.name);});
            }
            catch (Exception ex) {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void Find(int h)
        {
            if (h < 1 || h > 3) throw new ArgumentException();
            try {
                if (h == 1) {
                    Console.WriteLine("Enter number to find: ");
                    int temp = Convert.ToInt32(Console.ReadLine());
                    int counter = 0;
                    Console.WriteLine($"Found records with the number - {temp}:");
                    foreach (Animal person in animals.FindAll(e => (e.number == temp)).ToList())  
                    {  
                        counter++;
                        person.Print_info();
                    }
                    Console.WriteLine($"found {counter} records");
                }
                if (h == 2) {
                    Console.WriteLine("Enter age to find: ");
                    int temp = Convert.ToInt32(Console.ReadLine());
                    int counter = 0;
                    Console.WriteLine($"Found records with the age - {temp}:");
                    foreach (Animal person in animals.FindAll(e => (e.age == temp)).ToList())  
                    {  
                        counter++;
                        person.Print_info();
                    }
                    Console.WriteLine($"found {counter} records");
                }

                if (h == 3) {
                    Console.WriteLine("Enter name to find: ");
                    string temp = Console.ReadLine();
                    int counter = 0;
                    Console.WriteLine($"Found records with the name - {temp}:");
                    foreach (Animal person in animals.FindAll(e => (e.name == temp)).ToList()) {
                        counter++;
                        person.Print_info();
                    }
                    Console.WriteLine($"found {counter} records");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.ReadKey();
            }
        }

        public void Del_elem(int i)
        {
            animals.RemoveAt(i);
        }

        public void Print()
        {
            foreach (var i in animals)
                i.Print_info();
        }

        public int Last_elem()
        {
            return animals.Count;
        }
    }

    class Program
    {
        static void Main()
        {
            const string PressAnyKey = "\nPress any key...";
            Zoo NY_Zoo = new Zoo();
            int item;
            do {
                int k0;
                do {
                    Console.Clear();
                    Console.WriteLine("\nChoose one of the following:");
                    Console.WriteLine(" 1. Add object to the collection");
                    Console.WriteLine(" 2. Edit collection element's field...");
                    Console.WriteLine(" 3. Delete element");
                    Console.WriteLine(" 4. Print collection");
                    Console.WriteLine(" 5. Sort collection by...");
                    Console.WriteLine(" 6. Find elements");
                    Console.WriteLine(" 7. Exit");
                    k0 = 8;

                    Console.Write("\nEnter the number of choosing action: ");
                    item = 0;
                    try {
                        item = Convert.ToInt16(Console.ReadLine());
                    }
                    catch (FormatException ex) {
                        Console.WriteLine("\nYou must to enter integers from 1 to " + Convert.ToString(k0) +
                                          PressAnyKey);
                        Console.ReadKey();
                    }
                } while ((item < 1) || (item > k0));

                switch (item) {
                    case 1: {
                        try {
                            Animal m = new Mammals();

                            Console.Write("Enter the number: ");
                            m.Change_number(Convert.ToInt32(Console.ReadLine()));

                            Console.Write("Enter the age: ");
                            m.Change_age(Convert.ToInt32(Console.ReadLine()));

                            Console.Write("Enter the name: ");
                            m.Change_name(Console.ReadLine());


                            Console.Write("Enter int value >0 if animal has horns: ");
                            if (Convert.ToInt32(Console.ReadLine()) == 0)
                                m.Change_param(0);
                            else
                                m.Change_param(1);


                            Console.Write("Enter int value >0 if animal has claws: ");
                            if (Convert.ToInt32(Console.ReadLine()) == 0)
                                m.Change_param2(0);
                            else
                                m.Change_param2(1);


                            NY_Zoo.Add_elem(m);
                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                            Console.ReadKey();
                        }
                        
                        break;
                    }

                    case 2: {
                        try {
                            Console.Write("\nIndex of the element to edit: ");
                            int i = Convert.ToInt32(Console.ReadLine());
                            Console.Write(
                                "\nWhat do u wanna change?\n1-number\n2-age\n3-name\n4-horns\n5-claws\n0-exit\nEnter: ");
                            int value = Convert.ToInt32(Console.ReadLine());
                            NY_Zoo.Edit_elem(i, value);
                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                            Console.ReadKey();
                        }

                        break;
                    }
                    case 3: {
                        try {
                            Console.Write("Index of the element to delete: ");
                            int i = Convert.ToInt32(Console.ReadLine());
                            NY_Zoo.Del_elem(i);
                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                            Console.ReadKey();
                        }

                        break;
                    }

                    case 4: {
                        Console.WriteLine();
                        NY_Zoo.Print();
                        Console.ReadKey();
                        break;
                    }

                    case 5: {
                        try {
                            Console.WriteLine("\n   Choose one of the following:");
                            Console.WriteLine("     1. Sort by number");
                            Console.WriteLine("     2. Sort by age");
                            Console.WriteLine("     3. Sort by name");
                            Console.Write("\nEnter the number of choosing action: ");
                            int i = Convert.ToInt32(Console.ReadLine());
                            if (i < 1 || i > 3) {
                                Console.WriteLine("Wrong data! Try again at the next time");
                            }
                            else {
                                NY_Zoo.Sort(i);
                                NY_Zoo.Print();
                                Console.ReadKey();
                            }
                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                            Console.ReadKey();
                        }
                        
                        break;
                    }

                    case 6: {
                        try {
                            Console.WriteLine("\n   Choose one of the following:");
                            Console.WriteLine("     1. Find by number");
                            Console.WriteLine("     2. Find by age");
                            Console.WriteLine("     3. Find by name");
                            Console.Write("\nEnter the number of choosing action: ");
                            int i = Convert.ToInt32(Console.ReadLine());
                            if (i < 1 || i > 3) {
                                Console.WriteLine("Wrong data! Try again at the next time");
                            }
                            else {
                                NY_Zoo.Find(i);
                                Console.ReadKey();
                            }
                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                            Console.ReadKey();
                        }
                        
                        break;
                    }

                    case 7: {
                        item = 0;
                        break;
                    }
                }

                if (item == 0) break;
            } while (true);
        }

        /*static void Main()
        {
            
            Mammals m = new Mammals(1, 3, "Av", 1, 0);
            Mammals l = new Mammals(2, 2, "Ab", 0, 1);
            Zoo a = new Zoo();
            a.Add_elem(m);
            a.Add_elem(l);
            
            a.Print();
            a.Sort(1);
            a.Print();
            a.Sort(3);
            a.Print();

        }*/
    }
}