using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="ArriveTime")]
	public class ArriveTime {
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
		[XmlAttribute(AttributeName="Arrived")]
		public string Arrived { get; set; }
		[XmlAttribute(AttributeName="timestamp")]
		public string Timestamp { get; set; }
	}

}
