using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName = "SpecialNotices")]
    public class SpecialNotices
    {
        [XmlElement(ElementName = "SpecialNotice")]
        public SpecialNotice SpecialNotice { get; set; }
    }
}