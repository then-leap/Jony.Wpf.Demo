using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Jony.Demo.SideMenu.Messages;

public class NavigateMessage(string route) : ValueChangedMessage<string>(route);