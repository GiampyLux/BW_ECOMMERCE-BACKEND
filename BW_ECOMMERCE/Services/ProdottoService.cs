﻿using BW_ECOMMERCE.Models;
using System.Data;
using System.Data.SqlClient;

namespace BW_ECOMMERCE.Services
{
    public class ProdottoService : IProdottoService
    {
        private readonly DatabaseContext _context;

        public ProdottoService(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Prodotto> GetProdotti()
        {
            List<Prodotto> prodotti = new List<Prodotto>();

            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "SELECT * FROM Prodotti where Disponibile = 1";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Prodotto prodotto = new Prodotto
                            {
                                IdProdotto = reader.GetInt32(0),
                                NomeProdotto = reader.GetString(1),
                                Prezzo = reader.GetDecimal(2),
                                DescBreve = reader.GetString(3),
                                Descrizione = reader.GetString(4),
                                ImgProdotto = reader.IsDBNull(5) ? null : reader.GetString(5),
                                DataIns = reader.GetDateTime(6),
                                Quantity = reader.GetInt32(7),
                                CodProd = reader.GetInt32(8)
                            };

                            prodotti.Add(prodotto);
                        }
                    }
                }
            }

            return prodotti;
        }

        public Prodotto GetProdottoById(int id)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "SELECT * FROM Prodotti WHERE IdProdotto = @Id and Disponibile = 1";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Prodotto
                            {
                                IdProdotto = reader.GetInt32(0),
                                NomeProdotto = reader.GetString(1),
                                Prezzo = reader.GetDecimal(2),
                                DescBreve = reader.GetString(3),
                                Descrizione = reader.GetString(4),
                                ImgProdotto = reader.IsDBNull(5) ? null : reader.GetString(5),
                                DataIns = reader.GetDateTime(6),
                                Quantity = reader.GetInt32(7),
                                CodProd = reader.GetInt32(8)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void InsertProdotto(Prodotto prodotto)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = @"INSERT INTO Prodotti (NomeProdotto, Prezzo, DescBreve, Descrizione, ImgProdotto, DataIns, Quantity, CodProd)
                                 VALUES (@NomeProdotto, @Prezzo, @DescBreve, @Descrizione, @ImgProdotto, @DataIns, @Quantity, @CodProd)";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@NomeProdotto", prodotto.NomeProdotto);
                    cmd.Parameters.AddWithValue("@Prezzo", prodotto.Prezzo);
                    cmd.Parameters.AddWithValue("@DescBreve", prodotto.DescBreve);
                    cmd.Parameters.AddWithValue("@Descrizione", prodotto.Descrizione);
                    cmd.Parameters.AddWithValue("@ImgProdotto", (object)prodotto.ImgProdotto ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DataIns", prodotto.DataIns);
                    cmd.Parameters.AddWithValue("@Quantity", prodotto.Quantity);
                    cmd.Parameters.AddWithValue("@CodProd", prodotto.CodProd);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /*public void UpdateProdotto(Prodotto prodotto)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = @"UPDATE Prodotti
                         SET NomeProdotto = @NomeProdotto,
                             Prezzo = @Prezzo,
                             DescBreve = @DescBreve,
                             Descrizione = @Descrizione,
                             ImgProdotto = @ImgProdotto,
                             Quantity = @Quantity
                         WHERE IdProdotto = @IdProdotto";

                using (SqlCommand cmd = new SqlCommand(query, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@NomeProdotto", prodotto.NomeProdotto);
                    cmd.Parameters.AddWithValue("@Prezzo", prodotto.Prezzo);
                    cmd.Parameters.AddWithValue("@DescBreve", prodotto.DescBreve);
                    cmd.Parameters.AddWithValue("@Descrizione", prodotto.Descrizione);
                    cmd.Parameters.AddWithValue("@ImgProdotto", (object)prodotto.ImgProdotto ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Quantity", prodotto.Quantity);
                    cmd.Parameters.AddWithValue("@IdProdotto", prodotto.IdProdotto);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }*/

        public void UpdateProdotto(Prodotto prodotto)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string updateQuery = @"UPDATE Prodotti
                 SET NomeProdotto = @NomeProdotto,
                     Prezzo = @Prezzo,
                     DescBreve = @DescBreve,
                     Descrizione = @Descrizione,
                     ImgProdotto = @ImgProdotto,
                     Quantity = @Quantity
                 WHERE IdProdotto = @IdProdotto";
                // Controlla se ImgProdotto è null nel prodotto in arrivo
                if (prodotto.ImgProdotto == null)
                {
                    updateQuery = @"UPDATE Prodotti
                 SET NomeProdotto = @NomeProdotto,
                     Prezzo = @Prezzo,
                     DescBreve = @DescBreve,
                     Descrizione = @Descrizione,
                     
                     Quantity = @Quantity
                 WHERE IdProdotto = @IdProdotto";
                }
                else
                {
                    updateQuery = @"UPDATE Prodotti
                 SET NomeProdotto = @NomeProdotto,
                     Prezzo = @Prezzo,
                     DescBreve = @DescBreve,
                     Descrizione = @Descrizione,
                     ImgProdotto = @ImgProdotto,
                     Quantity = @Quantity
                 WHERE IdProdotto = @IdProdotto";
                }

                

                using (SqlCommand cmd = new SqlCommand(updateQuery, (SqlConnection)conn))
                {
                    cmd.Parameters.AddWithValue("@NomeProdotto", prodotto.NomeProdotto);
                    cmd.Parameters.AddWithValue("@Prezzo", prodotto.Prezzo);
                    cmd.Parameters.AddWithValue("@DescBreve", prodotto.DescBreve);
                    cmd.Parameters.AddWithValue("@Descrizione", prodotto.Descrizione);
                    cmd.Parameters.AddWithValue("@ImgProdotto", (object)prodotto.ImgProdotto ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Quantity", prodotto.Quantity);
                    cmd.Parameters.AddWithValue("@IdProdotto", prodotto.IdProdotto);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void DeleteProdotto(int id)
        {
            using (IDbConnection conn = _context.CreateConnection())
            {
                string query = "UPDATE Prodotti set Disponibile = 0 where IdProdotto = @Id";

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