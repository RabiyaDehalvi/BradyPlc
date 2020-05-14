using System.Collections.Generic;
using System.Xml.Serialization;
namespace BradlyPlc.Test.Test.DTOs.OutputGeneration
{
	[XmlRoot(ElementName = "Generator")]
	public class Generator
	{
		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }
		[XmlElement(ElementName = "Total")]
		public decimal Total { get; set; }
	}

	[XmlRoot(ElementName = "Totals")]
	public class Totals
	{
		[XmlElement(ElementName = "Generator")]
		public List<Generator> Generator { get; set; }
	}

	[XmlRoot(ElementName = "Day")]
	public class Day
	{
		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }
		[XmlElement(ElementName = "Date")]
		public string Date { get; set; }
		[XmlElement(ElementName = "Emission")]
		public decimal Emission { get; set; }
	}

	[XmlRoot(ElementName = "MaxEmissionGenerators")]
	public class MaxEmissionGenerators
	{
		[XmlElement(ElementName = "Day")]
		public List<Day> Day { get; set; }
	}

	[XmlRoot(ElementName = "ActualHeatRates")]
	public class ActualHeatRates
	{
		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }
		[XmlElement(ElementName = "HeatRate")]
		public decimal HeatRate { get; set; }
	}

	[XmlRoot(ElementName = "GenerationOutput")]
	public class GenerationOutput
	{
		[XmlElement(ElementName = "Totals")]
		public Totals Totals { get; set; }
		[XmlElement(ElementName = "MaxEmissionGenerators")]
		public MaxEmissionGenerators MaxEmissionGenerators { get; set; }
		[XmlElement(ElementName = "ActualHeatRates")]
		public List<ActualHeatRates> ActualHeatRates { get; set; }
	}

}
