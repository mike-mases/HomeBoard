using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="LastReport")]
	public class LastReport {
		[XmlAttribute(AttributeName="tiploc")]
		public string Tiploc { get; set; }
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
		[XmlAttribute(AttributeName="type")]
		public string Type { get; set; }
		[XmlAttribute(AttributeName="station1")]
		public string OriginStation { get; set; }
		[XmlAttribute(AttributeName="station2")]
		public string DestinationStation { get; set; }
	}

}
