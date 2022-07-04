using System;
using System.Text;
using System.Text.RegularExpressions;
using Calendar.model;
using Calendar.view;

namespace Calendar.controller
{
    public class RegisterController
    {
        MainController mainController = MainController.GetInstance();


        private static RegisterController instance;

        private RegisterController()
        {
        }

        public static RegisterController GetInstance()
        {
            if (instance == null)
                instance = new RegisterController();
            return instance;
        }

        public string Register(string name, string password)
        {
            if (Regex.IsMatch(name, "^[0-9A-Za-z_]*$"))
            {
                if (User.GetUserByName(name) == null)
                {
                    if (Regex.IsMatch(password, "^[0-9A-Za-z_]*$"))
                    {
                        Match match1 = Regex.Match(password, "[A-Z]");
                        Match match2 = Regex.Match(password, "[a-z]");
                        Match match3 = Regex.Match(password, "[0-9]");
                        bool lenCond = password.Length >= 5;
                        if (match1.Success && match2.Success && match3.Success && lenCond)
                        {
                            User user = new User(name, password);
                            return Responses.SuccessfulRegistering;
                        }

                        return Responses.WeakPassword;
                    }

                    return Responses.InvalidPassword;
                }

                return Responses.ExistedUser;
            }

            return Responses.InvalidUserName;
        }


        public string Login(string name, string password)
        {
            if (Regex.IsMatch(name, "^[0-9A-Za-z_]*$"))
            {
                if (User.GetUserByName(name) != null)
                {
                    User user = User.GetUserByName(name);
                    if (Regex.IsMatch(password, "^[0-9A-Za-z_]*$"))
                    {
                        if (user.Password == password)
                        {
                            User.LoggedInUser = user;
                            Menu.currentMenu = MenuName.MainMenu;
                            if (user.IsFirstLogin)
                            {
                                user.IsFirstLogin = false;
                                return mainController.Create(user.Name, name) + "\n" + Responses.SuccessfulLogin +
                                       "\n" + Responses.MainMenu;
                            }

                            return Responses.SuccessfulLogin + "\n" + Responses.MainMenu;
                        }

                        return Responses.WrongPassword;
                    }

                    return Responses.InvalidPassword;
                }

                return Responses.NotExistedUser;
            }

            return Responses.InvalidUserName;
        }


        public string ChangePassword(string name, string oldPass, string newPass)
        {
            if (Regex.IsMatch(name, "^[0-9A-Za-z_]*$"))
            {
                if (User.GetUserByName(name) != null)
                {
                    User user = User.GetUserByName(name);
                    if (Regex.IsMatch(oldPass, "^[0-9A-Za-z_]*$"))
                    {
                        if (oldPass == user.Password)
                        {
                            if (Regex.IsMatch(newPass, "^[0-9A-Za-z_]*$"))
                            {
                                Match match1 = Regex.Match(newPass, "[A-Z]");
                                Match match2 = Regex.Match(newPass, "[a-z]");
                                Match match3 = Regex.Match(newPass, "[0-9]");
                                bool lenCond = newPass.Length >= 5;
                                if (match1.Success && match2.Success && match3.Success && lenCond)
                                {
                                    user.Password = newPass;
                                    return Responses.SuccessfulPasswordChanging;
                                }

                                return Responses.NewWeakPassword;
                            }

                            return Responses.InvalidNewPassword;
                        }

                        return Responses.WrongPassword;
                    }

                    return Responses.InvalidOldPassword;
                }

                return Responses.NotExistedUser;
            }

            return Responses.InvalidUserName;
        }


        public string Remove(string name, string pass)
        {
            if (Regex.IsMatch(name, "^[0-9A-Za-z_]*$"))
            {
                if (User.GetUserByName(name) != null)
                {
                    User user = User.GetUserByName(name);
                    if (Regex.IsMatch(pass, "^[0-9A-Za-z_]*$"))
                    {
                        if (pass == user.Password)
                        {
                            var ToBeRemoved = User.Users.Find(x => x.Name == name);
                            User.Users.Remove(ToBeRemoved);
                            return Responses.SuccessfulRemoving;
                        }

                        return Responses.WrongPassword;
                    }

                    return Responses.InvalidPassword;
                }

                return Responses.NotExistedUser;
            }

            return Responses.InvalidUserName;
        }


        public string ShowAllUsers()
        {
            StringBuilder allUsers = new StringBuilder();
            if (User.Users.Count != 0)
            {
                User.Users.Sort((x, y) => String.Compare(x.Name, y.Name, StringComparison.Ordinal));
                foreach (User user in User.Users)
                {
                    allUsers.Append(user.Name);
                    allUsers.Append("\n");
                }

                return allUsers.ToString().Trim();
            }

            return "nothing";
        }
    }
}