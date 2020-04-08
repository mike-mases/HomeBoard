using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="Destination1")]
	public class EndStation {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="tiploc")]
		public string Tiploc { get; set; }
		[XmlAttribute(AttributeName="crs")]
		public string Crs { get; set; }
		[XmlAttribute(AttributeName="ttarr")]
		public string Ttarr { get; set; }
		[XmlAttribute(AttributeName="etarr")]
		public string Etarr { get; set; }
	}

}
