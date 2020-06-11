namespace Ejercicio2.Api.Transversal.Email.Entities
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            Body = new BodyMessage();
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public BodyMessage Body { get; set; }
    }

    public class BodyMessage 
    { 
        public string SubMimeType { get; set; }
        public string Text { get; set; }
    }
}
