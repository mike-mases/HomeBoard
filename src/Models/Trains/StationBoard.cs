using System.Xml.Serialization;
using System.Collections.Generic;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName="ServiceType")]
	public class ServiceType {
		[XmlAttribute(AttributeName="Type")]
		public string Type { get; set; }
	}

	[XmlRoot(ElementName="ArriveTime")]
	public class ArriveTime {
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
		[XmlAttribute(AttributeName="Arrived")]
		public string Arrived { get; set; }
		[XmlAttribute(AttributeName="timestamp")]
		public string Timestamp { get; set; }
	}

	[XmlRoot(ElementName="DepartTime")]
	public class DepartTime {
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
		[XmlAttribute(AttributeName="timestamp")]
		public string Timestamp { get; set; }
		[XmlAttribute(AttributeName="sorttimestamp")]
		public string Sorttimestamp { get; set; }
	}

	[XmlRoot(ElementName="Platform")]
	public class Platform {
		[XmlAttribute(AttributeName="Number")]
		public string Number { get; set; }
		[XmlAttribute(AttributeName="Changed")]
		public string Changed { get; set; }
		[XmlAttribute(AttributeName="Parent")]
		public string Parent { get; set; }
	}

	[XmlRoot(ElementName="ServiceStatus")]
	public class ServiceStatus {
		[XmlAttribute(AttributeName="Status")]
		public string Status { get; set; }
	}

	[XmlRoot(ElementName="ExpectedDepartTime")]
	public class ExpectedDepartTime {
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
	}

	[XmlRoot(ElementName="ExpectedArriveTime")]
	public class ExpectedArriveTime {
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
	}

	[XmlRoot(ElementName="Delay")]
	public class Delay {
		[XmlAttribute(AttributeName="Minutes")]
		public string Minutes { get; set; }
	}

	[XmlRoot(ElementName="ExpectedDepartStatus")]
	public class ExpectedDepartStatus {
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
	}

	[XmlRoot(ElementName="ExpectedArriveStatus")]
	public class ExpectedArriveStatus {
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
	}

	[XmlRoot(ElementName="LastReport")]
	public class LastReport {
		[XmlAttribute(AttributeName="tiploc")]
		public string Tiploc { get; set; }
		[XmlAttribute(AttributeName="time")]
		public string Time { get; set; }
		[XmlAttribute(AttributeName="type")]
		public string Type { get; set; }
		[XmlAttribute(AttributeName="station1")]
		public string Station1 { get; set; }
		[XmlAttribute(AttributeName="station2")]
		public string Station2 { get; set; }
	}

	[XmlRoot(ElementName="LastPosition")]
	public class LastPosition {
		[XmlAttribute(AttributeName="SignalBox")]
		public string SignalBox { get; set; }
		[XmlAttribute(AttributeName="TDBerth")]
		public string TDBerth { get; set; }
	}

	[XmlRoot(ElementName="Operator")]
	public class Operator {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="code")]
		public string Code { get; set; }
		[XmlAttribute(AttributeName="brand")]
		public string Brand { get; set; }
	}

	[XmlRoot(ElementName="Origin1")]
	public class Origin1 {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName="tiploc")]
		public string Tiploc { get; set; }
		[XmlAttribute(AttributeName="crs")]
		public string Crs { get; set; }
	}

	[XmlRoot(ElementName="Destination1")]
	public class Destination1 {
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

	[XmlRoot(ElementName="Dest1CallingPoints")]
	public class Dest1CallingPoints {
		[XmlElement(ElementName="CallingPoint")]
		public List<CallingPoint> CallingPoint { get; set; }
		[XmlAttribute(AttributeName="NumCallingPoints")]
		public string NumCallingPoints { get; set; }
	}

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
		public Origin1 Origin1 { get; set; }
		[XmlElement(ElementName="Destination1")]
		public Destination1 Destination1 { get; set; }
		[XmlElement(ElementName="Via")]
		public string Via { get; set; }
		[XmlElement(ElementName="Coaches1")]
		public string Coaches1 { get; set; }
		[XmlElement(ElementName="CoachesList")]
		public string CoachesList { get; set; }
		[XmlElement(ElementName="Incident")]
		public string Incident { get; set; }
		[XmlElement(ElementName="Dest1CallingPoints")]
		public Dest1CallingPoints Dest1CallingPoints { get; set; }
		[XmlAttribute(AttributeName="Headcode")]
		public string Headcode { get; set; }
		[XmlAttribute(AttributeName="Uid")]
		public string Uid { get; set; }
		[XmlAttribute(AttributeName="RetailID")]
		public string RetailID { get; set; }
		[XmlAttribute(AttributeName="TigerID")]
		public string TigerID { get; set; }
	}

	[XmlRoot(ElementName="Incident")]
	public class Incident {
		[XmlAttribute(AttributeName="Summary")]
		public string Summary { get; set; }
	}

	[XmlRoot(ElementName="StationBoard")]
	public class StationBoard {
		[XmlElement(ElementName="TridentId")]
		public string TridentId { get; set; }
		[XmlElement(ElementName="Service")]
		public List<Service> Service { get; set; }
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
	}

}
