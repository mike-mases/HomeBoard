using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="ExpectedDepartStatus")]
	public class ExpectedDepartStatus {
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
	}

}
