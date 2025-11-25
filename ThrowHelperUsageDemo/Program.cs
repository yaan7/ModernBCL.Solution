using System;

namespace ModernBCL.Demo
{
    public class UserProfileService
    {
        // Example method using the polyfilled ThrowHelper
        public void UpdateUserProfile(string userId, object userSettings)
        {
            // 1. Throws ArgumentNullException if userId is null, 
            //    or ArgumentException if it's empty or whitespace.
            //    The parameter name ("userId") is automatically captured.
            ThrowHelper.ThrowIfNullOrWhiteSpace(userId);

            // 2. Throws ArgumentNullException if userSettings is null.
            //    The parameter name ("userSettings") is automatically captured.
            ThrowHelper.ThrowIfNull(userSettings);

            // If execution reaches here, all arguments are valid.
            Console.WriteLine($"Profile updated successfully for user: {userId}");
            // ... actual update logic ...
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var service = new UserProfileService();
            Console.WriteLine("--- Testing valid input ---");
            try
            {
                service.UpdateUserProfile("john.doe.123", new object());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.GetType().Name}: {ex.Message}");
            }

            Console.WriteLine("\n--- Testing null userId ---");
            try
            {
                string userId = null;
                service.UpdateUserProfile(userId, new object());
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"[SUCCESS] Caught: {ex.GetType().Name}. Param: {ex.ParamName}");
                Console.WriteLine($"Message: {ex.Message}");
            }

            Console.WriteLine("\n--- Testing whitespace userId ---");
            try
            {
                string whitespaceId = "  \t  ";
                service.UpdateUserProfile(whitespaceId, new object());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[SUCCESS] Caught: {ex.GetType().Name}. Param: {ex.ParamName}");
                Console.WriteLine($"Message: {ex.Message}");
            }
        }
    }
}