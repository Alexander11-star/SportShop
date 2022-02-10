using API_RestFull_Prueba.Models.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace API_RestFull_Prueba.Models.LoginUser
{
    public class LoginUserDAL : LoginUserDTO
    {
        private readonly string _conectionString;
        public LoginUserDAL(string conectionString)
        {
            _conectionString = conectionString;
        }

        public Usuario AutenticarUsuario(LoginUserDTO pLogin)
        {
            Connection db = new Connection(_conectionString);
            var usuario = new Usuario();
            usuario.Result = new Resultado();
            usuario.Valido = false;
            byte[] Contasena = new byte[0];
            byte[] KEY = new byte[0]; ;
            byte[] IV = new byte[0]; ;

            var dtDetalleEncueta = new DataTable();
            try
            {
                if (String.IsNullOrEmpty(pLogin.password) || String.IsNullOrEmpty(pLogin.Email))
                {
                    usuario.Result.Valor = 100;
                    usuario.Result.Mensaje = "El usuario y la clave son obligatorios";
                    return usuario;
                }
                db.SQLParametros.Clear();
                db.SQLParametros.AddWithValue("@pEmail", pLogin.Email);
                db.TiempoEsperaComando = 0;
                var reader = db.EjecutarReader("SP_ConsultarUsuario", CommandType.StoredProcedure);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (!Convert.IsDBNull(reader["idUser"])) { usuario.idUsuario = (int)reader["idUser"]; }
                        if (!Convert.IsDBNull(reader["fullName"])) { usuario.nombreCompleto = reader["fullName"].ToString(); }
                        if (!Convert.IsDBNull(reader["idDocument"])) { usuario.idDocument = (int)reader["idDocument"]; }

                        usuario.password = pLogin.password;
                        if (!Convert.IsDBNull(reader["Email"])) { usuario.Email = (string)reader["Email"]; }
                        if (!Convert.IsDBNull(reader["password"])) { Contasena = (byte[])reader["password"]; }
                        if (!Convert.IsDBNull(reader["pKEY"])) { KEY = (byte[])reader["pKEY"]; }
                        if (!Convert.IsDBNull(reader["pIv"])) { IV = (byte[])reader["pIv"]; }


                        string contrasenaFinal = DecryptStringFromBytes(Contasena, KEY, IV);
                        if (contrasenaFinal == pLogin.password)
                        {
                            usuario.Valido = true;
                        }
                        else
                        {
                            usuario.Valido = false;
                        }
                    }
                    else
                    {
                        return usuario;
                    }

                }
                else
                {
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                //db.AbortarTransaccion();
                //usuario.Result.Valor = 100;
                usuario.Result.Mensaje = "Se genero un error inesperado:" + ex.Message;
                return usuario;
            }
            finally { }
            if (db != null)
            {
                db.Dispose();
            }

            return usuario;
        }
        public string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }



    }
}
