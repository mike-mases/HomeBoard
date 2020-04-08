using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="LastPosition")]
	public class LastPosition {
		[XmlAttribute(AttributeName="SignalBox")]
		public string SignalBox { get; set; }
		[XmlAttribute(AttributeName="TDBerth")]
		public string TDBerth { get; set; }
	}

}
