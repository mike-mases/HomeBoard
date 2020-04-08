using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="Incident")]
	public class Incident {
		[XmlAttribute(AttributeName="Summary")]
		public string Summary { get; set; }
	}

}
