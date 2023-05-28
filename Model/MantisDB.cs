using LinqToDB;

namespace mantis_tests
{
    public class MantisDB : LinqToDB.Data.DataConnection
    {
        public MantisDB() : base("bugtracker") { }
        public ITable<ProjectData> Projects => this.GetTable<ProjectData>();
    }
}
