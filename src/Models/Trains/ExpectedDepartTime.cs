using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="ExpectedDepartTime")]
	public class ExpectedDepartTime {
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
	}

}
