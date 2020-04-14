using System.Xml.Serialization;

namespace HomeBoard.Models.Trains
{
    [XmlRoot(ElementName = "SpecialNotice")]
    public class SpecialNotice
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
}