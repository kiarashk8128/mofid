using ConsoleApp2.Model;
using System;
using System.Text.RegularExpressions;
using System.Collections;
using ConsoleApp2.View;

namespace ConsoleApp2.Controller;

public class ProgramController
{
    public static string currentUserName;
    public void run()
    {
        string command = Console.ReadLine();
        while (!command.Equals("exit"))
        {
            if (Regex.IsMatch(command, "register (.+)"))
            {
                Match match = commandMatcher(command, "register (.+)");
                Console.WriteLine(register(match).ToString());
            }

            else if (Regex.IsMatch(command, "login (.+)"))
            {
                Match match = commandMatcher(command, "login (.+)");
                Console.WriteLine(login(match).ToString());
                if (login(match).ToString().Equals(RegisterMenuResponses.LOGIN_SUCCESSFUL.ToString()) )
                {
                    
                    MenuController menu = new MenuController();
                    menu.run();
                }
            }
            else if (Regex.IsMatch(command, "change password (.+)"))
            {
                Match match = commandMatcher(command, "change password (.+)");
                Console.WriteLine(changePassword(match).ToString());
            }
            else if (Regex.IsMatch(command, "remove (.+)"))
            {
                Match match = commandMatcher(command, "remove (.+)");
                Console.WriteLine(removeUser(match).ToString());
            }
            else if (Regex.IsMatch(command, "show all users"))
            {
                showAllUsers();
            }
            else
            {
                Console.WriteLine("invalid command");
            }

            command = Console.ReadLine();
        }

        Environment.Exit(0);
    }

    private RegisterMenuResponses register(Match match)
    {
        string need = match.Groups[1].Value.Trim();
        String[] needed = Regex.Split(need, "\\s+");
        string username = needed[0];
        string password = needed[1].Trim();
        if (Regex.IsMatch(username, "(^\\w+$)"))
        {
            if (Regex.IsMatch(password, "(^\\w+$)"))
            {
                if (Regex.IsMatch(password, "((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])([a-zA-Z0-9_]{5,}))"))
                {
                    if (User.getUserByUsername(username) == null)
                    {
                        
                        User user = new User(username, password);
                        Calendar calendar=new Calendar(username,username,true);
                        
                        return RegisterMenuResponses.REGISTER_SUCCESSFUL;
                    }

                    return RegisterMenuResponses.USER_WITH_THIS_USERNAME_EXISTS;
                }

                return RegisterMenuResponses.WEAK_PASSWORD;
            }

            return RegisterMenuResponses.INVALID_PASSWORD;
        }

        return RegisterMenuResponses.INVALID_USERNAME;
    }

    private RegisterMenuResponses login(Match match)
    {
        string need = match.Groups[1].Value.Trim();
        String[] needed = Regex.Split(need, "\\s+");
        string username = needed[0];
        string password = needed[1].Trim();
        if (Regex.IsMatch(username, "(^\\w+$)"))
        {
            if (Regex.IsMatch(password, "(^\\w+$)"))
            {
                if (Regex.IsMatch(password, "((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])([a-zA-Z0-9_]{5,}))"))
                {
                    if (User.getUserByUsername(username) != null)
                    {
                        if (User.getUserByUsername(username).Password == password)
                        {
                            
                            currentUserName = username;
                            return RegisterMenuResponses.LOGIN_SUCCESSFUL;
                        }

                        return RegisterMenuResponses.INVALID_PASSWORD;
                    }

                    return RegisterMenuResponses.NO_USER_WITH_THIS_USERNAME_EXISTS;
                }

                return RegisterMenuResponses.WEAK_PASSWORD;
            }

            return RegisterMenuResponses.INVALID_PASSWORD;
        }

        return RegisterMenuResponses.INVALID_USERNAME;
    }

    private RegisterMenuResponses changePassword(Match match)
    {
        string need = match.Groups[1].Value.Trim();
        String[] needed = Regex.Split(need, "\\s+");
        string username = needed[0].Trim();
        string oldPassword = needed[1].Trim();
        string newPassword = needed[2].Trim();
        if (Regex.IsMatch(username, "(^\\w+$)"))
        {
            if (User.getUserByUsername(username) != null)
            {
                if (Regex.IsMatch(oldPassword, "(^\\w+$)"))
                {
                    if (Regex.IsMatch(oldPassword, "((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])([a-zA-Z0-9_]{5,}))"))
                    {
                        if (User.getUserByUsername(username).Password == oldPassword)
                        {
                            if (Regex.IsMatch(newPassword, "(^\\w+$)"))
                            {
                                if (Regex.IsMatch(newPassword, "((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])([a-zA-Z0-9_]{5,}))"))
                                {
                                    return RegisterMenuResponses.PASSWORD_CHANGED_SUCCESFULLY;
                                }

                                return RegisterMenuResponses.NEW_PASSWORD_IS_WEAK;
                            }

                            return RegisterMenuResponses.INVALID_NEW_PASSWORD;
                        }

                        return RegisterMenuResponses.PASSWORD_IS_WRONG;
                    }

                    return RegisterMenuResponses.OLD_PASSWORD_IS_WEAK;
                }

                return RegisterMenuResponses.INVALID_PASSWORD;
            }

            return RegisterMenuResponses.NO_USER_WITH_THIS_USERNAME_EXISTS;
        }

        return RegisterMenuResponses.INVALID_USERNAME;
    }

    private RegisterMenuResponses removeUser(Match match)
    {
        string need = match.Groups[1].Value.Trim();
        String[] needed = Regex.Split(need, "\\s+");
        string username = needed[0];
        string password = needed[1].Trim();
        if (Regex.IsMatch(username, "(^\\w+$)"))
        {
            if (Regex.IsMatch(password, "(^\\w+$)"))
            {
                if (User.getUserByUsername(username) != null)
                {
                    if (User.getUserByUsername(username).Password == password)
                    {
                        User.removeUser(username);
                        return RegisterMenuResponses.REMOVED_SUCCESSFULLY;
                    }

                    return RegisterMenuResponses.PASSWORD_IS_WRONG;
                }

                return RegisterMenuResponses.NO_USER_WITH_THIS_USERNAME_EXISTS;
            }

            return RegisterMenuResponses.INVALID_PASSWORD;
        }

        return RegisterMenuResponses.INVALID_USERNAME;
    }

    private void showAllUsers()
    {
        List<User> users = User.getAllUsers();
        List<string> userNames = new List<string>();
        foreach (User user in users)
        {
            userNames.Add(user.Username);
            Console.WriteLine(user.Username);
        }

        if (userNames.Count == 0)
        {
            Console.WriteLine("nothing");
        }
    }

    private Match commandMatcher(string command, string pattern)
    {
        Match match = Regex.Match(command, pattern);
        return match;
    }
}