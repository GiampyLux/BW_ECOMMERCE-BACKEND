using BW_ECOMMERCE.Models;
using System.Data;
using System.Data.SqlClient;

namespace BW_ECOMMERCE.Services
{
    public class UtenteService : IUtenteService
    {
        private readonly DatabaseContext _context;

        public UtenteService(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Utente> GetUtenti()
        {
            List<Utente> utenti = new List<Utente>();

            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "SELECT * FROM Utenti";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Utente utente = new Utente
                            {
                                IdUtente = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Cognome = reader.GetString(2),
                                Password = reader.GetString(3),
                                Img = reader.GetString(4),
                                Username = reader.GetString(5),
                                DataNascita = reader.GetDateTime(6),
                                Email = reader.GetString(7),
                                Indirizzo = reader.GetString(8),
                            };
                            utenti.Add(utente);
                        }
                    }
                }
            }

            return utenti;
        }

        public Utente GetUtenteById(int id)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "SELECT * FROM Utenti WHERE IdUtente = @Id";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Utente
                            {
                                IdUtente = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Cognome = reader.GetString(2),
                                Password = reader.GetString(3),
                                Img = reader.GetString(4),
                                Username = reader.GetString(5),
                                DataNascita = reader.GetDateTime(6),
                                Email = reader.GetString(7),
                                Indirizzo = reader.GetString(8),
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void InsertUtente(Utente utente)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = @"INSERT INTO Utenti (IdUtente, Nome, Cognome, Password, Img, Username, Data_nascita, Email, Indirizzo)
                                 VALUES (@IdUtente, @Nome, @Cognome, @Password, @Img, @Username, @Data_nascita, @Email, @Indirizzo)";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@IdUtente", utente.IdUtente);
                    cmd.Parameters.AddWithValue("@Nome", utente.Nome);
                    cmd.Parameters.AddWithValue("@Cognome", utente.Cognome);
                    cmd.Parameters.AddWithValue("@Password", utente.Password);
                    cmd.Parameters.AddWithValue("@Img", utente.Img);
                    cmd.Parameters.AddWithValue("@Username", utente.Username);
                    cmd.Parameters.AddWithValue("@Data_nascita", utente.DataNascita);
                    cmd.Parameters.AddWithValue("@Email", utente.Email);
                    cmd.Parameters.AddWithValue("@Indirizzo", utente.Indirizzo);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUtente(Utente utente)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = @"UPDATE Utenti
                                 SET IdUtente = @IdUtente,
                                     Nome = @Nome,
                                     Cognome = @Cognome,
                                     Password = @Password,
                                     Img = @Img
                                     Username = @Username
                                     Data_nascita = @Data_nascita
                                     Email = @Email
                                     Indirizzo = @Indirizzo        
                                 WHERE IdUtente = @IdUtente";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", utente.Nome);
                    cmd.Parameters.AddWithValue("@Cognome", utente.Cognome);
                    cmd.Parameters.AddWithValue("@Password", utente.Password);
                    cmd.Parameters.AddWithValue("@Img", utente.Img);
                    cmd.Parameters.AddWithValue("@Username", utente.Username);
                    cmd.Parameters.AddWithValue("@Data_nascita", utente.DataNascita);
                    cmd.Parameters.AddWithValue("@Email", utente.Email);
                    cmd.Parameters.AddWithValue("@Indirizzo", utente.Indirizzo);
                    cmd.Parameters.AddWithValue("@IdUtente", utente.IdUtente);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUtente(int id)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "DELETE FROM Utenti WHERE IdUtente = @Id";

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

