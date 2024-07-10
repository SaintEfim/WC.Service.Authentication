// using System.Globalization;
// using FluentValidation.TestHelper;
// using WC.Service.Authentication.Domain.Services.Validators;
// using WC.Service.Authentication.Domain.Test.Services.Data;
//
// namespace WC.Service.Authentication.Domain.Test.Services.Validators;
//
// public class ResetPasswordValidatorTest
// {
//     private readonly ResetPasswordValidator _validator = new();
//
//     public ResetPasswordValidatorTest()
//     {
//         Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
//     }
//
//     [Fact]
//     public void ResetPassword_Positive_Create_New_Record()
//     {
//         var res = _validator.TestValidate(ResetPasswordData.ResetPasswordModel());
//         res.ShouldNotHaveAnyValidationErrors();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Empty_Email()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.Email = string.Empty;
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("'Email' must not be empty.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Short_Email()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.Email = new string('x', 7);
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Email must be at least 8 characters.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Long_Email()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.Email = new string('x', 65);
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Email must be no more than 64 characters.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_An_Incorrect_Email()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.Email = "test@@mail.ru";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Invalid email address format.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_Without_Special_Character()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.Email = "testmail.ru";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Invalid email address format.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Not_Correct_Domain_In_Email()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.Email = "test@mail.com";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Invalid domain 'mail.com'")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Empty_Old_Password()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.OldPassword = string.Empty;
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("'Old Password' must not be empty.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Short_Old_Password()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.OldPassword = new string('x', 7);
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must be at least 8 characters.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Long_Old_Password()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.OldPassword = new string('x', 65);
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must be no more than 64 characters.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Old_Password_Without_Number()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.OldPassword = "Testttt@";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must contain at least one digit.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Old_Password_Without_Lowercase_Letters()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.OldPassword = "TEEEEEEST@4";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must contain at least one lowercase letter.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Old_Password_Without_Uppercase_Letters()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.OldPassword = "teeeeeeest@4";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must contain at least one uppercase letter.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Old_Password_Without_Special_Character()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.OldPassword = "teeeeeeestE4";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must contain at least one special character.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_A_Old_Password_That_Has_A_Space()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.OldPassword = "teeeeee estE4@";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password cannot contain whitespace characters.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Empty_New_Password()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.NewPassword = string.Empty;
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("'New Password' must not be empty.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Short_New_Password()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.NewPassword = new string('x', 7);
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must be at least 8 characters.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_Long_New_Password()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.NewPassword = new string('x', 65);
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must be no more than 64 characters.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_New_Password_Without_Number()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.NewPassword = "Testttt@";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must contain at least one digit.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_New_Password_Without_Lowercase_Letters()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.NewPassword = "TEEEEEEST@4";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must contain at least one lowercase letter.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_New_Password_Without_Uppercase_Letters()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.NewPassword = "teeeeeeest@4";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must contain at least one uppercase letter.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_New_Password_Without_Special_Character()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.NewPassword = "teeeeeeestE4";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password must contain at least one special character.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_A_New_Password_That_Has_A_Space()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.NewPassword = "teeeeee estE4@";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("Password cannot contain whitespace characters.")
//             .Only();
//     }
//
//     [Fact]
//     public void ResetPassword_Negative_Create_New_Record_With_New_Password_Is_Equal_To_Old_Password()
//     {
//         var model = ResetPasswordData.ResetPasswordModel();
//         model.OldPassword = "Test1234@";
//         model.NewPassword = "Test1234@";
//         var res = _validator.TestValidate(model);
//         res.ShouldHaveAnyValidationError()
//             .WithErrorMessage("New password cannot be the same as the old password.")
//             .Only();
//     }
// }