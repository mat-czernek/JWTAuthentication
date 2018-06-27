namespace JWTWebApiAuth.Infrastructure
{
    /// <summary>
    /// Class used to map JWT settings from the json configuration file
    /// </summary>
    public class JWTSettings
    {
        public string SecretKey {get; set;}

        public string Issuer {get; set;}

        public string Audience {get; set;}

        public string ExpirationTime {get; set;}
    }
}