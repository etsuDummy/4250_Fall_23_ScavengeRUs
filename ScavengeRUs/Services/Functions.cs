using System;
using System.IO;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Google.Cloud.SecretManager.V1;
using ScavengeRUs.Services;
using System.Net;

public class Functions
{
    /// <summary>
    /// a set of configurations that aid in the functionality of this class
    /// typically are set once and do not change
    /// </summary>
    private static IConfiguration? _configuration;


    /// <summary>
    /// sets the configuration for the Functions class
    /// </summary>
    /// <param name="configuration"></param>
    public static void SetConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    /// <summary>
    /// uses an SMTP client running an existing buc hunt email and google's SMTP service to send emails to recipients email address
    /// </summary>
    /// <param name="recipientEmail"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <returns></returns>
	public static async Task SendEmail(string recipientEmail, string subject, string body)
    {

            // Read the password from the text file
            //NOTE: If you are part of the next group of SE1 students and need the login info,
            //please contact either SCUTTW@ETSU.EDU, or grantscutt2@gmail.com, preferably from a school account
            string passwordFilePath = Path.Combine(Directory.GetCurrentDirectory(), "gmail.txt");
            string emailPassword = File.ReadAllText(passwordFilePath).Trim();

            // Configure SMTP client
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("etsubuchunt98@gmail.com", emailPassword);
                client.EnableSsl = true;

            // Create email message
            using (MailMessage message = new MailMessage("etsubuchunt98@gmail.com", recipientEmail))
                {
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;

                    // Send email
                    client.Send(message);
                }
            }
            #region Google Secrets Manager Code (not currently in use)
            // Set the environment variable
            //string credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "scrum-bums-042e94718ef6.json");
            //Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);

            //// Get email settings
            //string emailFrom = _configuration.GetValue<string>("EmailSettings:EmailFrom");
            //// Use the Secret Manager API to get the email password
            //string password = GetSecretAsync("etsuscavengerhuntemail").Result;

            //Console.WriteLine(emailFrom);
            //Console.WriteLine(password);

            //// Configure SMTP client
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            //client.EnableSsl = true;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new System.Net.NetworkCredential(emailFrom, password);

            //// Create email message
            //MailMessage message = new MailMessage(emailFrom, recipientEmail);
            //message.Subject = subject;
            //message.Body = body;

            ////TODO: consider try/catch
            //client.Send(message);



            ///// <summary>
            ///// asychronously fetches secrets
            ///// </summary>
            ///// <param name="secretId"></param>
            ///// <returns></returns>
            //private static async Task<string> GetSecretAsync(string secretId)
            //{
            //    var secretManagerClient = await SecretManagerServiceClient.CreateAsync();
            //    var secretName = new SecretName("341278893241", secretId);
            //    var latestVersion = await secretManagerClient.GetSecretVersionAsync(new GetSecretVersionRequest
            //    {
            //        SecretVersionName = new SecretVersionName(secretName.ProjectId, secretName.SecretId, "latest")
            //    });
            //    var accessRequest = new AccessSecretVersionRequest
            //    {
            //        Name = latestVersion.SecretVersionName.ToString(),
            //    };
            //    var response = await secretManagerClient.AccessSecretVersionAsync(accessRequest);
            //    var passwordBytes = response.Payload.Data;
            //    string password = passwordBytes.ToStringUtf8();
            //    return password;
    }
    #endregion
}
