using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace HomeBoard.Models.Trains
{

    [XmlRoot(ElementName="StationBoard")]
	public class StationBoard {
		[XmlElement(ElementName="TridentId")]
		public string TridentId { get; set; }
		[XmlElement(ElementName="Service")]
		public List<Service> Services { get; set; }
		[XmlElement(ElementName="Incident")]
		public Incident Incident { get; set; }
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="tiploc")]
		public string Tiploc { get; set; }
		[XmlAttribute(AttributeName="crs")]
		public string Crs { get; set; }
		[XmlAttribute(AttributeName="PlatformData")]
		public string PlatformData { get; set; }
		[XmlAttribute(AttributeName="Timestamp")]
		public string Timestamp { get; set; }
		[XmlElement(ElementName="SpecialNotices")]
		public SpecialNotices SpecialNotices { get; set; }

		public DateTime TimestampParsed {
			get {
				return Timestamp != null ? DateTime.ParseExact(Timestamp, "dd/MM/yyyy HH:mm:ss", null) : DateTime.MinValue;
			}
		}
	}
}
