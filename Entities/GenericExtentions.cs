namespace AssignmentManager.Entities
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Common Extention methods.
    /// </summary>
    public static class GenericExtentions
    {
        /// <summary>
        /// Hashes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>SHA256 Hash of the value.</returns>
        public static string Hash(this string value)
        {
            var sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}
