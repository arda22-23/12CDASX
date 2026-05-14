using Rocket.API;
using System.Xml.Serialization;

namespace RPEkonomi
{
    public class EkonomiConfig : IRocketPluginConfiguration
    {
        [XmlElement("BaslangicParasi")]
        public decimal BaslangicParasi { get; set; }

        [XmlElement("MaxPara")]
        public decimal MaxPara { get; set; }

        [XmlElement("MinTransferMiktar")]
        public decimal MinTransferMiktar { get; set; }

        [XmlElement("TransferVergiYuzdesi")]
        public decimal TransferVergiYuzdesi { get; set; }

        [XmlElement("ParaBirimiSembol")]
        public string ParaBirimiSembol { get; set; }

        public void LoadDefaults()
        {
            BaslangicParasi = 1000m;
            MaxPara = 10000000m;
            MinTransferMiktar = 1m;
            TransferVergiYuzdesi = 5m;   // %5 vergi
            ParaBirimiSembol = "₺";
        }
    }
}
