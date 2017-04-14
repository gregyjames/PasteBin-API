using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace PasteBin_API
{
    [XmlRoot(ElementName = "paste")]
    public class Paste
    {
        [XmlElement(ElementName = "paste_key")]
        public string Paste_key { get; set; }
        [XmlElement(ElementName = "paste_date")]
        public string Paste_date { get; set; }
        [XmlElement(ElementName = "paste_title")]
        public string Paste_title { get; set; }
        [XmlElement(ElementName = "paste_size")]
        public string Paste_size { get; set; }
        [XmlElement(ElementName = "paste_expire_date")]
        public string Paste_expire_date { get; set; }
        [XmlElement(ElementName = "paste_private")]
        public string Paste_private { get; set; }
        [XmlElement(ElementName = "paste_format_long")]
        public string Paste_format_long { get; set; }
        [XmlElement(ElementName = "paste_format_short")]
        public string Paste_format_short { get; set; }
        [XmlElement(ElementName = "paste_url")]
        public string Paste_url { get; set; }
        [XmlElement(ElementName = "paste_hits")]
        public string Paste_hits { get; set; }
    }

    [XmlRoot(ElementName = "Pastes")]
    public class Pastes
    {
        [XmlElement(ElementName = "paste")]
        public List<Paste> Paste { get; set; }
    }

}
