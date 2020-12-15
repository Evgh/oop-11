using System;
using System.Collections.Generic;
using System.Linq;

namespace oop_11
{
    partial class Abiturient
    {
        public static int Amount = 0;
        static int hashNum;
        const int DEFAULT_MARKS_AMOUNT = 3;

        readonly int _id;
        string _phone;

        public int Id { get; }
        public int[] Marks { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Father { get; set; }
        public string Adress { get; set; }
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                bool flag = true;
                if (value != null)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        if ((value[i] >= 48 && value[i] <= 57) || value[i] == 32 || value[i] == 43 || value[i] == 45)
                        {
                            continue;
                        }
                        else
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                else
                {
                    flag = false;
                }

                if (flag)
                {
                    _phone = value;
                }

            }
        }
        static Abiturient()
        {
            Amount = 0;
            Random rrr = new Random();
            hashNum = rrr.Next();
        }
        public Abiturient() : this(null) { }

        public Abiturient(string name, string surname, int[] marks) : this(marks, name, surname, null, null, null) { }
        public Abiturient(int[] marks = null, string name = "Kolya", string surname = "Bovkun", string father = "Fatherless", string adress = "BGTU", string phone = "911") : this(10, marks, name, surname, father, adress, phone) { }
        Abiturient(int id, int[] marks, string name, string surname, string father, string adress, string phone)
        {
            _id = hashNum + Amount;

            if (marks != null)
            {
                Marks = marks;
            }
            else
            {
                Marks = new int[DEFAULT_MARKS_AMOUNT];
            };

            Name = name;
            Surname = surname;
            Father = father;
            Adress = adress;
            Phone = phone;
            Amount++;
        }
        public override string ToString()
        {
            string marks = "";
            foreach (int x in Marks)
            {
                marks += x.ToString() + " ";
            }

            return Name + " " + Surname + " " + Father + " " + Adress + " " + Phone + " |Marks: " + marks;
        }
        public int GetMinMark()
        {
            int min = Marks[0];
            foreach (int x in Marks)
            {
                min = x < min ? x : min;
            }
            return min;
        }

/*        public double Geе(out double sum, ref bool isGood)
        {
            sum = 0;
            isGood = true;
            foreach (int x in Marks)
            {
                sum += x;
                isGood = isGood && x >= 4;
            }
            return sum;
        }*/
    }
    class Person
    {
        public string Status { get; set; }
        public string Sex { get; set; }
        public Person (string str, string s)
        {
            Status = str;
            Sex = s;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            int num;
            try
            {
                num = Convert.ToInt32(Console.ReadLine());
            }
            catch(FormatException e)
            {
                num = 5;
            }
            string[] mounths = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            var mounthX = from m in mounths
                          where m.Length <= num
                          select m;

            mounthX.ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine();

            //////

            List<Abiturient> abitura = new List<Abiturient> { 
                new Abiturient("Anton", "Surdov", new int[] { 4, 2, 4, 5}), 
                new Abiturient("Anton", "Lityagin", new int[] { 6, 7, 10, 10}),
                new Abiturient("Nastya", "Voikel", new int[] { 4, 2, 4, 5}),
                new Abiturient("Denis", "Bozhko", new int[] { 8, 8, 8, 8}),
                new Abiturient("Artem", "Orlov", new int[] { 10, 10, 10, 10}),
                new Abiturient("Valera", "Velichko", new int[] { 7, 7, 5, 10}),
                new Abiturient("Evgenia", "Kasperovich", new int[] { 5, 6, 10, 10}),
                new Abiturient("Kolya", "Bovkun", new int[] { 8, 4, 2, 7}),
            };

            var bad = from a in abitura
                      where a.GetMinMark() < 4
                      select a;
            Console.WriteLine("Неуспевающие студенты: ");
            bad.ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine();


            Console.WriteLine("Студенты с суммой баллов больше 20: ");
            (from a in abitura 
             where a.Marks.Sum() > 20 
             select a).ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine();


            Console.WriteLine( $"Количество абитуриентов с десятками: {(from a in abitura where a.Marks[3] == 10 select a).ToList().Count }\n");


            (from a in abitura
             orderby a.Name
             select a).ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine();

//            (abitura.OrderBy(x => x.Name).ToList()).ForEach(x => Console.WriteLine(x));
//            Console.WriteLine();


            Console.WriteLine("4 студента с самой низкой успеваемостью: ");
            abitura.OrderBy(x => x.Marks.Sum()).Take(4).ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine();



            (from a in abitura
             where a.Name[0] == 'A'
             orderby a.Marks.Sum() * (-1)
             select new
             {
                 a.Name,
                 a.Marks,
                 Property = "Cool"
             }).ToList().ForEach(x => Console.WriteLine($" {x.Name} | Сумма баллов: {x.Marks.Sum()} | {x.Property}"));
            Console.WriteLine();

            //

            List<Person> ppp = new List<Person>
            {
                new Person("Anton", "male"),
                new Person("Artem", "male"),
                new Person("Denis", "male"),
                new Person("Evgenia", "female"),
                new Person("Nastya", "female")
            };

            (from a in abitura
             join p in ppp on a.Name equals p.Status
             select new
             {
                 a.Name,
                 a.Surname,
                 p.Sex
             }).ToList().ForEach(x => Console.WriteLine(x));

        }
    }
}
