namespace JWTWebApiAuth.Infrastructure
{
    public class JWTSettings
    {
        public string SecretKey {get; set;}

        public string Issuer {get; set;}

        public string Audience {get; set;}

        public string ExpirationTime {get; set;}
    }
}