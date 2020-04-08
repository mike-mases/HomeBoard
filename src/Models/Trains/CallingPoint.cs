using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="CallingPoint")]
	public class CallingPoint {
		[XmlAttribute(AttributeName="Name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="tiploc")]
		public string Tiploc { get; set; }
		[XmlAttribute(AttributeName="crs")]
		public string Crs { get; set; }
		[XmlAttribute(AttributeName="ttarr")]
		public string Ttarr { get; set; }
		[XmlAttribute(AttributeName="ttdep")]
		public string Ttdep { get; set; }
		[XmlAttribute(AttributeName="etarr")]
		public string Etarr { get; set; }
		[XmlAttribute(AttributeName="etdep")]
		public string Etdep { get; set; }
		[XmlAttribute(AttributeName="type")]
		public string Type { get; set; }
	}

}
