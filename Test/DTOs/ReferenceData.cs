using System.Xml.Serialization;
namespace BradlyPlc.Test.Test.DTOs.ReferenceData
{
	[XmlRoot(ElementName = "ValueFactor")]
	public class ValueFactor
	{
		[XmlElement(ElementName = "High")]
		public decimal High { get; set; }
		[XmlElement(ElementName = "Medium")]
		public decimal Medium { get; set; }
		[XmlElement(ElementName = "Low")]
		public decimal Low { get; set; }
	}

	[XmlRoot(ElementName = "EmissionsFactor")]
	public class EmissionsFactor
	{
		[XmlElement(ElementName = "High")]
		public decimal High { get; set; }
		[XmlElement(ElementName = "Medium")]
		public decimal Medium { get; set; }
		[XmlElement(ElementName = "Low")]
		public decimal Low { get; set; }
	}

	[XmlRoot(ElementName = "Factors")]
	public class Factors
	{
		[XmlElement(ElementName = "ValueFactor")]
		public ValueFactor ValueFactor { get; set; }
		[XmlElement(ElementName = "EmissionsFactor")]
		public EmissionsFactor EmissionsFactor { get; set; }
	}

	[XmlRoot(ElementName = "ReferenceData")]
	public class ReferenceData
	{
		[XmlElement(ElementName = "Factors")]
		public Factors Factors { get; set; }
	}

}
