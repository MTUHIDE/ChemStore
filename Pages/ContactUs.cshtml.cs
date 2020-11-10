using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChemStoreWebApp.Pages
{

    public class ContactFormModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }

    public class ContactUsModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("Index");
        }

        [BindProperty]
        public ContactFormModel Contact { get; set; }
        public void OnGet()
        {

        }

        private void SendMail(string mailbody)
        {
            using (var message = new MailMessage(Contact.Email, "jeholtre@mtu.edu"))
            {
                message.To.Add(new MailAddress("jeholtre@mtu.edu"));
                message.From = new MailAddress(Contact.Email);
                message.Subject = "New E-Mail from my website";
                message.Body = mailbody;
                using (var smtpClient = new SmtpClient("mail.mydomain.com"))
                {
                    smtpClient.Send(message);
                }
            }
        }
    }
}
