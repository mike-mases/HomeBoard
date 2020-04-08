using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="Delay")]
	public class Delay {
		[XmlAttribute(AttributeName="Minutes")]
		public string Minutes { get; set; }
	}

}
