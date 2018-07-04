using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Infrastructure.Constants
{
    public static class MembershipHeader
    {
        public static readonly string Login = "Login/Member Registration";
        public static readonly string ForWHOSaler = "For Member";
        public static readonly string WelcomeText = "Welcome";
        public static readonly string LogoutText = "Logout";
        public static class Modal
        {
            public static readonly string HeaderText = @"You are an agency";
            public static readonly string DescriptionText = @"Vuong enter text";
            public static readonly string StatementText = @"* If 'No', return to the site top page.";
            public static readonly string YesButtonText = "Yes";
            public static readonly string NoButtonText = "No";
        }
    }
    public static class Header
    {
        public static class Index
        {
            public static readonly string ForWHOSaler = "For Member";
            public static readonly string Login = "Login";
            public static readonly string Logout = "Logout";
            public static readonly string Menu = "Menu";
        }
    }
    public static class ResetPassword
    {
        public class Index
        {
            public static readonly string Title = "Reset Password";
            public static readonly string Statement1 = "Please enter the e-mail address at the time of registration and your registered name and click 'Send' button";
            public static readonly string Statement2 = "* Since we will issue a new password, we will not be able to use your forgotten password.";
            public static readonly string EmailPlaceHolder = "Mail Address";
            public static readonly string Example = "wanko @vetzpetz.co";
            public static readonly string SendButton = "Send";
            public static readonly string RequiredEmailValidationMessage = "Please fill out this field.";
            public static readonly string MismatchEmailValidatationMessage = "Email address is incorrect. Please check again.";
        }

        public class Success
        {
            public static readonly string Message = "New password has been sent to email";
        }

        public static readonly string HasResetPasswordForUser = "HasResetPasswordForUser";
    }
    public static class Login
    {
        public static class _FormSection
        {
            public static readonly string MemberLoginText = "Login";
            public static readonly string EmailHelpText = "Example: wanko@livin.market.com";
            public static readonly string PasswordHelpText = "If you forgot your password, click here";
            public static readonly string ButtonText = "Login";
            public static readonly string RegistrationButtonText = "Register as a member";
            public static readonly string EmailText = "Email";
            public static readonly string PasswordText = "Password";
        }

        public static class Validations
        {
            public static readonly string Failure = "Your email or password is incorect.";
        }
    }
    public static class HomePage
    {
        public static class Index
        {
            public static readonly string Top = "Top";
        }
    }
    public static class RegistrationPage
    {
        public static readonly string Name = "NEW CUSTOMER";
        public static readonly string Note = "All fields marked with a * are mandatory.";
        public static readonly string Lastname = "First Name";
        public static readonly string Firstname = "First Name";
        public static readonly string Title= "Title";
        public static readonly string Email = "Email";
        public static readonly string StreetNameAndHouse = "Street Name & House Number";
        public static readonly string VillageOrTown = "Village or Town";
        public static readonly string Country = "Country";
        public static readonly string PostCode = "Postal Code";
        public static readonly string Phone = "Phone Number";
        public static readonly string Password = "Password";
        public static readonly string PasswordHiny = "Password Hint";
    }
}
