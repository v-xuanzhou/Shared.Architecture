
namespace Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable
{
    public class Astrotable
    {
        public AstroTableColumn[] Columns { get; set; }
        public List<object> Rows { get; set; }
        public object[] Errors { get; set; }

        public string Name;

        public Astrotable() { }
        
        public Astrotable(string name)
        {
            Name = name;
        }
    }

    public class AstroTableColumn
    {
        public string Name { get; set; }

        public AstroTableColumn() { }
        public AstroTableColumn(string columnName)
        {
            Name = columnName;
        }

        public AstroTableColumn Clone()
        {
            return new AstroTableColumn
            {
                Name = Name
            };
        }
    }

}
