using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="DepartTime")]
	public class DepartTime {
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
		[XmlAttribute(AttributeName="timestamp")]
		public string Timestamp { get; set; }
		[XmlAttribute(AttributeName="sorttimestamp")]
		public string Sorttimestamp { get; set; }
	}

}
