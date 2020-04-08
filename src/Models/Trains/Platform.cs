using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="Platform")]
	public class Platform {
		[XmlAttribute(AttributeName="Number")]
		public string Number { get; set; }
		[XmlAttribute(AttributeName="Changed")]
		public string Changed { get; set; }
		[XmlAttribute(AttributeName="Parent")]
		public string Parent { get; set; }
	}

}
