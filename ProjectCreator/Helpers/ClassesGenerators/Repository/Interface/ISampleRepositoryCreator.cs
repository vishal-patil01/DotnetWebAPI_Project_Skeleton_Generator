namespace ProjectCreator.Helpers.ClassesGenerators.Repository.Interface
{
    internal class ISampleRepositoryCreator
    {
        public static string GetIRepositoryConfiguration(string projectName)
        {
            return @$"namespace {projectName}.Repository.Interface
{{
    public interface ISampleRepository
    {{
        List<string> GetSampleData();
    }}
}}
";
        }
    }
}
