using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="ServiceStatus")]
	public class ServiceStatus {
		[XmlAttribute(AttributeName="Status")]
		public string Status { get; set; }
	}

}
