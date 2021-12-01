using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string conString = "Data Source=DESKTOP-K30E79F;Initial Catalog=WCFReservasi;Persist Security Info=True;User ID=sa;Password=A12zpanMDO";
        SqlConnection connection;
        SqlCommand com;

        public string deletePemesanan(string idPemesanan)
        {
            throw new NotImplementedException();
        }

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>();
            try
            {
                string sql = "select ID_Lokasi, Nama_Lokasi, Deskripsi_Full, Kuota from dbo.Lokasi";
                connection = new SqlConnection(conString);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    DetailLokasi data = new DetailLokasi();
                    data.idLokasi = reader.GetString(0);
                    data.namaLokasi = reader.GetString(1);
                    data.deskripsiFull = reader.GetString(2);
                    data.kuota = reader.GetInt32(3);
                    LokasiFull.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }

        public string editPemesanan(string idPemesanan, string namaCustomer)
        {
            throw new NotImplementedException();
        }

        public string pemesanan(string idPemesanan, string namaCustomer, string noTelepon, int jumlahPemesanan, string idLokasi)
        {
            string a = "gagal";
            try
            {
                string sql = "insert into dbo.Pemesanan values('"+idPemesanan+"','"+namaCustomer+"','"+noTelepon+"',"+jumlahPemesanan+",'"+idLokasi+"')";
                connection = new SqlConnection(conString);
                com = new SqlCommand(sql,connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();
                a = "sukses";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return a;
        }

        public List<Pemesanan> Pemesanan()
        {
            throw new NotImplementedException();
        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }
    }
}
