using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Solid_Principles
{
    public static class DependencyInversion
    {
        public static void Run()
        {
            var parent = new Person() { Name = "John" };
            var child1 = new Person() { Name = "Mary" };
            var child2 = new Person() { Name = "Nick" };

            var relationships = new RelatioShips();
            relationships.AddRelation(parent, child1);
            relationships.AddRelation(parent, child2);

            var search = new Research(relationships);
        }
    }

    public enum Relations
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public interface IFindRelation
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    public class RelatioShips: IFindRelation
    {

        private List<(Person, Relations, Person)> relations = new List<(Person, Relations, Person)>();

        public List<(Person, Relations, Person)> RelationList { get => relations; set => relations = value; }

        public void AddRelation(Person person1, Person person2)
        {
            relations.Add((person1, Relations.Parent, person2));
            relations.Add((person2, Relations.Child, person1));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(x => x.Item1.Name == name && x.Item2 == Relations.Parent).Select(y=>y.Item3);

            //return from relation in relations.Where(x => x.Item1.Name == name && x.Item2 == Relations.Parent)
            //       select relation.Item3;

            //foreach (var relation in relations.Where(x => x.Item1.Name == name && x.Item2 == Relations.Parent))
            //{
            //    yield return relation.Item3;
            //}
        }
    }

    public class Research
    {
        public Research(IFindRelation relationShips)
        {
            foreach (var person in relationShips.FindAllChildrenOf("John"))
            {
                Console.WriteLine($"John has a child whos name is {person.Name}");
            }
        }
    }
}
