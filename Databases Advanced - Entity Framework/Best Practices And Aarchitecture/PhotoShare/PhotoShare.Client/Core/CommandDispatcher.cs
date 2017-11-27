using System.Linq;
using PhotoShare.Client.Core.Commands;

namespace PhotoShare.Client.Core
{
    using System;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            var commands = commandParameters.Select(x => x.ToLower()).ToArray();


            if (commands.Length == 5 && commands[0] == "registeruser")
            {
                return RegisterUserCommand.Execute(commands);
            }

            else if (commands[0] == "login" && commands.Length == 3)
            {
                return LoginCommand.Execute(commands);
            }

            if (commands.Length == 3 && commands[0] == "addtown")
            {
                return AddTownCommand.Execute(commands);
            }

            else if (commands.Length == 4 && commands[0] == "modifyuser")
            {
                return ModifyUserCommand.Execute(commands);
            }

            else if (commands.Length == 2 && commands[0] == "deleteuser")
            {
                return DeleteUser.Execute(commands);
            }
            else if (commands.Length == 2 && commands[0] == "addtag")
            {
                return AddTagCommand.Execute(commands);
            }

            else if (commands[0] == "createalbum" && commands.Length >= 5)
            {
                return CreateAlbumCommand.Execute(commands);
            }

            else if (commands[0] == "addtagto" && commands.Length == 3)
            {
                return AddTagToCommand.Execute(commands);
            }

            else if (commands[0] == "addfriend" && commands.Length == 3)
            {
                return AddFriendCommand.Execute(commands);
            }

            else if (commands[0] == "acceptfriend" && commands.Length == 3)
            {
                return AcceptFriendCommand.Execute(commands);
            }

            else if (commands[0] == "listfriends" && commands.Length == 2)
            {
                return PrintFriendsListCommand.Execute(commands);
            }

            else if (commands[0] == "sharealbum" && commands.Length == 4)
            {
                return ShareAlbumCommand.Execute(commands);
            }

            else if (commands[0] == "uploadpicture" && commands.Length == 4)
            {
                return UploadPictureCommand.Execute(commands);
            }

            else if (commands[0] == "logout" && commands.Length == 1)
            {
                return LogOutCommand.Execute(commands);
            }

            else if (commands[0] == "exit" && commands.Length == 1)
            {
                return ExitCommand.Execute();
            }

            return $"Command not valid!";
        }
    }
}
