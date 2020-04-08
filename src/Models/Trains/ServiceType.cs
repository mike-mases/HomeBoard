using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="ServiceType")]
	public class ServiceType {
		[XmlAttribute(AttributeName="Type")]
		public string Type { get; set; }
	}

}
