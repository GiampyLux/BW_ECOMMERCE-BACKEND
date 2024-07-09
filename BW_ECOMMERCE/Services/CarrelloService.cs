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

        public Carrello GetCarrelloById(int id)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "SELECT * FROM Carrelli WHERE IdCarrello = @Id";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Carrello
                            {
                                IdCarrello = reader.GetInt32(0),
                                IdUtenteFK = reader.GetInt32(1),
                                IdProdottoFK = reader.GetInt32(2),
                                Confermato = reader.GetBoolean(3),
                                Presente = reader.GetBoolean(4),
                                Qnty = reader.GetInt32(5)
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
                string query = @"INSERT INTO Carrelli (IdUtenteFK, IdProdottoFK, Confermato, Presente, Qnty)
                                 VALUES (@IdUtenteFK, @IdProdottoFK, @Confermato, @Presente, @Qnty)";

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

        public void DeleteCarrello(int id)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "DELETE FROM Carrelli WHERE IdCarrello = @Id";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
