using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mantis_tests
{
    [Table(Name = "mantis_project_table")]
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public ProjectData() { }
        public ProjectData(string name) 
        {
            Name = name;
        }

        [Column(Name = "name")]
        public string Name { get; set; }
        [Column(Name="description")]
        public string Description { get; set; }
        [Column(Name = "id")]
        public int Id { get; set; }
        public int CompareTo(ProjectData other)
        {
            return other is null
                ? 1
                : Name.CompareTo(other.Name);
        }
        /*
        public override string ToString() // закомментировано потому что иначе тесты из VS не запускаются
        {
            return "name=" + Name + ";\ndescription= " + Description;
        }
        */
        public bool Equals(ProjectData other)
        {
            return !(other is null)
            && (ReferenceEquals(this, other) || Name == other.Name);
        }
        public static List<ProjectData> GetProjects()
        {
            using (MantisDB dB = new MantisDB())
            {
                return (from p in dB.Projects select p).ToList();
            }
        }
    }
}
