using System.Xml.Serialization;
using System.Collections.Generic;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="Dest1CallingPoints")]
	public class EndStationCallingPoints {
		[XmlElement(ElementName="CallingPoint")]
		public List<CallingPoint> CallingPoints { get; set; }
		[XmlAttribute(AttributeName="NumCallingPoints")]
		public string NumCallingPoints { get; set; }
	}

}
