using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="ExpectedArriveTime")]
	public class ExpectedArriveTime {
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
	}

}
