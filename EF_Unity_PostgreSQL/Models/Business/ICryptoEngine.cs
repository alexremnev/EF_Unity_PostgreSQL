﻿namespace EF_Unity_PostgreSQL.Models.Business
{
    public interface ICryptoEngine
    {
        /// <summary>
        /// Encrypt text.
        /// </summary>
        /// <param name="plainText">unencrypted text.</param>
        /// <returns>Encrypted text.</returns>
        string Encrypt(string plainText);
        /// <summary>
        /// Decrypt text.
        /// </summary>
        /// <param name="encryptText">cipher text.</param>
        /// <returns>Decrypted text.</returns>
        string Decrypt(string encryptText);
    }
}
