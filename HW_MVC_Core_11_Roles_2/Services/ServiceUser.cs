using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HW_MVC_Core_11_Roles_2.Models;
using Microsoft.AspNetCore.Authorization;

namespace HW_MVC_Core_11_Roles_2.Services
{
    public interface IServiceUser
    {
        Task<object> CreateUserAsync(UserModel model);
        Task<object> CreateRoleAsync(RoleModel roleModel);
        Task<object> AsignRoleAsync(AssignRoleUserModel model);
        Task<object> UpdateUserAsync(UserModel model);
        Task<object> DeleteUserAsync(UserModel model);
        
    }
    public class ServiceUser : IServiceUser
    {
        public  UserManager<IdentityUser> _userManager;
        public  SignInManager<IdentityUser> _signInManager;
        //Продовжити назначення ролі
        public  RoleManager<IdentityRole> _roleManager;
        public ServiceUser(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<object> AsignRoleAsync(AssignRoleUserModel model)
        {
            if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.RoleName))
            {
                return new { error = "User ID or Role Name is required." };
            }

            // Пошук користувача за ID
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return new { error = "User not found." };
            }

            // Перевірка, чи існує роль
            var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!roleExists)
            {
                return new { error = $"Role '{model.RoleName}' not found." };
            }

            // Додавання користувача до ролі
            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (result.Succeeded)
            {
                return new { message = "Role assigned successfully." };
            }

            // Повернення помилок, якщо щось пішло не так
            var errors = result.Errors.Select(e => e.Description).ToList();
            return new { errors };
        }


        public async Task<object> CreateRoleAsync(RoleModel roleName)
        {
            if (string.IsNullOrEmpty(roleName.roleName))
            {
                return new { error = "Role name is required." };
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName.roleName);
            if (roleExists)
            {
                return new { error = "Role already exists." };
            }

            // Check if the user is authenticated
            if (/*User.Identity.IsAuthenticated*/true)
            {
                var role = new IdentityRole { Name = roleName.roleName };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return new { message = $"The role: {role.Name} is created successfully." };
                }

                // Return errors as a list of strings
                var errors = result.Errors.Select(e => e.Description).ToList();
                return new { errors };
            }

            return new { error = "User is not authenticated. Role creation failed." };
        }

        public async Task<object> CreateUserAsync(UserModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return new { error = "Email and password are required." };
            }

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return new { message = "User created successfully." };
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return new { errors };
        }


        public async Task<object> DeleteUserAsync(UserModel model)
        {
            if (string.IsNullOrEmpty(model.UserId))
            {
                return new { error = "User ID is required." };
            }

            // Find the user by ID
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return new { error = "User not found." };
            }

            // Attempt to delete the user
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new { message = "User deleted successfully." };
            }

            // Collect and return any errors that occurred during deletion
            var errors = result.Errors.Select(e => e.Description).ToList();
            return new { errors };
        }

        public async Task<object> UpdateUserAsync(UserModel model)
        {
            if (string.IsNullOrEmpty(model.UserId))
            {
                return new { error = "User ID is required." };
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return new { error = "User not found." };
            }

            // Update properties as needed
            user.Email = model.Email ?? user.Email;
            user.UserName = model.Email ?? user.UserName;

            // If you want to update the password
            if (!string.IsNullOrEmpty(model.Password))
            {
                var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetResult = await _userManager.ResetPasswordAsync(user, passwordResetToken, model.Password);
                if (!resetResult.Succeeded)
                {
                    var errors = resetResult.Errors.Select(e => e.Description).ToList();
                    return new { errors };
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
            {
                return new { message = "User updated successfully." };
            }

            var updateErrors = updateResult.Errors.Select(e => e.Description).ToList();
            return new { errors = updateErrors };
        }
    }
}
