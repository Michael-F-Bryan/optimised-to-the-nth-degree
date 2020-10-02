using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace cs
{
    class Program
    {
        static void Main(string[] args)
        {
            var iterations = args.Length >= 1 ? int.Parse(args[0]) : 10_000_000;

            var people = new List<Person>();

            people.Add(new Person() { Occupation = "1", Id = -9 });

            for (int i = 0; i < iterations; i++)
            {
                people.Add(new Person() { Occupation = i.ToString(), Id = i });
            }

            var stopwatch = Stopwatch.StartNew();
            var result = GroupByOccupation(people);

            Console.WriteLine("C# processed {0:#,##0} people in {1} at {2:f} us/iteration",
                              iterations,
                              stopwatch.Elapsed.TotalSeconds,
                              stopwatch.Elapsed.TotalMilliseconds * 1000 / iterations);
        }

        static List<OccupationGroup> GroupByOccupation(List<Person> people)
        {
            var occupations = new List<OccupationGroup>();
            var occupationMap = new Dictionary<string, OccupationGroup>();

            foreach (Person person in people)
            {
                if (!occupationMap.TryGetValue(person.Occupation, out var occupation))
                {
                    occupation = new OccupationGroup
                    {
                        People = new List<Person>(),
                        Occupation = person.Occupation
                    };

                    occupations.Add(occupation);
                    occupationMap.Add(person.Occupation, occupation);
                }

                occupation.People.Add(person);
            }

            return occupations;
        }
    }

    class Person
    {
        public string Occupation;
        public int Id;
    }

    class OccupationGroup
    {
        public string Occupation;
        public List<Person> People;
    }

}
