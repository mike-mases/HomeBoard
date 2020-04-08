using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="Service")]
	public class Service {
		[XmlElement(ElementName="ServiceType")]
		public ServiceType ServiceType { get; set; }
		[XmlElement(ElementName="ArriveTime")]
		public ArriveTime ArriveTime { get; set; }
		[XmlElement(ElementName="DepartTime")]
		public DepartTime DepartTime { get; set; }
		[XmlElement(ElementName="Platform")]
		public Platform Platform { get; set; }
		[XmlElement(ElementName="SecondaryServiceStatus")]
		public string SecondaryServiceStatus { get; set; }
		[XmlElement(ElementName="ServiceStatus")]
		public ServiceStatus ServiceStatus { get; set; }
		[XmlElement(ElementName="ExpectedDepartTime")]
		public ExpectedDepartTime ExpectedDepartTime { get; set; }
		[XmlElement(ElementName="ExpectedArriveTime")]
		public ExpectedArriveTime ExpectedArriveTime { get; set; }
		[XmlElement(ElementName="Delay")]
		public Delay Delay { get; set; }
		[XmlElement(ElementName="ExpectedDepartStatus")]
		public ExpectedDepartStatus ExpectedDepartStatus { get; set; }
		[XmlElement(ElementName="ExpectedArriveStatus")]
		public ExpectedArriveStatus ExpectedArriveStatus { get; set; }
		[XmlElement(ElementName="DelayCause")]
		public string DelayCause { get; set; }
		[XmlElement(ElementName="LastReport")]
		public LastReport LastReport { get; set; }
		[XmlElement(ElementName="LastPosition")]
		public LastPosition LastPosition { get; set; }
		[XmlElement(ElementName="CommentLine")]
		public string CommentLine { get; set; }
		[XmlElement(ElementName="CommentLine2")]
		public string CommentLine2 { get; set; }
		[XmlElement(ElementName="ArrivalComment1")]
		public string ArrivalComment1 { get; set; }
		[XmlElement(ElementName="ArrivalComment2")]
		public string ArrivalComment2 { get; set; }
		[XmlElement(ElementName="PlatformComment1")]
		public string PlatformComment1 { get; set; }
		[XmlElement(ElementName="PlatformComment2")]
		public string PlatformComment2 { get; set; }
		[XmlElement(ElementName="DepartureComment1")]
		public string DepartureComment1 { get; set; }
		[XmlElement(ElementName="DepartureComment2")]
		public string DepartureComment2 { get; set; }
		[XmlElement(ElementName="AssociatedPageNotices")]
		public string AssociatedPageNotices { get; set; }
		[XmlElement(ElementName="ChangeAt")]
		public string ChangeAt { get; set; }
		[XmlElement(ElementName="Operator")]
		public Operator Operator { get; set; }
		[XmlElement(ElementName="Origin1")]
		public StartStation StartStation { get; set; }
		[XmlElement(ElementName="Destination1")]
		public EndStation EndStation { get; set; }
		[XmlElement(ElementName="Via")]
		public string Via { get; set; }
		[XmlElement(ElementName="Coaches1")]
		public int CoachesCount { get; set; }
		[XmlElement(ElementName="CoachesList")]
		public string CoachesList { get; set; }
		[XmlElement(ElementName="Incident")]
		public string Incident { get; set; }
		[XmlElement(ElementName="Dest1CallingPoints")]
		public EndStationCallingPoints EndStationCallingPoints { get; set; }
		[XmlAttribute(AttributeName="Headcode")]
		public string Headcode { get; set; }
		[XmlAttribute(AttributeName="Uid")]
		public string Uid { get; set; }
		[XmlAttribute(AttributeName="RetailID")]
		public string RetailID { get; set; }
		[XmlAttribute(AttributeName="TigerID")]
		public string TigerID { get; set; }
	}

}
