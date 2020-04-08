using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="ExpectedArriveStatus")]
	public class ExpectedArriveStatus {
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
	}

}
