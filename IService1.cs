using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string pemesanan(string idPemesanan, string namaCustomer, string noTelepon, int jumlahPemesanan, string idLokasi);
        [OperationContract]
        string editPemesanan(string idPemesanan, string namaCustomer, string noTelepon);
        [OperationContract]
        string deletePemesanan(string idPemesanan);
        [OperationContract]
        List<CekLokasi> ReviewLokasi();
        [OperationContract]
        List<DetailLokasi> DetailLokasi();
        [OperationContract]
        List<Pemesanan> Pemesanan();
    }

    [DataContract]
    public class CekLokasi
    {
        [DataMember]
        public string idLokasi { get; set; }
        [DataMember]
        public string namaLokasi { get; set; }
        [DataMember]
        public string deskripsiSingkat { get; set; }
    }

    [DataContract]
    public class DetailLokasi
    {
        [DataMember]
        public string idLokasi { get; set; }
        [DataMember]
        public string namaLokasi { get; set; }
        [DataMember]
        public string deskripsiFull { get; set; }
        [DataMember]
        public int kuota { get; set; }
    }

    [DataContract]
    public class Pemesanan
    {
        [DataMember]
        public string idPemesanan { get; set; }
        [DataMember]
        public string namaCustomer { get; set; }
        [DataMember]
        public string noTelepon { get; set; }
        [DataMember]
        public int jumlahPemesanan { get; set; }
        [DataMember]
        public string Lokasi { get; set; }
    }

    
}
