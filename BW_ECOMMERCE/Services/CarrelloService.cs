using BW_ECOMMERCE.Models;
using System.Data;
using System.Data.SqlClient;

namespace BW_ECOMMERCE.Services
{
    public class CarrelloService : ICarrelloService
    {
        private readonly DatabaseContext _context;

        public CarrelloService(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Carrello> GetCarrelli()
        {
            List<Carrello> carrelli = new List<Carrello>();

            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "SELECT * FROM Carrelli";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Carrello carrello = new Carrello
                            {
                                IdCarrello = reader.GetInt32(0),
                                IdUtenteFK = reader.GetInt32(1),
                                IdProdottoFK = reader.GetInt32(2),
                                Confermato = reader.GetBoolean(3),
                                Presente = reader.GetBoolean(4),
                                Qnty = reader.GetInt32(5)
                            };

                            carrelli.Add(carrello);
                        }
                    }
                }
            }

            return carrelli;
        }

        public IEnumerable<CarrelloView> GetCarrelloView()
        {
            List<CarrelloView> carrelli = new List<CarrelloView>();

            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "SELECT c.IdCarrello,p.NomeProdotto , SUM(p.Prezzo * c.Qnty) as Totale, c.Qnty  " +
                    "FROM Carrelli c " +
                    "JOIN Prodotti p ON c.IdProdottoFK = p.IdProdotto " +
                    "WHERE c.IdUtenteFK = 30 and c.Presente = 1  " +
                    "GROUP BY c.IdCarrello, p.NomeProdotto, c.Qnty";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@IdUtenteFK", 1);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CarrelloView carrello = new CarrelloView
                            {
                                Id = reader.GetInt32(0),
                                NomeProdotto = reader.GetString(1),
                                Prezzo = reader.GetDecimal(2),
                                Quatita = reader.GetInt32(3),
                            };

                            carrelli.Add(carrello);
                        }
                    }
                }
            }

            return carrelli;
        }

        public CarrelloView GetCarrelloById(int id)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "SELECT c.IdCarrello p.NomeProdotto, SUM(p.prezzo * c.Qnty) as Totale, c.Qnty " +
                    "FROM Carrelli c JOIN Prodotti p ON c.IdProdottoFK = p.IdProdotto " +
                    "WHERE c.IdUtenteFK = @IdUtenteFK and c.Presente = 1";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@IdUtenteFK", 1);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CarrelloView
                            {
                                Id = reader.GetInt32(0),
                                NomeProdotto = reader.GetString(1),
                                Prezzo = reader.GetDecimal(2),
                                Quatita = reader.GetInt32(3),

                            };
                        }
                    }
                }
            }

            return null;
        }

        public void InsertCarrello(Carrello carrello)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = @"EXEC [dbo].[AddOrUpdateCarrello] @IdUtente = @IdUtenteFK, @IdProdotto = @IdProdottoFK, @Qnty = @Qnty;";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@IdUtenteFK", carrello.IdUtenteFK);
                    cmd.Parameters.AddWithValue("@IdProdottoFK", carrello.IdProdottoFK);
                    cmd.Parameters.AddWithValue("@Confermato", carrello.Confermato);
                    cmd.Parameters.AddWithValue("@Presente", carrello.Presente);
                    cmd.Parameters.AddWithValue("@Qnty", carrello.Qnty);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCarrello(Carrello carrello)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = @"UPDATE Carrelli
                                 SET IdUtenteFK = @IdUtenteFK,
                                     IdProdottoFK = @IdProdottoFK,
                                     Confermato = @Confermato,
                                     Presente = @Presente,
                                     Qnty = @Qnty
                                 WHERE IdCarrello = @IdCarrello";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@IdUtenteFK", carrello.IdUtenteFK);
                    cmd.Parameters.AddWithValue("@IdProdottoFK", carrello.IdProdottoFK);
                    cmd.Parameters.AddWithValue("@Confermato", carrello.Confermato);
                    cmd.Parameters.AddWithValue("@Presente", carrello.Presente);
                    cmd.Parameters.AddWithValue("@Qnty", carrello.Qnty);
                    cmd.Parameters.AddWithValue("@IdCarrello", carrello.IdCarrello);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CompraCarrello(int Id)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = @"UPDATE Carrelli
                                 SET Confermato = 1,
                                     Presente = 0
                                 WHERE IdCarrello = @IdCarrello";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@IdCarrello", Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCarrello(int id)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "DELETE FROM Carrelli WHERE IdCarrello = @IdCarrello";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@IdCarrello", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ToglidaCarrello(int id)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "UPDATE Carrelli set Presente = 0 WHERE IdCarrello = @IdCarrello";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@IdCarrello", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
