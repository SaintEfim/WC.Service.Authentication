// using System.Globalization;
// using FluentValidation.TestHelper;
// using WC.Service.Authentication.Domain.Services.Validators;
// using WC.Service.Authentication.Domain.Test.Services.Data;
//
// namespace WC.Service.Authentication.Domain.Test.Services.Validators;
//
// public class LoginRequestValidatorTest
// {
//     private readonly LoginRequestValidator _validator = new();
//
//     public LoginRequestValidatorTest()
//     {
//         Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
//     }
//
//     [Fact]
//     public void LoginRequest_Positive_Create_New_Record()
//     {
//         var res = _validator.TestValidate(LoginRequestData.EmployeeAuthenticationLoginRequestModel());
//         res.ShouldNotHaveAnyValidationErrors();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_Empty_Email()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Email = string.Empty;
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("'Email' must not be empty.")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_Short_Email()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Email = new string('x', 7);
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Email must be at least 8 characters.")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_Long_Email()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Email = new string('x', 65);
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Email must be no more than 64 characters.")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_An_Incorrect_Email()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Email = "test@@mail.ru";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Invalid email address format.")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_Without_Special_Character()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Email = "testmail.ru";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Invalid email address format.")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_Not_Correct_Domain_In_Email()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Email = "test@mail.com";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Invalid domain 'mail.com'")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_Empty_Password()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Password = string.Empty;
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("'Password' must not be empty.")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_Short_Password()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Password = new string('x', 7);
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must be at least 8 characters.")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_Long_Password()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Password = new string('x', 65);
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must be no more than 64 characters.")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_Password_Without_Number()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Password = "Testttt@";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must contain at least one digit.")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_Password_Without_Lowercase_Letters()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Password = "TEEEEEEST@4";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must contain at least one lowercase letter.")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_Password_Without_Uppercase_Letters()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Password = "teeeeeeest@4";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must contain at least one uppercase letter.")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_Password_Without_Special_Character()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Password = "teeeeeeestE4";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must contain at least one special character.")
//             .Only();
//     }
//
//     [Fact]
//     public void LoginRequest_Negative_Create_New_Record_With_A_Password_That_Has_A_Space()
//     {
//         var model = LoginRequestData.EmployeeAuthenticationLoginRequestModel();
//         model.Password = "teeeeee estE4@";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password cannot contain whitespace characters.")
//             .Only();
//     }
// }


