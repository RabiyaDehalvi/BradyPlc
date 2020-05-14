using BradlyPlc.Test.Test.DTOs.InputGeneration;
using BradlyPlc.Test.Test.DTOs.OutputGeneration;
using BradlyPlc.Test.Test.DTOs.ReferenceData;
using Microsoft.Extensions.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace BradlyPlc.Test.Test.Helpers
{
    public class XmlDataSourceProcessor
    {
        IConfigurationRoot _config;
        public XmlDataSourceProcessor()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("specflow.json").Build();
        }

        public InputGenerationReport GetGenerationReport()
        {
            XmlSerializer serilizer = new XmlSerializer(typeof(InputGenerationReport));
            return (InputGenerationReport)serilizer
                .Deserialize(XmlReader.Create(_config.GetSection("inputFile").Value));
        }

        public GenerationOutput GetGenerationOutput()
        {
            XmlSerializer serilizer = new XmlSerializer(typeof(GenerationOutput));
            return (GenerationOutput)serilizer
                .Deserialize(XmlReader.Create(_config.GetSection("outputFile").Value));
        }

        public ReferenceData GetReferenceData()
        {
            XmlSerializer serilizer = new XmlSerializer(typeof(ReferenceData));
            return (ReferenceData)serilizer
                .Deserialize(XmlReader.Create(_config.GetSection("referenceFile").Value));
        }

    }
}
