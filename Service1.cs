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

        public List<DataRegister> dataRegist()
        {
            List<DataRegister> list = new List<DataRegister>();
            try
            {
                string sql = "select ID_Login,Username,Password,Kategori from dbo.Login";
                connection = new SqlConnection(conString);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    DataRegister data = new DataRegister();
                    data.id = reader.GetInt32(0);
                    data.username = reader.GetString(1);
                    data.password = reader.GetString(2);
                    data.kategori = reader.GetString(3);
                    list.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return list;
        }

        public string deletePemesanan(string idPemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "delete from dbo.Pemesanan where ID_Reservasi = '"+idPemesanan+"'";
                connection = new SqlConnection(conString);
                com = new SqlCommand(sql,connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();
                a = "sukses";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return a;
        }

        public string deleteRegister(string username)
        {
            try
            {
                int id = 0;
                string sql = "select ID_Login from dbo.Login where Username = '" + username + "'";
                connection = new SqlConnection(conString);
                com = new SqlCommand(sql,connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                connection.Close();
                string sql2 = "delete from dbo.Login where ID_Login = " + id + "";
                connection = new SqlConnection(conString);
                com = new SqlCommand(sql2,connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
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

        public string editPemesanan(string idPemesanan, string namaCustomer, string noTelepon)
        {
            string a = "gagal";
            try
            {
                string sql = "update dbo.Pemesanan set Nama_Customer='"+namaCustomer+"', No_Telepon='"+noTelepon+"' where ID_Reservasi='"+idPemesanan+"'";
                connection = new SqlConnection(conString);
                com= new SqlCommand(sql, connection);
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

        public string Login(string username, string password)
        {
            string kategori = "";

            string sql = "select Kategori from dbo.Login where Username = '"+ username + "' and Password = '" + password +"'";
            connection = new SqlConnection(conString);
            com = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                kategori = reader.GetString(0);
            }

            return kategori;
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

                string sql2 =  "update dbo.Lokasi set Kuota = Kuota - "+jumlahPemesanan+" where ID_Lokasi ='"+idLokasi+"'";
                connection = new SqlConnection(conString);
                com = new SqlCommand (sql2,connection);
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
            List<Pemesanan> pemesan = new List<Pemesanan>();
            try
            {
                string sql = "select ID_Reservasi, Nama_Customer, No_Telepon, Jumlah_Pemesanan, " +
                    "Nama_Lokasi from dbo.Pemesanan p join dbo.Lokasi l on p.ID_Lokasi = l.ID_Lokasi";
                connection = new SqlConnection(conString);
                com = new SqlCommand(sql,connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Pemesanan data = new Pemesanan();
                    data.idPemesanan = reader.GetInt32(0).ToString();
                    data.namaCustomer = reader.GetString(1);
                    data.noTelepon = reader.GetString(2);
                    data.jumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    pemesan.Add(data);
                }
                connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pemesan;
        }

        public string Register(string username, string password, string kategori)
        {
            try
            {
                string sql = "insert into dbo.Login values('" + username + "','" + password + "','" + kategori + "')";
                connection = new SqlConnection(conString);
                com = new SqlCommand(sql,connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }

        public string updateRegister(string username, string password, string kategori, int id)
        {
            try
            {
                string sql = "update dbo.Login set Username = '" + username + "', Password = '" + password + "', Kategori = '" + kategori + "' where ID_Login =" + id + "";
                connection = new SqlConnection(conString);
                com = new SqlCommand (sql,connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
