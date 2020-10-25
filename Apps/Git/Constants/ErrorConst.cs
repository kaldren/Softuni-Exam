using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Constants
{
    public static class ErrorConst
    {
        public const string InvalidRepoName = "Repository name must be between 3 and 10 characters long.";
        public const string InvalidDescriptionLength = "Description must be at least 5 characters long.";
        public const string UnableToDeleteCommit = "Unable to delete this commit.";
        public const string UnableToCreateCommit = "Couldn't create the commit. Please try again later.";
        public const string InvalidUsernameOrPassword = "Invalid username or password.";
        public const string InvalidUsernameLength = "Username should be between 5 and 20 character long.";
        public const string InvalidEmail = "Invalid email.";
        public const string RequiredPassword = "Password is required and should be between 6 and 20 characters.";
        public const string PasswordMismatch = "Passwords do not match.";
        public const string EmailTaken = "Email already taken.";
        public const string UsernameTaken = "Username already taken.";
        public const string UnableToCreateAccount = "Couldn't create an account. Please try again later.";
    }
}
